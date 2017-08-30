## class DirectoryNotFoundException

#### ```func new (path : str) : DirectoryNotFoundException```


```@desc:``` Constructs a new DirectoryNotFoundException using the specified path.
```    @param: path :``` The path of the directory that is not found.
```@returns:``` The new DirectoryNotFoundException object.

#### ```message { get; }```


```@desc:``` Gets the readonly string message of the exception.
```@returns:``` The exception message string.

#### ```path { get; }```


```@desc:``` Gets the readonly string of the directory that was not found.
```@returns:``` The directory path as a string.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.
```@returns:``` The string value of the exception.

