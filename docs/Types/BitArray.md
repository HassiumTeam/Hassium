## ```class BitArray```

### ```func new (capacity : int)```
Creates a new instance of the BitArray class using the specified capacity.
### ```func new (bytes : list)```
Creates a new instance of the BitArray class using the given initial bytes in the list.

### ```func and (bitArray : BitArray) : BitArray```
Preforms bitwise and using the elements in the BitArray instance and the specified BitARray, and returns the result.

### ```func get (index : int) : bool```
Returns the value of the bit at the specified index.

### ```func not () : BitArray```
Preforms bitwise not on each element in the BitArray instance and returns the result.

### ```func or (bitArray : BitArray) : BitArray```
Preforms bitwise or using the elements in the BitArray instance and the specified BitArray, and returns the result.

### ```func set (index : int, value : bool) : bool```
Sets the bit at the specified index to the specified value, and returns the value.

### ```func setAll (value : bool) : BitArray```
Sets each value in the BitArray to the specified value and returns the BitArray instance.
