## class ColorNotFoundException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing an exception that is thrown when a color is not found or cannot be resolved.

#### ```color { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string color that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The not found color as string.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

