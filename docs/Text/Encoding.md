## ```class Encoding```

### ```func new (name : string)```
Creates a new instance of the Encoding class using the specified encoding name.

### ```bodyName { get { return this.bodyName; } }```
Returns the body name of the Encoding.

### ```encodingName { get { return this.encodingName; } }```
Returns the encoding name.

### ```func getBytes (str : string) : list```
Returns a list of bytes as chars made up of the specified string, using the encoding.

### ```func getString (data : list) : string```
Returns a string of all the bytes inside of the given data list, using the encoding.

### ```headerName { get { return tihs.headerName; } }```
Returns the header name of the encoding.
