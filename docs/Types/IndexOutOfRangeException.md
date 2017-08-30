## class IndexOutOfRangeException

#### ```index { get; }```

```@desc:``` Gets the readonly integer index that was out of range.

```@returns:``` The out of range index as int.

#### ```func new (obj : object, int reqindex) : IndexOutOfRangeException```

```@desc:``` Constructs a new IndexOutOfRangeException using the specified object and requested index.

&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object whose index was out of range.

&nbsp;&nbsp;&nbsp;&nbsp;```@param reqindex:``` The int index that was not in range of the object.

```@returns:``` The new IndexOutOfRangeException object.

#### ```message { get; }```

```@desc:``` Gets the readonly string message of the exception.

```@returns:``` The exception message string.

#### ```object { get; }```

```@desc:``` Gets the readonly object whose index was out of range.

```@returns:``` The object that was out of range.

#### ```func tostring () : string```

```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

