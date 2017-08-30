## class FileClosedException

#### ```file { get; }```


```@desc:``` Gets the readonly IO.File object that has been closed.
```@returns:``` The closed IO.File object.

#### ```filepath { get; }```


```@desc:``` Gets the readonly string filepath that has been closed.
```@returns:``` The closed string filepath.

#### ```func new (file : File, path : string) : FileClosedException```


```@desc:``` Constructs a new FileClosedException using the specified IO.File object and string path.
```    @param: file :``` The IO.File object that has been closed.
```    @param: path :``` The path of the file that has been closed.
```@returns:``` The new FileClosedException object.

#### ```message { get; }```


```@desc:``` Gets the readonly string message of the exception.
```@returns:``` The exception message string.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.
```@returns:``` The string value of the exception.

