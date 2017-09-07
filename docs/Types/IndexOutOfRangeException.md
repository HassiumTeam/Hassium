## class IndexOutOfRangeException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` @desc A class representing an exception that is thrown if an index is out of the range of an object.

#### ```index { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly integer index that was out of range.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The out of range index as int.

#### ```func new (obj : object, int reqindex) : IndexOutOfRangeException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new IndexOutOfRangeException using the specified object and requested index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object whose index was out of range.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param reqindex:``` The int index that was not in range of the object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new IndexOutOfRangeException object.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```object { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly object whose index was out of range.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The object that was out of range.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

