## class SocketClosedException

#### ```func new (sock : Socket) : SocketClosedException```


```@desc:``` Constructs a new SocketClosedException using the specified Net.Socket object.

&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The Net.Socket object that has been closed.

```@returns:``` The new SocketClosedException object.

#### ```message { get; }```


```@desc:``` Gets the readonly string message of the exception.

```@returns:``` The exception message string.

#### ```socket { get; }```


```@desc:``` Gets the readonly Net.Socket object that has been closed.

```@returns:``` The closed Net.Socket object.

#### ```func toString () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

