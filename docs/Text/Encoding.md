## class Encoding

#### ```bodyname { get; }```


```@desc:``` Gets the readonly string body name of this encoding.

```@returns:``` The body name as string.

#### ```encodingname { get; }```


```@desc:``` Gets the readonly string encoding name of this encoding.

```@returns:``` The encoding name as string.

#### ```func getbytes (str : string) : list```


```@desc:``` Converts the specified string into a list of bytes using this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to convert.
```@returns:``` The bytes of str as a list.

#### ```func getstring (bytes : list) : string```


```@desc:``` Converts the given list of bytes into a string using this encoding.

&nbsp;&nbsp;&nbsp;&nbsp;```@param bytes:``` The list of bytes to convert.
```@returns:``` The string value of the bytes.

#### ```headername { get; }```


```@desc:``` Gets the readonly string header name of this encoding.

```@returns:``` The header name as string.

#### ```func new (scheme : string) : Encoding```


```@desc:``` Constructs a new Encoding object using the specified encoding scheme.

&nbsp;&nbsp;&nbsp;&nbsp;```@param scheme:``` The string name of the scheme to use. UNICODE, UTF7, UTF8, UTF32 or ASCII.
```@returns:``` The new Encoding object.

