## class ColorNotFoundException

#### ```color { get; }```


```@desc:``` Gets the readonly string color that was not found.

```@returns:``` The not found color as string.

#### ```func new (col : string) : ColorNotFoundException```


```@desc:``` onstructs a new ColorNotFoundException using the specified color string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param col:``` The string name of the color that was not found.
```@returns:``` The new ColorNotFoundException object.

#### ```message { get; }```


```@desc:``` Gets the readonly string message of the exception.

```@returns:``` The exception message string.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

