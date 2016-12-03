import tkinter as tk
from tkinter import ttk

def main():
	###
	root = tk.Tk()
	gui = ttk.Frame(root, width=600, height=800)

	slider1 = ttk.Scale(gui, from_=-1, to=1, orient=tk.HORIZONTAL, length=250)
	slider2 = ttk.Scale(gui, from_=-1, to=1, orient=tk.HORIZONTAL, length=250)
	slid1lbl = ttk.Label(gui, text="-/+")
	slid2lbl = ttk.Label(gui, text="Val")
	namelbl = ttk.Label(gui, text="Playing now:")
	songname = ttk.Label(gui, text="<to be played>")

	prevbut = ttk.Button(gui, text="< prev")
	nextbut = ttk.Button(gui, text="next >")
	restartbut = ttk.Button(gui, text="Restart song", width = 30)

	dirname = ttk.Entry(gui, width=50)
	loadbut = ttk.Button(gui, text="Load")
	savebut = ttk.Button(gui, text="Save")

	filelist = tk.Listbox(gui, width=50, height=30)
	slider1list = tk.Listbox(gui, width=25, height=30)
	slider2list = tk.Listbox(gui, width=25, height=30)

	###

	gui.grid(column=0, row=0)

	slider1.grid(column=0, row=0, columnspan=2)
	slider2.grid(column=0, row=1, columnspan=2)
	slid1lbl.grid(column=2, row=0)
	slid2lbl.grid(column=2, row=1)
	namelbl.grid(column=3, row=0)
	songname.grid(column=3, row=1)

	prevbut.grid(column=0, row=2)
	nextbut.grid(column=1, row=2)
	restartbut.grid(column=2, row=2, columnspan=2)

	dirname.grid(column=0, row=3, columnspan=2)
	loadbut.grid(column=2, row=3)
	savebut.grid(column=3, row=3)

	filelist.grid(column=0, row=4, columnspan=2)
	slider1list.grid(column=2, row=4)
	slider2list.grid(column=3, row=4)

	root.mainloop()


if __name__ == "__main__":
	main()