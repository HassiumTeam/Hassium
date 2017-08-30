## class int

#### ```func __add__ (num : number) : number```


```@desc:``` Implements the + operator, adding this int to the specified number.

```    @param: num :``` The number to add.

```@returns:``` This int plus the number.

#### ```func __bitshiftleft__ (num : number) : int```


```@desc:``` Implements the << operator, shifting the bits in this int by the specified number of positions.

```    @param: num :``` The number to shift by.

```@returns:``` A new int with the bits shifted left.

#### ```func __bitshiftright__ (num : number) : int```


```@desc:``` Implements the >> oerator, shifting the bits in this int by the specified number of positions.

```    @param: num :``` The number to shift by.

```@returns:``` A new int with the bits shifted right.

#### ```func __bitwiseand__ (num : number) : int```


```@desc:``` Implements the & operator to perform a bitwise and operation on this int with the specified number.

```    @param: num :``` The number to perform the and by.

```@returns:``` A new int containing only the bits that were 1 between both numbers.

#### ```func __bitwisenot__ () : int```


```@desc:``` Implements the ~ operator to perform a bitwise not operation on this int.

```@returns:``` A new int with all of the bytes in this int flipped.

#### ```func __bitwiseor__ (i : int) : int```


```@desc:``` Implements the | operator to perform a bitwise or operation on this int using the specified int.

```    @param: i :``` The int to perform the or by.

```@returns:``` A new int containing bits that were 1 in either of the ints.

#### ```func __divide__ (num : number) : number```


```@desc:``` Implements the / operator to divide this int by the specified number.

```    @param: num :``` The number to divide by.

```@returns:``` This int divided by the number.

#### ```func __equals__ (num : number) : bool```


```@desc:``` Implements the == operator to determine if this int is equal to the specified number.

```    @param: num :``` The number to compare.

```@returns:``` true if the numbers are equal, otherwise false.

#### ```func getbit (index : int) : bool```


```@desc:``` Gets the bit at the specified 0-based index in this int and returns it's value.

```    @param: index :``` The 0-based index to get the bit at.

```@returns:``` true if the bit is 0, otherwise false.

#### ```func __greater__ (num : number) : bool```


```@desc:``` Implements the > operator to determine if this int is greater than the specified number.

```    @param: num :``` The number to compare.

```@returns:``` true if this int is greater than the number, otherwise false.

#### ```func __greaterorequal__ (num : number) : bool```


```@desc:``` Implements the >= operator to determine if this int is greater than or equal to the specified number.

```    @param: num :``` The number to compare.

```@returns:``` true if this int is greater than or equal to the number, otherwise false.

#### ```func __intdivision__ (num : number) : int```


```@desc:``` Implements the // operator to divide this int by the specified number and return the closest integer value.

```    @param: num :``` The number to divide by.

```@returns:``` This int divided by the number to the nearest whole int.

#### ```func __lesser__ (num : number) : bool```


```@desc:``` Implements the < operator to determine if this int is lesser than the specified number.

```    @param: num :``` The number to compare.

```@returns:``` true if this int is lesser than the number, otherwise false.

#### ```func __lesserorequal__ (num : number) : bool```


```@desc:``` Implements the <= operator to determine if this int is lesser than or equal to the specified number.

```    @param: num :``` The number to compare.

```@returns:``` true if this int is lesser than or equal to the number, otherwise false.

#### ```func __modulus__ (i : int) : int```


```@desc:``` Implements the % operator to calculate the modulus of this int and the specified number.

```    @param: num :``` The number to modulus by.

```@returns:``` The modulus of this int and the number.

#### ```func __multiply__ (num : number) : number```


```@desc:``` Implements the * operator to multiply this int by the specified number.

```    @param: num :``` The number to multiply by.

```@returns:``` This int times the number.

#### ```func __negate__ () : int```


```@desc:``` Implements the unary - operator to return this int times -1.

```@returns:``` his int multiplied by -1.

#### ```func __notequal__ (i : int) : bool```


```@desc:``` Implements the != operator to determine if this int is not equal to the specified int.

```    @param: i :``` The int to compare.

```@returns:``` true if the ints are not equal, otherwise false.

#### ```func __power__ (pow : number) : int```


```@desc:``` Implements the ** operator to raise this int to the specified power.

```    @param: pow :``` The power to raise this int to.

```@returns:``` This int to the power of the number.

#### ```func setbit (index : int, val : bool) : int```


```@desc:``` Sets the bit at the specified index to the specicied value.

```    @param: index :``` The zero-based index to be set.

```    @param: val :``` The bool value to set to, true for 1 and false for 0.

```@returns:``` A new int with the value set.

#### ```func __subtract__ (num : number) : number```


```@desc:``` Implements the binary - operator to subtract the specified number from this int.

```    @param: num :``` The number to subtract.

```@returns:``` This int minux the number.

#### ```func tochar () : char```


```@desc:``` Converts this int to a char and returns it.

```@returns:``` The char value of this int.

#### ```func tofloat () : float```


```@desc:``` Converts this int to a float and returns it.

```@returns:``` This int as float.

#### ```func toint () : int```


```@desc:``` Returns this int.

```@returns:``` This int.

#### ```func tostring () : string```


```@desc:``` Converts this int to a string and returns it.

```@returns:``` This int as string.

#### ```func __xor__ (i : int) : int```


```@desc:``` Implements the ^ operator to perform an xor operation on this int using the specified int.

```    @param: i :``` The int to perform the xor by.

```@returns:``` A new int containing bits that were opposing in each int.

