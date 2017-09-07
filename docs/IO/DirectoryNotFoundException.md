## class DirectoryNotFoundException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` @desc A class representing an exception thrown when a directory is not found.

#### ```func new (path : str) : DirectoryNotFoundException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new DirectoryNotFoundException using the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the directory that is not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new DirectoryNotFoundException object.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```path { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string of the directory that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The directory path as a string.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

