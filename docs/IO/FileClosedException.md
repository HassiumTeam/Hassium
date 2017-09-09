## class FileClosedException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing an exception that is thrown when an IO.File object's stream has been closed.

#### ```file { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly IO.File object that has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The closed IO.File object.

#### ```filepath { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string filepath that has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The closed string filepath.

#### ```func new (file : File, path : string) : FileClosedException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new FileClosedException using the specified IO.File object and string path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The IO.File object that has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the file that has been closed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new FileClosedException object.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

