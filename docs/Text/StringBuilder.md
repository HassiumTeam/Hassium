## ```class StringBuilder```

### ```func new ()```
Creates a new instance of the StringBuilder class.
### ```func new (str : string)```
Creates a new instance of the StringBuilder class, using the specified string as the default value.

### ```func append (str : string) : StringBuilder```
Appends the specified string to the StringBuilder, then returns the current instance.

### ```func appendFormat (formatStr : string, str1 : string, str2 : string, str3 : string ...)```
Appends the specified format string to the StringBuilder, then returns the current instance.

### ```func appendLine () : StringBuilder```
Appends a newline to the StringBuilder, then returns the current instance.
### ```func appendLine (str : string) : StringBuilder```
Appends the specified string followed by a newline to the StringBuilder, then returns the current instance.

### ```func clear () : StringBuilder```
Clears the string in the StringBuilder, then returns the current instance.

### ```func insert (index : int, str : string)```
Inserts the specified string at the specified index.

### ```length { get { return this.length; } }```
Returns the length of the string inside the StringBuilder.

### ```func replace (str1 : string, str2 : string) : StringBuilder```
Replaces str1 with str2 inside the StringBuilder, then returns the current instance.

### ```func toString () : string```
Returns the string inside of the StringBuilder.
