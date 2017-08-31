## class bool

#### ```func __equals__ (b : bool) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the == operator to determine if this bool is equal to the specified bool.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param b:``` The bool to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the bools are equal, otherwise false.

#### ```func __logicaland__ (b : bool) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the && operator to determine if both this bool and the specified bool are true.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param b:``` The second bool to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if both bools are true, otherwise false.

#### ```func __logicalnot__ () : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the ! operator to return the boolean opposite of this bool.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this bool is false, otherwise false.

#### ```func __logicalor__ (b : bool) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the || operator to determine if either this bool or the specified bool are true.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param b:``` The second bool to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if either this bool or the other is true, otherwise false.

#### ```func __notequal__ (b : bool) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the != operator to determine if this bool is not equal to the specified bool.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param b:``` The bool to compare to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the bools are not equal, otherwise false.

#### ```func tobool () : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this bool.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This bool.

#### ```func toint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the integer value of this bool.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` 1 if this is true, otherwise 0.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of this bool.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` 'true' if this is true, otherwise 'false'.

