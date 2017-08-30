## class list

#### ```func add (obj : object) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Adds the specified object to this list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to add.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func contains (obj : object) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a boolean indicating if the specified object was found in this list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the object was found in the list, otherwise false.

#### ```func __equals__ (l : list) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the == operator to determine if the specified list is equal to this list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The list to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the lists are equal, otherwise false.

#### ```func fill (count : int, val : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new list filled with the specified count of specified objects.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param count:``` The amount of objects the new list should contain.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to fill the list with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new filled list.

#### ```func format (fmt : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Formats this list using the specified format string and returns the string result.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param fmt:``` The format string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This list formatted as string.

#### ```func __index__ (i : int) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the [] operator to return the object at the specified 0-based index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param i:``` The 0-based index to get the object at.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The object at the index.

#### ```func __iter__ () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the foreach loop, returning this list.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This list.

#### ```func __iterfull__ () : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the foreach loop, returning a boolean indicating if the loop has reached the end of the list.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the loop is at the end of the list, otherwise false.

#### ```func __iternext__ () : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the foreach loop, returning the next iterable object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The next iterable object in this list.

#### ```length { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int representing the amount of elements in this list.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The amount of elements in the list as int.

#### ```func __notequal__ (l : list) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the != operator to determine if this list is not equal to the specified list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The list to compare.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the lists are not equal, otherwise false.

#### ```func peek () : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the top value in this list.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The top value in this list.

#### ```func pop () : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Removes the top value from this list and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The top value in this list.

#### ```func push (obj : object) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Adds the specified object to the top of the list (same as list.add).

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to add.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func remove (obj : object) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Removes the specified object from this list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to remove.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func removeat (index : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Removes the object at the specified 0-based index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The 0-based index to remove at.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` nuil.

#### ```func reverse () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new list with the values in this list reversed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list containing the elements from this list in reverse order.

#### ```func __storeindex__ (index : int, obj : object) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the []= operator, storing the specified object at the specified 0-based index.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param index:``` The 0-based index to store at.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to store at the index.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The object that was passed.

#### ```func toascii () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this list to an ASCII string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The ASCII string value of this list.

#### ```func tobytearr () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this list to a more optimized form of list called bytearr.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This list as an optimized byte arr.

#### ```func tohex () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this list to a hex string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The hex value of this string.

#### ```func tolist () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this list.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` This list.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this list as a string formatted as [ val1, val2, ... ]

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of this list.

