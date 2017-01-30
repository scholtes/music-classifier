with open('listnames') as f:
	listnames_raw = f.read()

with open('sorteds') as f:
	sorteds_raw = f.read()

listnames = [f.lower().strip() for f in listnames_raw.split('\n')]
sorteds = [f.lower().strip() for f in sorteds_raw.split('\n')]

for listname in listnames:
	print(sorteds.index(listname)+1)