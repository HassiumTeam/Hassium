## class StringBuilder

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` 

#### ```func new () : StringBuilder```

#### ```func new (obj : object) : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new StringBuilder object, optionally using the specified obj.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional obj:``` The object whose string value to use.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new StringBuilder object.

#### ```func append (obj : object) : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Appends the given object's string value to the string builder.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to append.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This current instance of StringBuilder.

#### ```func appendf (fmt : string, params obj) : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Appends the result of formatting the specified format string with the given format args.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param fmt:``` The format string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params obj:``` The format args.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This current instance of StringBuilder.

#### ```func appendline (obj : object) : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Appends the given object's string value to the string builder, followed by a newline.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to append.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This current instance of StringBuilder.

#### ```func clear () : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Clears the string builder of all data.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This current instance of StringBuilder.

#### ```func insert (index : int, obj : object) : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Inserts the string value of the given object to the specified index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The 0-based index to insert at.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to insert.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This current instance of StringBuilder.

#### ```length { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int length of the string builder.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The length of the string builder as int.

#### ```func replace (obj1 : object, obj2 : object) : StringBuilder```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Replaces the specified obj1 with the specified obj2.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj1:``` The object to replace.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj2:``` The object to replace with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This current instance of StringBuilder.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the values inside the string builder.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The value of the string builder as string.

