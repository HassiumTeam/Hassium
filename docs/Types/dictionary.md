## class dictionary

#### ```func add (key : object, val : object) : null```


```@desc:``` Adds the given value to the dictionary under the specified key.

	```@param key:``` The key for the entry.
	```@param value:``` The value for the key.
```@returns:``` null.

#### ```func containskey (key : object) : bool```


```@desc:``` Returns a boolean indicating if the specified key is present in the dictionary.

	```@param key:``` The key to check.
```@returns:``` true if the key is present, otherwise false.

#### ```func containsvalue (val : object) : bool```


```@desc:``` Returns a boolean indicating if the specified value is present in the dictionary.

	```@param val:``` The value to check.
```@returns:``` true if the value is present, otherwise false.

#### ```func __index__ (key : object) : object```


```@desc:``` Implements the [] operator by retrieving the value at the specified key.

	```@param key:``` The key for the value to get.
```@returns:``` The value at key.

#### ```func __iter__ () : list```


```@desc:``` Implements the foreach loop, returning a list of tuples in the format (key, value) ...

```@returns:``` A list of (key, value) tuples.

#### ```func keybyvalue (val : object) : object```


```@desc:``` Gets the first key that owns the specified value.

	```@param val:``` The value to get the key by.
```@returns:``` The key that owns value.

#### ```func __storeindex__ (key : object, val : object) : object```


```@desc:``` Implements the []= operator, storing the specified object at the specified key.

	```@param key:``` The key to store.
```@returns:``` l The value to store under key.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the dictionary in format { <key> : <value>, ... }

```@returns:``` The string value of this dictionary.

#### ```func valuebykey (key : object) : object```


```@desc:``` Gets the value for the specified key.

	```@param key:``` The key for the value to get.
```@returns:``` The value at key.

