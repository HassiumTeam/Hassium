## class string

#### ```func tostring () : string```


```@desc:``` Returns this string.

```@returns:``` This string.

#### ```func __add__ (str : string) : string```


```@desc:``` Implements the + operator to return the specified string appended to this string.

```	@param: str:``` The string to append.
```@returns:``` A new string with the value of this string appended with the string.

#### ```func __equals__ (str : string) : bool```


```@desc:``` Implements the == operator to determine if the specified string is equal to this string.

```	@param: str:``` The string to compare.
```@returns:``` true if the strings are equal, otherwise false.

#### ```func endswith (str : string) : bool```


```@desc:``` Returns a boolean indicating if this string ends with the specified string.

```	@param: str:``` The string to check.
```@returns:``` true if this string does end with the string, otherwise false.

#### ```func format (params fargs) : string```


```@desc:``` Treats this string as a format string, using the given list of format args to format and return a new string.

```	@optional: params fargs:``` The format args to format this string with.
```@returns:``` A new formatted string using this string and the format args.

#### ```func __greater__ (str : string) : bool```


```@desc:``` Implements the > operator to determine if this string is greater than the specified string.

```	@param: str:``` The string to compare.
```@returns:``` true if this string is greater than the string, otherwise false.

#### ```func __greaterorequal__ (str : string) : bool```


```@desc:``` Implements the >= operator to determine if this string is greater than or equal to the specified string.

```	@param: str:``` The string to compare.
```@returns:``` true if this string is greater than or equal to the string, otherwise false.

#### ```func __index__ (index : int) : char```


```@desc:``` Implements the [] operator to get the char at the specified 0-based index.

```	@param: index:``` The 0-based index to get the char at.
```@returns:``` The char at the index.

#### ```func indexof (str : string) : int```


```@desc:``` Returns the first 0-based index of the specified string in this string.

```	@param: str:``` The string to get the index of.
```@returns:``` The 0-based index of where the string starts in this string.

#### ```func __iter__ () : list```


```@desc:``` Implements the foreach loop to return a list of chars in this string.

```@returns:``` A new list of the chars in this string.

#### ```length { get; }```


```@desc:``` Gets the readonly int length of this string.

```@returns:``` The length of this string as int.

#### ```func __lesser__ (str : string) : bool```


```@desc:``` Implements the < operator to determine if this string is lesser than the specified string.

```	@param: str:``` The string to compare.
```@returns:``` true if this string is lesser than the string, otherwise false.

#### ```func __lesserorequal__ (str : string) : bool```


```@desc:``` Implements the <= operator to determine if this string is lesser than or equal to the specified string.

```	@param: str:``` the string to compare.
```@returns:``` true if this string is lesser than or equal to the string, otherwise false.

#### ```func __notequal__ (str : string) : bool```


```@desc:``` Implements the != operator to determine if this string is not equal to the specified string.

```	@param: str:``` The string to compare.
```@returns:``` true if this string is not equal to the string, otherwise false.

#### ```func startswith (str : string) : bool```


```@desc:``` Returns a boolean indicating if this string starts with the specified string.

```	@param: str:``` The string to check.
```@returns:``` true if this string does start with the string, otherwise false.

#### ```func substring (start : int) : string```


```@desc:``` Takes the substring of this string at the specified start index and optional length.

```	@param: start:``` The 0-based start index.
```	@optional: len:``` The 0-based ending index.
```@returns:``` The substring.

#### ```func substring (start : int, len : int) : string```


```@desc:``` Takes the substring of this string at the specified start index and optional length.

```	@param: start:``` The 0-based start index.
```	@optional: len:``` The 0-based ending index.
```@returns:``` The substring.

#### ```func tofloat () : float```


```@desc:``` Converts this string to a float value and returns it.

```@returns:``` The float value of this string.

#### ```func toint () : int```


```@desc:``` Converts this string to an integer value and returns it.

```@returns:``` The int value of this string.

#### ```func tolist () : list```


```@desc:``` Converts this string to a list this string's chars.

```@returns:``` A list containing each char in the string.

#### ```func tolower () : string```


```@desc:``` Converts this string to lowercase and returns it.

```@returns:``` This string with all lowercase values.

#### ```func toupper () : string```


```@desc:``` Converts this string to uppercase and returns it.

```@returns:``` This string with all uppercase values.

