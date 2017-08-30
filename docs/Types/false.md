## class false

#### ```func __equals__ (b : bool) : bool```


```@desc:``` Implements the == operator to determine if this bool is equal to the specified bool.

	```@param b:``` The bool to compare.
```@returns:``` true if the bools are equal, otherwise false.

#### ```func __logicaland__ (b : bool) : bool```


```@desc:``` Implements the && operator to determine if both this bool and the specified bool are true.

	```@param b:``` The second bool to check.
```@returns:``` true if both bools are true, otherwise false.

#### ```func __logicalnot__ () : bool```


```@desc:``` Implements the ! operator to return the boolean opposite of this bool.

```@returns:``` true if this bool is false, otherwise false.

#### ```func __logicalor__ (b : bool) : bool```


```@desc:``` Implements the || operator to determine if either this bool or the specified bool are true.

	```@param b:``` The second bool to check.
```@returns:``` true if either this bool or the other is true, otherwise false.

#### ```func __notequal__ (b : bool) : bool```


```@desc:``` Implements the != operator to determine if this bool is not equal to the specified bool.

	```@param b:``` The bool to compare to.
```@returns:``` true if the bools are not equal, otherwise false.

#### ```func tobool () : bool```


```@desc:``` Returns this bool.

```@returns:``` This bool.

#### ```func toint () : int```


```@desc:``` Returns the integer value of this bool.

```@returns:``` 1 if this is true, otherwise 0.

#### ```func tostring () : string```


```@desc:``` Returns the string value of this bool.

```@returns:``` 'true' if this is true, otherwise 'false'.

