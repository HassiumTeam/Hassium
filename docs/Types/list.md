## class list

#### ```func add (obj : object) : null```


```@desc:``` Adds the specified object to this list.
```    @param: obj :``` The object to add.
```@returns:``` null.

#### ```func contains (obj : object) : bool```


```@desc:``` Returns a boolean indicating if the specified object was found in this list.
```    @param: obj :``` The object to check.
```@returns:``` true if the object was found in the list, otherwise false.

#### ```func __equals__ (l : list) : bool```


```@desc:``` Implements the == operator to determine if the specified list is equal to this list.
```    @param: l :``` The list to compare.
```@returns:``` true if the lists are equal, otherwise false.

#### ```func fill (count : int, val : object) : list```


```@desc:``` Returns a new list filled with the specified count of specified objects.
```    @param: count :``` The amount of objects the new list should contain.
```    @param: obj :``` The object to fill the list with.
```@returns:``` The new filled list.

#### ```func format (fmt : string) : string```


```@desc:``` Formats this list using the specified format string and returns the string result.
```    @param: fmt :``` The format string.
```@returns:``` This list formatted as string.

#### ```func __index__ (i : int) : object```


```@desc:``` Implements the [] operator to return the object at the specified 0-based index.
```    @param: i :``` The 0-based index to get the object at.
```@returns:``` The object at the index.

#### ```func __iter__ () : list```


```@desc:``` Implements the foreach loop, returning this list.
```@returns:``` This list.

#### ```func __iterfull__ () : bool```


```@desc:``` Implements the foreach loop, returning a boolean indicating if the loop has reached the end of the list.
```@returns:``` true if the loop is at the end of the list, otherwise false.

#### ```func __iternext__ () : bool```


```@desc:``` Implements the foreach loop, returning the next iterable object.
```@returns:``` The next iterable object in this list.

#### ```length { get; }```


```@desc:``` Gets the readonly int representing the amount of elements in this list.
```@returns:``` The amount of elements in the list as int.

#### ```func __notequal__ (l : list) : bool```


```@desc:``` Implements the != operator to determine if this list is not equal to the specified list.
```    @param: l :``` The list to compare.
```@returns:``` true if the lists are not equal, otherwise false.

#### ```func peek () : object```


```@desc:``` Returns the top value in this list.
```@returns:``` The top value in this list.

#### ```func pop () : object```


```@desc:``` Removes the top value from this list and returns it.
```@returns:``` The top value in this list.

#### ```func push (obj : object) : null```


```@desc:``` Adds the specified object to the top of the list (same as list.add).
```    @param: obj :``` The object to add.
```@returns:``` null.

#### ```func remove (obj : object) : null```


```@desc:``` Removes the specified object from this list.
```    @param: obj :``` The object to remove.
```@returns:``` null.

#### ```func removeat (index : int) : null```


```@desc:``` Removes the object at the specified 0-based index.
```    @param: index :``` The 0-based index to remove at.
```@returns:``` nuil.

#### ```func reverse () : list```


```@desc:``` Returns a new list with the values in this list reversed.
```@returns:``` A new list containing the elements from this list in reverse order.

#### ```func __storeindex__ (index : int, obj : object) : object```


```@desc:``` Implements the []= operator, storing the specified object at the specified 0-based index.
```    @param: index :``` The 0-based index to store at.
```    @param: obj :``` The object to store at the index.
```@returns:``` The object that was passed.

#### ```func toascii () : string```


```@desc:``` Converts this list to an ASCII string and returns it.
```@returns:``` The ASCII string value of this list.

#### ```func tobytearr () : list```


```@desc:``` Converts this list to a more optimized form of list called bytearr.
```@returns:``` This list as an optimized byte arr.

#### ```func tohex () : string```


```@desc:``` Converts this list to a hex string and returns it.
```@returns:``` The hex value of this string.

#### ```func tolist () : list```


```@desc:``` Returns this list.
```@returns:``` This list.

#### ```func tostring () : string```


```@desc:``` Returns this list as a string formatted as [ val1, val2, ... ]
```@returns:``` The string value of this list.

