## class Socket

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a socket connection to a server.

#### ```autoflush { get; }```

#### ```autofluah { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable bool indicating if the socket will autoflush.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` True if the stream will automatically flush, otherwise false.

#### ```func close () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Closes the socket.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func connect (ip : IPAddr) : null```

#### ```func connect (ip : string, port : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Connects the socket to either the specified Net.IPAddr object or the specified string ip and int port.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The port to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func __enter__ () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the 'enter' portion of the with statement. Does nothing.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func __exit__ () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the 'exit' portion of the with statement. Closes the socket.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```fromip { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly Net.IPAddr of the ip the socket is connecting from (local ip).

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The Net.IPAddr object of the from address.

#### ```func flush () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Flushes the socket stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func new () : Socket```

#### ```func new (ip : IPAddr) : Socket```

#### ```func new (ip : string, port : int) : Socket```

#### ```func new (ip : string, port : int, ssl : bool) : Socket```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new Socket object with either no parameters, a Net.IPAddr object, a string ip and int port, or a string ip int port and bool ssl.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object that has the ip and port.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional ssl:``` The bool indicating if the Socket will use ssl.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new Socket object.

#### ```isconnected { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a readonly bool indicating if the socket is currently connected.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the socket is connected, otherwise false.

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

#### ```toip { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly Net.IPAddr of the ip the socket is connecting to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The Net.IPAddr object of the to address.

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

