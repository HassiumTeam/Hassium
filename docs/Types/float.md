## class float

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` @desc A class representing a double-precision floating point number.

#### ```func __add__ (num : number) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the + operator, adding this float to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to add.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float plus the number.

#### ```func __divide__ (num : number) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the / operator, dividing this float by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float divided by the number.

#### ```func __equals__ (num : number) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the == operator to determine if both numbers are equal.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if both numbers are equal, otherwise false.

#### ```func __greater__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the > operator to determine if this float is greater than the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this float is greater than the number, otherwise false.

#### ```func __greaterorequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the >= operator to determine if this float is greater than or equal to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this float is greater than or equal to the number, otherwise false.

#### ```func __intdivision__ (num : number) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the // operator to divide this float by the specified number and return the closest integer value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float divided by the number to the nearest whole int.

#### ```func new (val : object) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new float object using the specified value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new float object.

#### ```func __lesser__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the < operator to determine if this float is lesser than the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this float is lesser than the number, otherwise false.

#### ```func __lesserorequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the <= operator to determine if this float is lesser than or equal to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this float is lesser than or equal to the number, otherwise false.

#### ```func __multiply__ (num : number) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the * operator, multiplying this float by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to multiply by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float multiplied by the number.

#### ```func __negate__ () : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the unary - operator, to return this float times -1.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float multiplied by -1.

#### ```func __notequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the != operator to determine if this float is not equal to the specified float.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this float is not equal to the number, otherwise false.

#### ```func __power__ (pow : number) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the ** operator to raise this float to the specified power.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param pow:``` The power to raise to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float to the power of the number.

#### ```func __subtract__ (num : number) : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the - binary operator to subtract the specified number from this float.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to subtract.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float minux the specified number.

#### ```func tofloat () : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this float.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float.

#### ```func toint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this float to an integer and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float as int.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this float to a string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This float as string.

