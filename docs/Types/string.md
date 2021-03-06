## class string

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class represneting an array of characters.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This string.

#### ```func __add__ (str : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the + operator to return the specified string appended to this string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to append.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new string with the value of this string appended with the string.

#### ```func __equals__ (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the == operator to determine if the specified string is equal to this string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the strings are equal, otherwise false.

#### ```func endswith (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a boolean indicating if this string ends with the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string does end with the string, otherwise false.

#### ```func format (params fargs) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Treats this string as a format string, using the given list of format args to format and return a new string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params fargs:``` The format args to format this string with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new formatted string using this string and the format args.

#### ```func __greater__ (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the > operator to determine if this string is greater than the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string is greater than the string, otherwise false.

#### ```func __greaterorequal__ (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the >= operator to determine if this string is greater than or equal to the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string is greater than or equal to the string, otherwise false.

#### ```func __index__ (index : int) : char```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the [] operator to get the char at the specified 0-based index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The 0-based index to get the char at.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The char at the index.

#### ```func indexof (str : string) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the first 0-based index of the specified string in this string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to get the index of.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The 0-based index of where the string starts in this string.

#### ```func __iter__ () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the foreach loop to return a list of chars in this string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of the chars in this string.

#### ```length { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int length of this string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The length of this string as int.

#### ```func __lesser__ (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the < operator to determine if this string is lesser than the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string is lesser than the string, otherwise false.

#### ```func __lesserorequal__ (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the <= operator to determine if this string is lesser than or equal to the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` the string to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string is lesser than or equal to the string, otherwise false.

#### ```func __modulus__ (listOrTuple : object) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the % operator to use this string as a format string with the specified list or tuple as format args.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param listOrTuple:``` The list or tuple object that will act as format args.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The formatted string.

#### ```func __notequal__ (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the != operator to determine if this string is not equal to the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string is not equal to the string, otherwise false.

#### ```func startswith (str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a boolean indicating if this string starts with the specified string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this string does start with the string, otherwise false.

#### ```func substring (start : int) : string```

#### ```func substring (start : int, len : int) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Takes the substring of this string at the specified start index and optional length.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param start:``` The 0-based start index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional len:``` The 0-based ending index.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The substring.

#### ```func tofloat () : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this string to a float value and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The float value of this string.

#### ```func toint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this string to an integer value and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The int value of this string.

#### ```func tolist () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this string to a list this string's chars.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list containing each char in the string.

#### ```func tolower () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this string to lowercase and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This string with all lowercase values.

#### ```func toupper () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this string to uppercase and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This string with all uppercase values.

