## class IPAddr

#### ```address { get; }```


```@desc:``` Gets the readonly string ip address.
```@returns:``` The ip address as a string.

#### ```func new (host : string) : IPaddr```


```@desc:``` Constructs a new IPAddr object using the specified string host and an optional int port.
```    @param: host :``` The hostname or ip address as string.
```    @optional: port:``` The port for a connection.
```@returns:``` The new IPAddr object.

#### ```func new (host : string, port : int) : IPAddr```


```@desc:``` Constructs a new IPAddr object using the specified string host and an optional int port.
```    @param: host :``` The hostname or ip address as string.
```    @optional: port:``` The port for a connection.
```@returns:``` The new IPAddr object.

#### ```port { get; }```


```@desc:``` Gets the readonly port number.
```@returns:``` The port as an int.

#### ```func tostring () : string```


```@desc:``` Returns a string representation of this IPAddr object.
```@returns:``` The string ip and/or port.

