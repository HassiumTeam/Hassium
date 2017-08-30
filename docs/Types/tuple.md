## class tuple

#### ```func __index__ (index : int) : object```

```@desc:``` Implements the [] operator to return the value at the 0-based index.

```@returns:``` dex The 0-based index to get.

#### ```func __iter__ () : list```

```@desc:``` Implements the foreach loop by returning a new list of the values in the tuple.

```@returns:``` A new list containing the values inside the tuple.

#### ```length { get; }```

```@desc:``` Gets the readonly int that represents the amount of elements in this tuple.

```@returns:``` The number of values in this tuple as int.

#### ```func tostring () : string```

```@desc:``` Returns this tuple as a string formatted as ( val1, val2, ... )

```@returns:``` The string value of this list.

