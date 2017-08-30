## class KeyNotFoundException

#### ```func new (obj : object, key : object) : KeyNotFoundException```


```@desc:``` Constructs a new KeyNotFoundException object using the specified object and key.

&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object that the key was not found in.
&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key that was not found in the object.
```@returns:``` The new KeyNotFoundException object.

#### ```key { get; }```


```@desc:``` Gets the readonly object key that was not found.

```@returns:``` The key that was not found as object.

#### ```message { get; }```


```@desc:``` Gets the readonly string message of the exception.

```@returns:``` The exception message string.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

