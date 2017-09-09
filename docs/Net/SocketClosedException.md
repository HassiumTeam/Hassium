## class SocketClosedException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` 

#### ```func new (sock : Socket) : SocketClosedException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new SocketClosedException using the specified Net.Socket object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The Net.Socket object that has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new SocketClosedException object.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```socket { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly Net.Socket object that has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The closed Net.Socket object.

#### ```func toString () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

