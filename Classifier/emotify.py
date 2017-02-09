import sys
import os
import glob
import json
from src import bextract
from src import linregress

files = [glob.glob(arg, recursive=True) for arg in sys.argv[1:]]
files = [os.path.abspath(val) for subfiles in files for val in subfiles]

data = bextract.extract(files)
results = linregress.classify(data)
print(json.dumps(results))