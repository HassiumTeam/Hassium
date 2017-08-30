## class StringBuilder

#### ```func new () : StringBuilder```


```@desc:``` Constructs a new StringBuilder object, optionally using the specified obj.
```    @optional: obj:``` The object whose string value to use.
```@returns:``` The new StringBuilder object.

#### ```func new (obj : object) : StringBuilder```


```@desc:``` Constructs a new StringBuilder object, optionally using the specified obj.
```    @optional: obj:``` The object whose string value to use.
```@returns:``` The new StringBuilder object.

#### ```func append (obj : object) : StringBuilder```


```@desc:``` Appends the given object's string value to the string builder.
```    @param: obj :``` The object to append.
```@returns:``` This current instance of StringBuilder.

#### ```func appendf (fmt : string, params obj) : StringBuilder```


```@desc:``` Appends the result of formatting the specified format string with the given format args.
```    @param: fmt :``` The format string.
```    @optional: params obj:``` The format args.
```@returns:``` This current instance of StringBuilder.

#### ```func appendline (obj : object) : StringBuilder```


```@desc:``` Appends the given object's string value to the string builder, followed by a newline.
```    @param: obj :``` The object to append.
```@returns:``` This current instance of StringBuilder.

#### ```func clear () : StringBuilder```


```@desc:``` Clears the string builder of all data.
```@returns:``` This current instance of StringBuilder.

#### ```func insert (index : int, obj : object) : StringBuilder```


```@desc:``` Inserts the string value of the given object to the specified index.
```    @param: index :``` The 0-based index to insert at.
```    @param: obj :``` The object to insert.
```@returns:``` This current instance of StringBuilder.

#### ```length { get; }```


```@desc:``` Gets the readonly int length of the string builder.
```@returns:``` The length of the string builder as int.

#### ```func replace (obj1 : object, obj2 : object) : StringBuilder```


```@desc:``` Replaces the specified obj1 with the specified obj2.
```    @param: obj1 :``` The object to replace.
```    @param: obj2 :``` The object to replace with.
```@returns:``` This current instance of StringBuilder.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the values inside the string builder.
```@returns:``` The value of the string builder as string.

