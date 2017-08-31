## class FileNotFoundException

#### ```func new (path : string) : FileNotFoundException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new FileNotFoundException object using the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to the file that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new FileNotFoundException.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```path { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string of the file that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The file path as a string.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

