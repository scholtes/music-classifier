import sys
import os
import glob
from src import bextract

files = [glob.glob(arg, recursive=True) for arg in sys.argv[1:]]
files = [os.path.abspath(val) for subfiles in files for val in subfiles]

bextract.extract(files)