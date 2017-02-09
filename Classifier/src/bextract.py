import os
import sys
import subprocess

BEXTRACT_DIRECTORY = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'bin'))
BEXTRACT_FILENAME = os.path.join(BEXTRACT_DIRECTORY, "bextract.exe")
FFMPEG_FILENAME = os.path.join(BEXTRACT_DIRECTORY, "ffmpeg.exe")
TEMP_DIRECTORY = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'tmp'))

# Number of samples in bextract window
WINDOW_FS = 2**21

# bextract.exe -fe -ws 2097152 -hp 2097152 -od path\to\whatever\ music.mf

def extract(filenames):
	'''
	parameters: 
	    filenames - a list of filenames to any audio files (mp3, wav, etc)
	returns:
		A list of dicts containing bextract data.  Each element of list is formatted like: 
		    {"name": "/full/path/to/music.mp3",
		    "data": [0.1234, 0.4321, 0.420, ... ]}
	'''
	files = [os.path.abspath(file) for file in filenames]
	if not os.path.exists(TEMP_DIRECTORY):
		os.makedirs(tmp)
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
	# Cleanup 