## ```class list```

### ```func add (obj : object) : object```
Adds the specified object to the list and returns it.

### ```func clear () : null```
Clears the list of all elements.

### ```func contains (obj : object) : bool```
Returns a bool if the list contains the specified object.

### ```func fill (amount : int) : list```
Fills the list from index 0 for the amount specified with a null value, then
returns the current list instance.

### ```length { get { return this.length; } }```
Returns the amount of elements in the list.

### ```func remove (obj : object) : object```
Removes the specified object from the list and returns it.

### ```func reverse () : list```
Returns a new list with the elements of the base list in reverse order.

### ```func toList () : list```
Returns the current list.

### ```func toString () : string```
Returns a string with all of the element strings concatenated.
### ```func toString (seperator : string) : string```
Returns a string with all of the element strings concatenated seperated by the given seperator.

### ```func toTuple () : tuple```
Returns a new tuple using the elements in the list.
