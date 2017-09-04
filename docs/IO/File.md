## class File

#### ```abspath { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly absolute path string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The absolute path string.

#### ```autoflush { get; }```

#### ```autoflush { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable bool indicating if the file stream will autoflush.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` True if the stream will automatically flush, otherwise false.

#### ```func close () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Closes the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func copyto (path : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Copies this file to the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The string path to be copied to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```exists { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly bool indicating if the file exists on disc.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` True if the file exists, otherwise false.

#### ```extension { get; }```

#### ```extension { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string of this file's extension.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This file's extension as string.

#### ```func flush () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Flushes this file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```isclosed { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly bool indicating if this file has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` True if the file has been closed, otherwise false.

#### ```length { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int that represents the size of the file in bytes.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The size of the file in bytes as an int.

#### ```func moveto (path : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Moves this file to the specified file path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The string path to be moved to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```name { get; }```

#### ```name { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string containing the name of the file.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The name of this file.

#### ```position { get; }```

#### ```position { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int that represents the current position in the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The current position as int.

#### ```func readallbytes () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads all of the bytes from this file and returns them in a list of chars.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list of chars representing each line of the file.

#### ```func readalllines () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads all of the lines from this file and returns them in a list of strings.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list of strings representing each line of the file.

#### ```func readalltext () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads all of the characters from this file and returns them as a single string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The file as a string.

#### ```func readbyte () : char```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a single byte from the stream and returns it as a char.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The byte as char.

#### ```func readbytes (count : int) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads the specified count of bytes from the stream and returns them in a list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param count:``` The amount of bytes to read.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list containing the specified amount of bytes.

#### ```func readint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a single 32-bit integer from the stream and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The read 32-bit int.

#### ```func readline () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a line from the stream and returns it as a string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The read line string.

#### ```func readlong () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a single 64-bit integer from the stream and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The read 64-bit int.

#### ```func readshort () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a single 16-bit integer from the stream and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The read 16-bit int.

#### ```func readstring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a single string from the stream and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The read string.

#### ```relpath{ get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly relative path of this file as a string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The relative path of the file as string.

#### ```size { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int that represents the size of the file in bytes.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The size of the file in bytes as an int.

#### ```func writeallbytes (bytes : list) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes all of the bytes in the given list to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param bytes:``` The list of bytes to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writealllines (lines : list) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes all of the string lines to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param lines:``` The list of lines to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writealltext (str : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the specified string as the file contents.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string that will become the file contents.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writebyte (b : char) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given single byte to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param b:``` The char to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writefloat (f : float) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given single float to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param f:``` The float to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writeint (i : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given single 32-bit integer to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param i:``` The 32-bit int to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writeline (str : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given string line to the file stream, followed by a newline.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writelist (l : list) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the byte value of each element in the given list to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The list to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writelong (l : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given 64-bit integer to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The 64-bit int to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writeshort (s : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given 16-bit integer to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param s:``` The 16-bit int to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func writestring (str : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the given string to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to write.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

