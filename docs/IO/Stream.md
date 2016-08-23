## ```class Stream```

### ```length { get { return this.length; } }```
Returns the length of the stream.

### ```position { get { return this.position; } set { this.position = value; } }```
Returns the position in the stream, sets the position in the stream to the value.

### ```func authenticateSsl (server : string) : null```
Authenticates the stream to the server with Ssl.

### ```func close () : null```
Closes the stream.

### ```func flush () : null```
Flushes the stream.

### ```func readByte () : char```
Returns a byte from the stream as a char and advances the position.

### ```func readTo (count : int) : list```
Returns a list containing bytes from the stream up to the specified count.

### ```func write (bytes : list) : null```
Writes all of the bytes in the specified list to the stream.

### ```func writeByte (byte : char) : null```
Writes the char as a byte to the stream.
