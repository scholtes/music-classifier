import os
import re
import subprocess
import sys

BEXTRACT_DIRECTORY = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'bin'))
BEXTRACT_FILENAME = os.path.join(BEXTRACT_DIRECTORY, "bextract.exe")
FFMPEG_FILENAME = os.path.join(BEXTRACT_DIRECTORY, "ffmpeg.exe")
TEMP_DIRECTORY = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'tmp'))
MKCOLLECTION = os.path.join(TEMP_DIRECTORY, 'music.mk')

# Number of samples in bextract window
WINDOW_FS = str(2**21)

# bextract.exe -fe -ws 2097152 -hp 2097152 -od path\to\whatever\ music.mf

def extract(filenames, window_fs=WINDOW_FS, usesrc=False):
	'''
	parameters: 
	    filenames - a list of filenames to any audio files (mp3, wav, etc)
	    window_fs - the window size and hop size for bextract (string, not integer)
	    usesrc - if true, do not copy and convert with ffmpeg.  If false, copy and convert
	returns:
		A list of dicts containing bextract data.  Each element of list is formatted like: 
		    {"name": "/full/path/to/music.mp3",
		    "data": [
		    	[0.1234, 0.4321, 0.420, ... ],
		    	[0.2345, 0.5432, 0.421, ... ], ...
		    ]}
	'''
	files = [os.path.abspath(file) for file in filenames]
	if not os.path.exists(TEMP_DIRECTORY):
		os.makedirs(TEMP_DIRECTORY)
	if not usesrc:
		# Do ffmpeg conversions
		tempfiles = []
		for file in files: 
			newfile = os.path.join(TEMP_DIRECTORY, os.path.basename(file))
			while (newfile+".wav") in tempfiles:
				newfile += "_dup"
			newfile += ".wav"
			tempfiles.append(newfile)
			p = subprocess.Popen([FFMPEG_FILENAME, '-loglevel', 'quiet', '-y', '-i', file, newfile])
			p.wait()
	else:
		tempfiles = files
	# Create mkcollection
	tempfilesstrlist= "\n".join(tempfiles)
	with open(MKCOLLECTION, 'w') as f:
		f.write(tempfilesstrlist)
	# Run bextract
	with open(os.devnull, 'w') as fp:
		p = subprocess.Popen([BEXTRACT_FILENAME, '-fe', '-ws', window_fs, '-hp', window_fs, '-od', TEMP_DIRECTORY+os.sep, MKCOLLECTION], cwd=TEMP_DIRECTORY, stdout=fp)
		p.wait()
	# Parse bextract data
	arff = ""
	with open(os.path.join(TEMP_DIRECTORY, 'MARSYAS_EMPTY'), 'r') as f:
		arff = f.read()
	data = _parse_arff(arff, tempfiles, files)
	# Cleanup
	if not usesrc:
		for file in tempfiles:
			if os.path.isfile(file):
				os.remove(file)
	if(os.path.isfile(MKCOLLECTION)):
		os.remove(MKCOLLECTION)
	if(os.path.isfile(os.path.join(TEMP_DIRECTORY, 'bextract_single.mf'))):
		os.remove(os.path.join(TEMP_DIRECTORY, 'bextract_single.mf'))
	if(os.path.isfile(os.path.join(TEMP_DIRECTORY, 'MARSYAS_EMPTY'))):
		os.remove(os.path.join(TEMP_DIRECTORY, 'MARSYAS_EMPTY'))
	try:
		os.rmdir(TEMP_DIRECTORY)
	except OSError as ex:
		pass
	return data

def _parse_arff(arff, tempfiles, properfiles):
	'''
	Takes: arff string from bextract
	Returns: list that contains dictionaries that look like this:
		{"name": "/full/path/to/music.mp3",
	    "data": [
	    	[0.1234, 0.4321, 0.420, ... ],
	    	[0.2345, 0.5432, 0.421, ... ], ...
	    ]}
	'''
	zippedfiles = dict(zip(tempfiles, properfiles))
	dictions = []
	nocrud = arff.split("@data")[1].strip()
	blocks = [block.strip() for block in nocrud.split("% filename") if block.strip() != ""]
	for block in blocks:
		lines = block.split("\n")
		name = lines[0]
		datalines = lines[2:]
		data = [[float(datum) for datum in dataline.split(",")[:-1]] for dataline in datalines]
		dictions.append({"name": zippedfiles[name], "data": data})
	return dictions
