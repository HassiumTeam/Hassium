### ```func format (formatStr : string, params objects) : string```
Returns a new string formatted based on the format string and the object arguments given.

### ```func getAttribute (obj : object, attrib : string) : object```
Returns the specified attribute value in the specified object.

### ```func getAttributes (obj : object) : dictionary```
Returns a new dictionary of all the attributes in the specified object.

### ```func hasAttribute (obj : object, attrib : string) : bool```
Returns a bool indicating if the specified attribute exists in the specified object.

### ```func input () : string```
Returns a line from standard input.

### ```func map (l : list, f : func) : list```
Loops through each item in list, and calls the specified func on the element, adding it to a list to get returned.

### ```func print (params strings) : null```
Writes the string to the console.

### ```func printf (formatString : string, params objects)```
Formats the given string and objects and writes it to the console.

### ```func println (params strings) : null```
Writes the string to the console followed by a newline.

### ```func range (count : int) : list```
Returns a new list with all the numbers between 0 and count.

### ```func range (start : int, end : int) : list```
Returns a new list with all the numbers betweem start and end.

### ```func readChar () : char```
Reads a single char from the input stream and returns it.

### ```func readKey () : char```
Reads a key from the input stream and returns the keychar.

### ```func removeAttribute (obj : object, attrib : string) : object```
Removes the specified attribute from the specified object and returns the given object.

### ```func setAttribute (obj : object, attrib : string, value : object) : object```
Sets the specified attribute to the specified value for the specified object and returns the given object.

### ```func sleep (milliseconds : int) : null```
Sleeps the calling thread for the specified amount of milliseconds.

### ```func type (obj : object) : TypeDefinition```
Returns the type for the object given.

### ```func types (obj : object) : list```
Returns a list of TypeDefinitions foreach type in the given object.
