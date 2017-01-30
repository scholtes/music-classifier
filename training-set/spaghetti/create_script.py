with open('filenames') as f:
	filenames_raw = f.read()

with open('filenames_reduced') as f:
	filenames_reduced_raw = f.read()

with open('listnames') as f:
	listnames_raw = f.read()

with open('times') as f:
	times_raw = f.read()

filenames = filenames_raw.split('\n')
filreduce = [f.lower().strip() for f in filenames_reduced_raw.split('\n')]
listnames = [f.lower().strip() for f in listnames_raw.split('\n')]
times = [t.split(' ') for t in times_raw.split('\n')]

count = 0
string = ""
for listname in listnames:
	time = times[count]
	idx = 0
	for i in range(0, len(filenames)):
		if listname in filreduce[i]:
			idx = i
			break
	string += 'ffmpeg -y -i "mp3\\' + filenames[idx] + '" '
	string += '-ss 00:0' + time[0] + ' -to 00:0' + time[1] + ' '
	string += '"wav\\' + listname + '.wav"\n'
	count += 1
print(string)