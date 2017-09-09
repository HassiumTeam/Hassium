## class KeyNotFoundException

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing an exception that is thrown when a key is not found in an object.

#### ```func new (obj : object, key : object) : KeyNotFoundException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new KeyNotFoundException object using the specified object and key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object that the key was not found in.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key that was not found in the object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new KeyNotFoundException object.

#### ```key { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly object key that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The key that was not found as object.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

