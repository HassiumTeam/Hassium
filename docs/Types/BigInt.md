## class BigInt

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a variable length integer represented with a list of bytes.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this BigInt to a string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value.

#### ```func abs () : BigInt```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the absolute value of this BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The absolute value as BigInt.

#### ```func __add__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the + operator to add to the BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to add.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This BigInt plus the number.

#### ```func __divide__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the / operator to divide from the BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This BigInt divided by the number.

#### ```func __equals__ (bigint : BigInt) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the == operator to determine equality of the BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param bigint:``` The BigInt to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the BigInts are equal, otherwise false.

#### ```func __greater__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the > operator to determine if this BigInt is greater than the specified num.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this BigInt is greater than the number.

#### ```func __greaterorequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the >= operator to determine if this BigInt is greater than or equal to the specified num.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this BigInt is greater than or equal to the number.

#### ```func __lesser__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the < operator to determine if this BigInt is lesser than the specified num.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this BigInt is lesser than the number.

#### ```func __lesserorequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the <= operator to determine if this BigInt is lesser than or equal to the specified num.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this BigInt is lesser than or equal to the number.

#### ```func new (obj : object) : BigInt```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new BigInt using the specified object, which is either a float, int, or list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object that will be the BigInt, either a float, int, or list.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new BigInt object.

#### ```func log (base : float) : BigInt```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Calculates the logarithm of this BigInt to the specified base.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param base:``` The base for the logarithm.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This BigInt to the base.

#### ```func __multiply__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the * operator to multiply this BigInt by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to multiply by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This BigInt times the number.

#### ```func __notequal__ (bigint : BigInt) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the != operator to determine if this BigInt is not equal to the specified BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param bigint:``` The BigInt to compare to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the BigInts are not equal, otherwise false.

#### ```func __subtract__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the - operator to calculate this BigInt minus the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to subtract.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This BigInt minus the number.

#### ```func tofloat () : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this BigInt to a float and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The float value.

#### ```func toint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this BigInt to an integer and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The int value.

#### ```func tolist () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this BigInt to a list of bytes and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list value.

