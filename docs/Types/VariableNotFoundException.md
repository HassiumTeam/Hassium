## class VariableNotFoundException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing an exception that is thrown when a variable is not found inside of the stack frame.

#### ```func new () : VariableNotFoundException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new VariableNotFoundException.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new VariableNotFoundException object.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

