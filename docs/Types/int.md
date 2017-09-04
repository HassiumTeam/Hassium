## class int

#### ```func __add__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the + operator, adding this int to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to add.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int plus the number.

#### ```func __bitshiftleft__ (num : number) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the << operator, shifting the bits in this int by the specified number of positions.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to shift by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int with the bits shifted left.

#### ```func __bitshiftright__ (num : number) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the >> oerator, shifting the bits in this int by the specified number of positions.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to shift by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int with the bits shifted right.

#### ```func __bitwiseand__ (num : number) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the & operator to perform a bitwise and operation on this int with the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to perform the and by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int containing only the bits that were 1 between both numbers.

#### ```func __bitwisenot__ () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the ~ operator to perform a bitwise not operation on this int.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int with all of the bytes in this int flipped.

#### ```func __bitwiseor__ (i : int) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the | operator to perform a bitwise or operation on this int using the specified int.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param i:``` The int to perform the or by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int containing bits that were 1 in either of the ints.

#### ```func __divide__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the / operator to divide this int by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int divided by the number.

#### ```func __equals__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the == operator to determine if this int is equal to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the numbers are equal, otherwise false.

#### ```func getbit (index : int) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the bit at the specified 0-based index in this int and returns it's value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The 0-based index to get the bit at.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the bit is 0, otherwise false.

#### ```func __greater__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the > operator to determine if this int is greater than the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this int is greater than the number, otherwise false.

#### ```func __greaterorequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the >= operator to determine if this int is greater than or equal to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this int is greater than or equal to the number, otherwise false.

#### ```func __intdivision__ (num : number) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the // operator to divide this int by the specified number and return the closest integer value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int divided by the number to the nearest whole int.

#### ```func new (val : object) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new int object using the specified value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new int object.

#### ```func __lesser__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the < operator to determine if this int is lesser than the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this int is lesser than the number, otherwise false.

#### ```func __lesserorequal__ (num : number) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the <= operator to determine if this int is lesser than or equal to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this int is lesser than or equal to the number, otherwise false.

#### ```func __modulus__ (i : int) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the % operator to calculate the modulus of this int and the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to modulus by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The modulus of this int and the number.

#### ```func __multiply__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the * operator to multiply this int by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to multiply by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int times the number.

#### ```func __negate__ () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the unary - operator to return this int times -1.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` his int multiplied by -1.

#### ```func __notequal__ (i : int) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the != operator to determine if this int is not equal to the specified int.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param i:``` The int to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the ints are not equal, otherwise false.

#### ```func __power__ (pow : number) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the ** operator to raise this int to the specified power.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param pow:``` The power to raise this int to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int to the power of the number.

#### ```func setbit (index : int, val : bool) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Sets the bit at the specified index to the specicied value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The zero-based index to be set.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The bool value to set to, true for 1 and false for 0.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int with the value set.

#### ```func __subtract__ (num : number) : number```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the binary - operator to subtract the specified number from this int.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to subtract.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int minux the number.

#### ```func tochar () : char```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this int to a char and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The char value of this int.

#### ```func tofloat () : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this int to a float and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int as float.

#### ```func toint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this int.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this int to a string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This int as string.

#### ```func __xor__ (i : int) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the ^ operator to perform an xor operation on this int using the specified int.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param i:``` The int to perform the xor by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new int containing bits that were opposing in each int.

