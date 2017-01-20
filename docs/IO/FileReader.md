## ```class FileReader```

### ```func new (path : Stream)```
Creates a new instance of the FileReader class using the specified Stream.
### ```func new (path : string)```
Creates a new instance of the FileReader class on the specified file path.

### ```baseStream { get { return this.baseStream; } } ```
Returns a Stream object that is the base of the FileReader instance.

### ```func dispose () null```
Disposes the current object.

### ```endOfFile { get { return this.endOfFile; } }```
Returns a bool indicating whether or not the stream has read to the end.

### ```length { get { return this.length; } }```
Returns an integer value of how long the stream can be read.

### ```position { get { return this.position; } set { this.position = value; } }```
Returns an integer value of the current position in the stream, sets the current
position in the stream.

### ```func readBool () : bool```
Returns a bool from the stream and advances the position.

### ```func readByte () : char```
Returns a byte (as a char) from the stream and advances the position.

### ```func readChar () : char```
Returns a char from the stream and advances the position.

### ```func readInt16 () : int```
Returns a 16 bit integer from the stream and advances the position.

### ```func readInt32 () : int```
Returns a 32 bit integer from the stream and advances the position.

### ```func readInt64 () : int```
Returns a 64 bit integer from the stream and advances the position.

### ```func readLine () : string```
Returns a line from the stream and advances the position.

### ```func readString () : string```
Returns a string from the stream and advances the position.
