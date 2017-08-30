## class IPAddr

#### ```address { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The ip address as a string.

#### ```func new (host : string) : IPaddr```

#### ```func new (host : string, port : int) : IPAddr```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new IPAddr object using the specified string host and an optional int port.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param host:``` The hostname or ip address as string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional port:``` The port for a connection.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new IPAddr object.

#### ```port { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly port number.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The port as an int.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a string representation of this IPAddr object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string ip and/or port.

