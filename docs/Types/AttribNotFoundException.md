## class AttribNotFoundException

#### ```attribute { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string attribute that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The attribute as string.

#### ```func new (obj : object, attrib : string) : AttribNotFoundException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new AttribNotFoundException using the specified object and attribute string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object the attrib was not found in.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param attrib:``` The string attrib that was not found.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new AttribNotFoundException.

#### ```message { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string message of the exception.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exception message string.

#### ```object { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly object that the attribute was not found in.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The object.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

