### ```func format (formatStr : string, obj1 : object, obj2 : object, obj3 : object, ...) : string```
Returns a new string formatted based on the format string and the object arguments given.

### ```func input () : string```
Returns a line from standard input.

### ```func map (l : list, f : func) : list```
Loops through each item in list, and calls the specified func on the element, adding it to a list to get returned.

### ```func print (str : string) : null```
Writes the string to the console.

### ```func println (str : string) : null```
Writes the string to the console followed by a newline.

### ```func range (count : int) : list```
Returns a new list with all the numbers between 0 and count.

### ```func range (start : int, end : int) : list```
Returns a new list with all the numbers betweem start and end.

### ```func sleep (milliseconds : int) : null```
Sleeps the calling thread for the specified amount of milliseconds.

### ```func type (obj : object) : TypeDefinition```
Returns the type for the object given.

### ```func types (obj : object) : list```
Returns a list of TypeDefinitions foreach type in the given object.
