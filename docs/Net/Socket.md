## class Socket

#### ```autoflush { get; }```


```@desc:``` Gets the mutable bool indicating if the socket will autoflush.

```@returns:``` True if the stream will automatically flush, otherwise false.

#### ```autofluah { set; }```


```@desc:``` Gets the mutable bool indicating if the socket will autoflush.

```@returns:``` True if the stream will automatically flush, otherwise false.

#### ```func close () : null```


```@desc:``` Closes the socket.

```@returns:``` null.

#### ```func connect (ip : IPAddr) : null```


```@desc:``` Connects the socket to either the specified Net.IPAddr object or the specified string ip and int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The port to connect to.

```@returns:``` null.

#### ```func connect (ip : string, port : int) : null```


```@desc:``` Connects the socket to either the specified Net.IPAddr object or the specified string ip and int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address to connect to.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The port to connect to.

```@returns:``` null.

#### ```fromip { get; }```


```@desc:``` Gets the readonly Net.IPAddr of the ip the socket is connecting from (local ip).

```@returns:``` The Net.IPAddr object of the from address.

#### ```func flush () : null```


```@desc:``` Flushes the socket stream.

```@returns:``` null.

#### ```func new () : Socket```


```@desc:``` Constructs a new Socket object with either no parameters, a Net.IPAddr object, a string ip and int port, or a string ip int port and bool ssl.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object that has the ip and port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ssl:``` The bool indicating if the Socket will use ssl.

```@returns:``` The new Socket object.

#### ```func new (ip : IPAddr) : Socket```


```@desc:``` Constructs a new Socket object with either no parameters, a Net.IPAddr object, a string ip and int port, or a string ip int port and bool ssl.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object that has the ip and port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ssl:``` The bool indicating if the Socket will use ssl.

```@returns:``` The new Socket object.

#### ```func new (ip : string, port : int) : Socket```


```@desc:``` Constructs a new Socket object with either no parameters, a Net.IPAddr object, a string ip and int port, or a string ip int port and bool ssl.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object that has the ip and port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ssl:``` The bool indicating if the Socket will use ssl.

```@returns:``` The new Socket object.

#### ```func new (ip : string, port : int, ssl : bool) : Socket```


```@desc:``` Constructs a new Socket object with either no parameters, a Net.IPAddr object, a string ip and int port, or a string ip int port and bool ssl.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The Net.IPAddr object that has the ip and port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ssl:``` The bool indicating if the Socket will use ssl.

```@returns:``` The new Socket object.

#### ```isconnected { get; }```


```@desc:``` Gets a readonly bool indicating if the socket is currently connected.

```@returns:``` true if the socket is connected, otherwise false.

#### ```func readbyte () : char```


```@desc:``` Reads a single byte from the stream and returns it as a char.

```@returns:``` The byte as char.

#### ```func readbytes (count : int) : list```


```@desc:``` Reads the specified count of bytes from the stream and returns them in a list.

&nbsp;&nbsp;&nbsp;&nbsp;```@param count:``` The amount of bytes to read.

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

#### ```toip { get; }```


```@desc:``` Gets the readonly Net.IPAddr of the ip the socket is connecting to.

```@returns:``` The Net.IPAddr object of the to address.

#### ```func writebyte (b : char) : null```


```@desc:``` Writes the given single byte to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param b:``` The char to write.

```@returns:``` null.

#### ```func writefloat (f : float) : null```


```@desc:``` Writes the given single float to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param f:``` The float to write.

```@returns:``` null.

#### ```func writeint (i : int) : null```


```@desc:``` Writes the given single 32-bit integer to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param i:``` The 32-bit int to write.

```@returns:``` null.

#### ```func writeline (str : string) : null```


```@desc:``` Writes the given string line to the file stream, followed by a newline.

&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to write.

```@returns:``` null.

#### ```func writelist (l : list) : null```


```@desc:``` Writes the byte value of each element in the given list to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The list to write.

```@returns:``` null.

#### ```func writelong (l : int) : null```


```@desc:``` Writes the given 64-bit integer to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The 64-bit int to write.

```@returns:``` null.

#### ```func writeshort (s : int) : null```


```@desc:``` Writes the given 16-bit integer to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param s:``` The 16-bit int to write.

```@returns:``` null.

#### ```func writestring (str : string) : null```


```@desc:``` Writes the given string to the file stream.

&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to write.

```@returns:``` null.

