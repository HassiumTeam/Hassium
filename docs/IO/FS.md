## class FS

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` 

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` 

#### ```func close (file : File) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Closes the given IO.File object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The IO.File object to close.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func copy (src : string, dest : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Copies the file at the specified source path to the specified destination path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param src:``` The source file path to copy.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param dest:``` The destination file path to be copied to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func createdir (path : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Creates a directory at the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the directory to be created.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` ull.

#### ```func createfile (path : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Creates a file at the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the file to be created.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```cwd { get; }```

#### ```cwd { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string of the current working directory.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The current working directory as string.

#### ```func delete (path : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Deltes the file or directory at the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path string to delete.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func deletedir (dir : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Deletes the directory at the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path string to delete.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func deletefile (file : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Deletes the file at the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path string to delete.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func direxists (dir : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a bool indicating if the specified directory path string exists.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param dir:``` The path string to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the directory exists, otherwise false.

#### ```func fileexists (file : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a bool indicating if the specicified file path string exist.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The path string to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the file exists, otherwise false.

#### ```func gettempfile () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new random temporary file path.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The random file path string.

#### ```func gettemppath () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new random temporary directory path.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The random path string.

#### ```func listdirs (path : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a list of all of the directories contained within the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to get directories from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of directories.

#### ```func listfiles (path : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a list of all of the files contained within the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to get directories from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of files.

#### ```func move (src : string, dest : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Moves the file at the specified source path to the specified destination path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param src:``` The source file path to move.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param dest:``` The destination file path to be moved to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func open (path : string) : File```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Opens a new file stream to the specified path and returns a new IO.File object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to open.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new IO.File object.

#### ```func readbytes (path : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads the bytes of the file at the specified path as a list and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to read from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of file bytes.

#### ```func readlines (path : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads the lines of a file at the specified path as a list and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to read from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of lines of the file.

#### ```func readstring (path : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads the specified file path as a string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to read from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The file as a string.

#### ```func writebytes (path : string, bytes : list) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given list of bytes to the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to write to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param bytes:``` The list of bytes to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writelines (path : string, lines : list) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given list of string lines to the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to write to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param lines:``` The list of string lines to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writestring (path : string, str : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given string as the contents for the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to write to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

