## class File

#### ```abspath { get; }```


```@desc:``` Gets the readonly absolute path string.
```@returns:``` The absolute path string.

#### ```autoflush { get; }```


```@desc:``` Gets the mutable bool indicating if the file stream will autoflush.
```@returns:``` True if the stream will automatically flush, otherwise false.

#### ```autoflush { set; }```


```@desc:``` Gets the mutable bool indicating if the file stream will autoflush.
```@returns:``` True if the stream will automatically flush, otherwise false.

#### ```func close () : null```


```@desc:``` Closes the file stream.
```@returns:``` null.

#### ```func copyto (path : string) : null```


```@desc:``` Copies this file to the specified file path.
```    @param: path :``` The string path to be copied to.
```@returns:``` null.

#### ```exists { get; }```


```@desc:``` Gets the readonly bool indicating if the file exists on disc.
```@returns:``` True if the file exists, otherwise false.

#### ```extension { get; }```


```@desc:``` Gets the mutable string of this file's extension.
```@returns:``` This file's extension as string.

#### ```extension { set; }```


```@desc:``` Gets the mutable string of this file's extension.
```@returns:``` This file's extension as string.

#### ```func flush () : null```


```@desc:``` Flushes this file stream.
```@returns:``` null.

#### ```isclosed { get; }```


```@desc:``` Gets the readonly bool indicating if this file has been closed.
```@returns:``` True if the file has been closed, otherwise false.

#### ```length { get; }```


```@desc:``` Gets the readonly int that represents the size of the file in bytes.
```@returns:``` The size of the file in bytes as an int.

#### ```func moveto (path : string) : null```


```@desc:``` Moves this file to the specified file path.
```    @param: path :``` The string path to be moved to.
```@returns:``` null.

#### ```name { get; }```


```@desc:``` Gets the mutable string containing the name of the file.
```@returns:``` The name of this file.

#### ```name { get; }```


```@desc:``` Gets the mutable string containing the name of the file.
```@returns:``` The name of this file.

#### ```position { get; }```


```@desc:``` Gets the mutable int that represents the current position in the file stream.
```@returns:``` The current position as int.

#### ```position { set; }```


```@desc:``` Gets the mutable int that represents the current position in the file stream.
```@returns:``` The current position as int.

#### ```func readallbytes () : list```


```@desc:``` Reads all of the bytes from this file and returns them in a list of chars.
```@returns:``` A list of chars representing each line of the file.

#### ```func readalllines () : list```


```@desc:``` Reads all of the lines from this file and returns them in a list of strings.
```@returns:``` A list of strings representing each line of the file.

#### ```func readalltext () : string```


```@desc:``` Reads all of the characters from this file and returns them as a single string.
```@returns:``` The file as a string.

#### ```func readbyte () : char```


```@desc:``` Reads a single byte from the stream and returns it as a char.
```@returns:``` The byte as char.

#### ```func readbytes (count : int) : list```


```@desc:``` Reads the specified count of bytes from the stream and returns them in a list.
```    @param: count :``` The amount of bytes to read.
```@returns:``` A list containing the specified amount of bytes.

#### ```func readint () : int```


```@desc:``` Reads a single 32-bit integer from the stream and returns it.
```@returns:``` The read 32-bit int.

#### ```func readline () : string```


```@desc:``` Reads a line from the stream and returns it as a string.
```@returns:``` The read line string.

#### ```func readlong () : int```


```@desc:``` Reads a single 64-bit integer from the stream and returns it.
```@returns:``` The read 64-bit int.

#### ```func readshort () : int```


```@desc:``` Reads a single 16-bit integer from the stream and returns it.
```@returns:``` The read 16-bit int.

#### ```func readstring () : string```


```@desc:``` Reads a single string from the stream and returns it.
```@returns:``` The read string.

#### ```relpath{ get; }```


```@desc:``` Gets the readonly relative path of this file as a string.
```@returns:``` The relative path of the file as string.

#### ```size { get; }```


```@desc:``` Gets the readonly int that represents the size of the file in bytes.
```@returns:``` The size of the file in bytes as an int.

#### ```func writeallbytes (bytes : list) : null```


```@desc:``` Writes all of the bytes in the given list to the file stream.
```    @param: bytes :``` The list of bytes to write.
```@returns:``` null.

#### ```func writealllines (lines : list) : null```


```@desc:``` Writes all of the string lines to the file stream.
```    @param: lines :``` The list of lines to write.
```@returns:``` null.

#### ```func writealltext (str : string) : null```


```@desc:``` Writes the specified string as the file contents.
```    @param: str :``` The string that will become the file contents.
```@returns:``` null.

#### ```func writebyte (b : char) : null```


```@desc:``` Writes the given single byte to the file stream.
```    @param: b :``` The char to write.
```@returns:``` null.

#### ```func writefloat (f : float) : null```


```@desc:``` Writes the given single float to the file stream.
```    @param: f :``` The float to write.
```@returns:``` null.

#### ```func writeint (i : int) : null```


```@desc:``` Writes the given single 32-bit integer to the file stream.
```    @param: i :``` The 32-bit int to write.
```@returns:``` null.

#### ```func writeline (str : string) : null```


```@desc:``` Writes the given string line to the file stream, followed by a newline.
```    @param: str :``` The string to write.
```@returns:``` null.

#### ```func writelist (l : list) : null```


```@desc:``` Writes the byte value of each element in the given list to the file stream.
```    @param: l :``` The list to write.
```@returns:``` null.

#### ```func writelong (l : int) : null```


```@desc:``` Writes the given 64-bit integer to the file stream.
```    @param: l :``` The 64-bit int to write.
```@returns:``` null.

#### ```func writeshort (s : int) : null```


```@desc:``` Writes the given 16-bit integer to the file stream.
```    @param: s :``` The 16-bit int to write.
```@returns:``` null.

#### ```func writestring (str : string) : null```


```@desc:``` Writes the given string to the file stream.
```    @param: str :``` The string to write.
```@returns:``` null.

