## ```class FileWriter```

### ```func new (path : Stream)```
Creates a new instance of the FileWriter class using the specified Stream.
### ```func new (path : string)```
Creates a new instance of the FileWriter class on the specified file path.

### ```baseStream { get { return this.baseStream; } }```
Returns a Stream object that is the base of the FileWriter instance.

### ```endOfFile { get { return this.endOfFile; } }```
Returns a bool indicating whether or not the stream has been written to the end.

### ```flush () : null```
Flushes the stream.

### ```length { get { return this.length; } }```
Returns an integer value of how long the stream is.

### ```position { get { return this.position; } set { this.position = value; } }```
Returns an integer value of the current position in the stream, sets the current
position in the stream.

### ```func writeBool (b : bool) : null```
Writes a bool to the stream and advances the position.

### ```func writeByte (b : bool) : null```
Writes a byte to the stream and advances the position.

### ```func writeChar (c : char) : null```
Writes a char to the stream and advances the position.

### ```func writeInt16 (i : int) : null```
Writes an integer to the stream as a 16-bit and advances the position.

### ```func writeInt32 (i : int) : null```
Writes an integer to the stream as a 32-bit and advances the position.

### ```func writeInt64 (i : int) : null```
Writes an integer to the stream as a 64-bit and advances the position.

### ```func writeLine (line : string) : null```
Writes a string to the stream, followed by a line terminator and advances the position.

### ```func writeString (str : string) : null```
Writes a string to the stream and advances the position.
