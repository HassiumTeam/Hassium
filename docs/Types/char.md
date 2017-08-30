## class char

#### ```func __add__ (c : char) : char```


```@desc:``` Implements the + operator to add to this char.

&nbsp;&nbsp;&nbsp;&nbsp;```@param c:``` The char to add.
```@returns:``` This char plus the char.

#### ```func __bitshiftleft__ (num : number) : char```


```@desc:``` Implements the << operator to perform a left bitshift of this char for the specified anmount of positions.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to shift left by.
```@returns:``` The new char that has been shifted left.

#### ```func __bitshiftright__ (num : number) : char```


```@desc:``` Implements the >> operator to perform a right bitshift of this char for the specified amount of positions.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to shift right by.
```@returns:``` This new char that has been shifted right.

#### ```func __bitswiseand__ (c : char) : char```


```@desc:``` Implements the & operator to perform a bitwise and operation on this char with the specified char.

&nbsp;&nbsp;&nbsp;&nbsp;```@param c:``` The char to perform the and by.
```@returns:``` A new char containing only the bits that were 1 between both chars.

#### ```func __bitwisenot__ () : char```


```@desc:``` Implements the ~ operator to perform a bitwise not operation on this char.

```@returns:``` A new char with all of the bytes in this char flipped.

#### ```func __bitwiseor__ (c : char) : char```


```@desc:``` Implements the | operator to perform a bitwise or operation on this char using the specified char.

&nbsp;&nbsp;&nbsp;&nbsp;```@param c:``` The char to perform the or by.
```@returns:``` A new char containing bits that were 1 in either of the chars.

#### ```func __divide__ (num : number) : int```


```@desc:``` Implements the / operator, returning this char divided by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.
```@returns:``` This char divided by the number.

#### ```func __equals__ (c : char) : bool```


```@desc:``` Implements the == operator, returning a boolean indicating if both chars are equal.

&nbsp;&nbsp;&nbsp;&nbsp;```@param c:``` The char to compare.
```@returns:``` true if both chars have the same value, otherwise false.

#### ```func getbit (index : int) : bool```


```@desc:``` Gets the bit at the specified 0-based index in this char and returns it's value.

&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The 0-based index to get the bit at.
```@returns:``` true if the bit is 0, otherwise false.

#### ```func __greater__ (num : number) : bool```


```@desc:``` Implements the > operator to determine if this char is greater than the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.
```@returns:``` true if this char is greater than the specified number, otherwise false.

#### ```func __greaterorequal__ (num : number) : bool```


```@desc:``` Implements the >= operator to determine if this char is greater than or equal to the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.
```@returns:``` true if this char is greater than or equal to the specified number, otherwise false.

#### ```func __intdivision__ (num : number) : int```


```@desc:``` Implements the // operator to divide this char by the specified number and return the closest integer value.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to divide by.
```@returns:``` This char divided by the number to the nearest whole int.

#### ```func iscontrol () : bool```


```@desc:``` Returns a boolean indicating if this char is a control char.

```@returns:``` true if this is a control char, otherwise false.

#### ```func isdigit () : bool```


```@desc:``` Returns a boolean indicating if this char is a digit (0-9).

```@returns:``` true if this is a digit, otherwise false.

#### ```func isletter () : bool```


```@desc:``` Returns a boolean indicating if this char is a letter (a-z, A-Z).

```@returns:``` true if this is a letter, otherwise false.

#### ```func isletterordigit () : bool```


```@desc:``` Returns a boolean indicating if this char is a letter or digit (a-z, A-Z, 0-9).

```@returns:``` true if this is a letter or digit, otherwise false.

#### ```func islower () : bool```


```@desc:``` Returns a boolean indicating if this char is a letter that is lowercase (a-z).

```@returns:``` true if this is a lowercase letter, otherwise false.

#### ```func issymbol () : bool```


```@desc:``` Returns a boolean indicating if this char is a symbol.

```@returns:``` true if this a symbol, otherwise false.

#### ```func isupper () : bool```


```@desc:``` Returns a boolean indicating if this char is a letter that is uppercase (A-Z).

```@returns:``` true if this is an uppercase letter, otherwise false.

#### ```func iswhitespace () : bool```


```@desc:``` Returns a boolean indicating if this char is a whitespace character.

```@returns:``` true if this is whitespace, otherwise false.

#### ```func __lesser__ (num : number) : bool```


```@desc:``` Implements the < operator to determine if this char is lesser than the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to compare.
```@returns:``` true if this is lesser than the number, otherwise false.

#### ```func __lesserorequal__ (num : number) : bool```


```@desc:``` Implements the <= operator to determine if this char is lesser than or equal to the specified number.

```@returns:``` true if this is lesser than or equal to the number, otherwise false.

#### ```func __modulus__ (num : number) : int```


```@desc:``` Implements the % operator to calculate the modulus of this char and the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to modulus by.
```@returns:``` The modulus of this char and the number.

#### ```func __multiply__ (num : number) : int```


```@desc:``` Implements the * operator to multiply this char by the specified number.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to multiply by.
```@returns:``` This char multiplied by the number.

#### ```func __notequal__ (c : char) : bool```


```@desc:``` Implements the != operator to determine if this char is not equal to the specified char.

&nbsp;&nbsp;&nbsp;&nbsp;```@param c:``` The char to compare.
```@returns:``` true if the chars are not equal, otherwise false.

#### ```func setbit (index : int, val : bool) : char```


```@desc:``` Sets the bit at the specified index to the specicied value.

&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The zero-based index to be set.
&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The bool value to set to, true for 1 and false for 0.
```@returns:``` A new char with the value set.

#### ```func __subtract__ (num : number) : char```


```@desc:``` Implements the - operator to subtract the specified number from this char.

&nbsp;&nbsp;&nbsp;&nbsp;```@param num:``` The number to subtract.
```@returns:``` This char minus the number.

#### ```func tochar () : char```


```@desc:``` Returns this char.

```@returns:``` This char.

#### ```func tofloat () : float```


```@desc:``` Converts this char to a float and returns it.

```@returns:``` The float value of this char.

#### ```func toint () : int```


```@desc:``` Converts this char to an integer and returns it.

```@returns:``` The int value of this char.

#### ```func tolower () : char```


```@desc:``` Returns the lowercase value of this char.

```@returns:``` The lowercase value.

#### ```func tostring () : string```


```@desc:``` Converts this char to a string and returns it.

```@returns:``` The string value of this char.

#### ```func toupper () : char```


```@desc:``` Returns the uppercase value of this char.

```@returns:``` The uppercase value.

#### ```func __xor__ (c : char) : char```


```@desc:``` Implements the ^ operator to perform an xor operation on this char using the specified char.

&nbsp;&nbsp;&nbsp;&nbsp;```@param c:``` The char to perform the xor by.
```@returns:``` A new char containing bits that were opposing in each char.

