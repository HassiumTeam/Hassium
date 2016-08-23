## ```go
class FileReader```

### ```go
func new (path : stream)```
Creates a new instance of the FileReader class using the specified stream.
### ```go
func new (path : string)```
Creates a new instance of the FileReader class on the specified file path.

### ```C#
baseStream { get { return this.baseStream; } } ```
Returns a Stream object that is the base of the FileReader instance.

### ```C# endOfFile { get { return this.endOfFile; } }```
Returns a bool indicating whether or not the stream has read to the end.

### ```C# length { get { return this.length; } }```
Returns an integer value of how long the stream can be read.

### ```C# position { get { return this.position; } set { this.position = value; } }```
Returns an integer value of the current position in the stream, sets the current
position in the stream.

### ```go func readBool () : bool```
Returns a bool from the stream and advances the position.

### ```go func readByte () : char```
Returns a byte (as a char) from the stream and advances the position.

### ```go func readChar () : char```
Returns a char from the stream and advances the position.

### ```go func readInt16 () : int```
Returns a 16 bit integer from the stream and advances the position.

### ```go func readInt32 () : int```
Returns a 32 bit integer from the stream and advances the position.

### ```go func readInt64 () : int```
Returns a 64 bit integer from the stream and advances the position.

### ```go func readLine () : string```
Returns a line from the stream and advances the position.

### ```go func readString () : string```
Returns a string from the stream and advances the position.
