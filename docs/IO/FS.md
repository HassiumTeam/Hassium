## class FS

#### ```func close (file : File) : null```


```@desc:``` Closes the given IO.File object.

&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The IO.File object to close.
```@returns:``` null.

#### ```func copy (src : string, dest : string) : null```


```@desc:``` Copies the file at the specified source path to the specified destination path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param src:``` The source file path to copy.
&nbsp;&nbsp;&nbsp;&nbsp;```@param dest:``` The destination file path to be copied to.
```@returns:``` null.

#### ```func createdir (path : string) : null```


```@desc:``` Creates a directory at the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the directory to be created.
```@returns:``` ull.

#### ```func createfile (path : string) : null```


```@desc:``` Creates a file at the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the file to be created.
```@returns:``` null.

#### ```cwd { get; }```


```@desc:``` Gets the mutable string of the current working directory.

```@returns:``` The current working directory as string.

#### ```cwd { set; }```


```@desc:``` Gets the mutable string of the current working directory.

```@returns:``` The current working directory as string.

#### ```func delete (path : string) : null```


```@desc:``` Deltes the file or directory at the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path string to delete.
```@returns:``` null.

#### ```func deletedir (dir : string) : null```


```@desc:``` Deletes the directory at the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path string to delete.
```@returns:``` null.

#### ```func deletefile (file : string) : null```


```@desc:``` Deletes the file at the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path string to delete.
```@returns:``` null.

#### ```func direxists (dir : string) : bool```


```@desc:``` Returns a bool indicating if the specified directory path string exists.

&nbsp;&nbsp;&nbsp;&nbsp;```@param dir:``` The path string to check.
```@returns:``` true if the directory exists, otherwise false.

#### ```func fileexists (file : string) : bool```


```@desc:``` Returns a bool indicating if the specicified file path string exist.

&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The path string to check.
```@returns:``` true if the file exists, otherwise false.

#### ```func gettempfile () : string```


```@desc:``` Returns a new random temporary file path.

```@returns:``` The random file path string.

#### ```func gettemppath () : string```


```@desc:``` Returns a new random temporary directory path.

```@returns:``` The random path string.

#### ```func listdirs (path : string) : list```


```@desc:``` Returns a list of all of the directories contained within the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to get directories from.
```@returns:``` The list of directories.

#### ```func listfiles (path : string) : list```


```@desc:``` Returns a list of all of the files contained within the specified path string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to get directories from.
```@returns:``` The list of files.

#### ```func move (src : string, dest : string) : null```


```@desc:``` Moves the file at the specified source path to the specified destination path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param src:``` The source file path to move.
&nbsp;&nbsp;&nbsp;&nbsp;```@param dest:``` The destination file path to be moved to.
```@returns:``` null.

#### ```func open (path : string) : File```


```@desc:``` Opens a new file stream to the specified path and returns a new IO.File object.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to open.
```@returns:``` The new IO.File object.

#### ```func readbytes (path : string) : list```


```@desc:``` Reads the bytes of the file at the specified path as a list and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to read from.
```@returns:``` The list of file bytes.

#### ```func readlines (path : string) : list```


```@desc:``` Reads the lines of a file at the specified path as a list and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to read from.
```@returns:``` The list of lines of the file.

#### ```func readstring (path : string) : string```


```@desc:``` Reads the specified file path as a string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to read from.
```@returns:``` The file as a string.

#### ```func writebytes (path : string, bytes : list) : null```


```@desc:``` Writes the given list of bytes to the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to write to.
&nbsp;&nbsp;&nbsp;&nbsp;```@param bytes:``` The list of bytes to write.
```@returns:``` null.

#### ```func writelines (path : string, lines : list) : null```


```@desc:``` Writes the given list of string lines to the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to write to.
&nbsp;&nbsp;&nbsp;&nbsp;```@param lines:``` The list of string lines to write.
```@returns:``` null.

#### ```func writestring (path : string, str : string) : null```


```@desc:``` Writes the given string as the contents for the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to write to.
&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to write.
```@returns:``` null.

