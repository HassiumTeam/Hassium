## class dict

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` @desc A class representing a dictionary where the keys and values are objects.

#### ```func add (key : object, val : object) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Adds the given value to the dictionary under the specified key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key for the entry.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param value:``` The value for the key.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func containskey (key : object) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a boolean indicating if the specified key is present in the dictionary.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the key is present, otherwise false.

#### ```func containsvalue (val : object) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a boolean indicating if the specified value is present in the dictionary.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The value to check.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the value is present, otherwise false.

#### ```func __index__ (key : object) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the [] operator by retrieving the value at the specified key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key for the value to get.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The value at key.

#### ```func new (l : list) : dict```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new dict using the given list or given list of tuples.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The list to use.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new dict object.

#### ```func __iter__ () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the foreach loop, returning a list of tuples in the format (key, value) ...

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list of (key, value) tuples.

#### ```func keybyvalue (val : object) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the first key that owns the specified value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The value to get the key by.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The key that owns value.

#### ```func __storeindex__ (key : object, val : object) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Implements the []= operator, storing the specified object at the specified key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key to store.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` l The value to store under key.

#### ```func tolist () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Converts this dictionary to a list of tuples and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of tuples in (key, value) format.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the dictionary in format { <key> : <value>, ... }

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of this dictionary.

#### ```func valuebykey (key : object) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the value for the specified key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The key for the value to get.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The value at key.

