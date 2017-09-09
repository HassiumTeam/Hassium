## class SocketListener

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` 

#### ```func acceptsock () : Socket```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Hangs until a new connection has been received and returns a Net.Socket object of that client.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The Net.Socket object that connected to this listener.

#### ```func new (portOrIPAddr : object) : SocketListener```

#### ```func new (ip : string, port : int) : SocketListener```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new SocketListener object using either the specified port, the specified Net.IPAddr, or the specified string ip and int port.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional portOrIPAddr:``` The int port to listen on or Net.IPAddr object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address to listen on.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port to listen on.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new SocketListener object.

#### ```localip { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly Net.IPAddr of the ip the listener is listening on (local ip).

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The Net.IPAddr object of the local address.

#### ```func start () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Starts the listener.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func stop () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Stops the listener.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

