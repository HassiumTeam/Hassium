## ```class Socket```

### ```func new (addressFamily : string, socketType : string, protocolType : string)```
Creates a new instance of the Socket class using the specified address family, socket type, and protocol type.

### ```available { get { return this.available; } }```
Returns a bool indicating if the socket is available.

### ```blocking { get { return this.blocking; } set { this.blocking = value; } }```
Returns a bool indicating if the socket is blocking, sets blocking mode on the socket.

### ```connected { get { return this.connected; } }```
Returns a bool indicating if the socket is connected.

### ```func close () : null```
Closes the socket.

### ```func connect (ip : string, port : int) : null```
Connects the socket to the specified ip and port.

### ```func listen (port : int) : null```
Tells the socket to listen on the specified port.

### ```noDelay { get { return this.noDelay; } set { this.noDelay = value; } }```
Returns a bool indicating if the socket noDelay mode was on, sets noDelay mode to the value.

### ```func send (data : list) : int```
Writes the bytes in the specified list to the socket and returns the status code.

### ```func sendFile (path : string) : null```
Sends the specified file to the socket.
