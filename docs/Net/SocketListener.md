## class SocketListener

#### ```func acceptsock () : Socket```


```@desc:``` Hangs until a new connection has been received and returns a Net.Socket object of that client.

```@returns:``` The Net.Socket object that connected to this listener.

#### ```func new (portOrIPAddr : object) : SocketListener```


```@desc:``` Constructs a new SocketListener object using either the specified port, the specified Net.IPAddr, or the specified string ip and int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional portOrIPAddr:``` The int port to listen on or Net.IPAddr object.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address to listen on.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port to listen on.

```@returns:``` The new SocketListener object.

#### ```func new (ip : string, port : int) : SocketListener```


```@desc:``` Constructs a new SocketListener object using either the specified port, the specified Net.IPAddr, or the specified string ip and int port.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional portOrIPAddr:``` The int port to listen on or Net.IPAddr object.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional ip:``` The string ip address to listen on.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The int port to listen on.

```@returns:``` The new SocketListener object.

#### ```localip { get; }```


```@desc:``` Gets the readonly Net.IPAddr of the ip the listener is listening on (local ip).

```@returns:``` The Net.IPAddr object of the local address.

#### ```func start () : null```


```@desc:``` Starts the listener.

```@returns:``` null.

#### ```func stop () : null```


```@desc:``` Stops the listener.

```@returns:``` null.

