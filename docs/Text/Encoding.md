## class Encoding

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a specific string encoding scheme.

#### ```bodyname { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string body name of this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The body name as string.

#### ```encodingname { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string encoding name of this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The encoding name as string.

#### ```func getbytes (str : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts the specified string into a list of bytes using this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to convert.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The bytes of str as a list.

#### ```func getstring (bytes : list) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts the given list of bytes into a string using this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param bytes:``` The list of bytes to convert.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the bytes.

#### ```headername { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string header name of this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The header name as string.

#### ```func new (scheme : string) : Encoding```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new Encoding object using the specified encoding scheme.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param scheme:``` The string name of the scheme to use. UNICODE, UTF7, UTF8, UTF32 or ASCII.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new Encoding object.

