## class tuple

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a fixed length non-mutable list of objects.

#### ```func __index__ (index : int) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the [] operator to return the value at the 0-based index.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The object at the index.

#### ```func __iter__ () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the foreach loop by returning a new list of the values in the tuple.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list containing the values inside the tuple.

#### ```length { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int that represents the amount of elements in this tuple.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The number of values in this tuple as int.

#### ```func tolist () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new list containing the elements inside this tuple.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list with the elements in this tuple.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns this tuple as a string formatted as ( val1, val2, ... )

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of this list.

