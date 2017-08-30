## class FileNotFoundException

#### ```func new (path : string) : FileNotFoundException```

```@desc:``` Constructs a new FileNotFoundException object using the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to the file that was not found.

```@returns:``` The new FileNotFoundException.

#### ```message { get; }```

```@desc:``` Gets the readonly string message of the exception.

```@returns:``` The exception message string.

#### ```path { get; }```

```@desc:``` Gets the readonly string of the file that was not found.

```@returns:``` The file path as a string.

#### ```func tostring () : string```

```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

