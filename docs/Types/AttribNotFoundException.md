## class AttribNotFoundException

#### ```attribute { get; }```


```@desc:``` Gets the readonly string attribute that was not found.

```@returns:``` The attribute as string.

#### ```func new (obj : object, attrib : string) : AttribNotFoundException```


```@desc:``` Constructs a new AttribNotFoundException using the specified object and attribute string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object the attrib was not found in.

&nbsp;&nbsp;&nbsp;&nbsp;```@param attrib:``` The string attrib that was not found.

```@returns:``` The new AttribNotFoundException.

#### ```message { get; }```


```@desc:``` Gets the readonly string message of the exception.

```@returns:``` The exception message string.

#### ```object { get; }```


```@desc:``` Gets the readonly object that the attribute was not found in.

```@returns:``` The object.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

