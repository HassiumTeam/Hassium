# Variable Type Classes
It is important to note that these are instance variables, meaning
that you would have to have a type already to use these methods.
For example:
```
myString "Hello, World!";
println(myString.toUpper());
```

## String Class

### string String.toLower()
Returns a lowercase version of the string.
```
myStr := "HELLO";
println(myStr.toLower());
```

### string String.toUpper()
Returns an uppercase version of the string.
```
myStr := "hello";
println(myStr.toUpper());
```

### bool String.begins(string prefix)
Returns true if the string starts with the speicified prefix, otherwise returns false.
```
if (args[0].begins("abc"))
	println("args[0] starts with abc");
else
	println("args[0] doesn't start with abc");
```

### bool String.ends(string sufix)
Returns true if the string ends with the speicified sufix, otherwise returns false.
```
if (args[0].ends("xyz"))
	println("args[0] ends with xyz");
else
	println("args[0] doesn't start with xyz");
```

### string String.getAt(number index)
Get's the character at the specified index and returns it.
```
myStr := "Hello, World!";
println(myStr.getAt(2));
```

### string String.substring(number startIndex, number length)
Returns a substring starting at the startIndex for the specified length.
```
myStr := "Hello, world!";
println(myStr.substring(2, 3));
```

### string String.concat(string newString)
Returns a new string that has been concatanated with the specified new string.
```
myStr := "Hello, ";
myStr := myStr.concat("World!");
println(myStr);
```

### bool String.contains(string characters)
Returns true if the string contains the specified characters, otherwise returns false.
```
if (args[0].contains("a"))
	println("The string has a");
else
	println("The string does not contain a");
```

### array String.split(string character)
Returns a new array splitting the string on the specified character.
```
myStr := "The quick brown fox jumps over the lazy dog";
words := myStr.split(" ");
foreach (word in words)
	println(word);
```

### string String.replace(string oldPart, string newPart)
Returns a new string with the old part replaced with the new part.
```
myStr := "ello world!";
myStr := myStr.replace("ello", "hello");
```

### number String.index(string character)
Returns the index number of the string at the character.
```
myStr := "Hello World";
println(myStr.index("e"));
```

### number String.lastIndex(string character)
Returns the last index number of the string at the character.
```
myStr := "Hello World";
println(myStr.lastIndex("l"));
```

### string String.padLeft(number length)
Returns a new string left-padded to the length specified.
```
myStr := "Hello World";
myStr := myStr.padLeft(20);
println(myStr.length);
```

### string String.padRight(number length)
Returns a new string right-padded to the length specified.
```
myStr := "Hello World";
myStr := myStr.padRight(20);
println(myStr.length);
```

### string String.trim()
Returns a new string with all of the whitespace characters removed.
```
myStr := "  \n \n h   \nello  world!";
myStr := myStr.trim();
```

### string String.trimLeft()
Returns a new string with all of the left-hand whitespace characters removed.
```
myStr := "\n\n\nHello World";
myStr := myStr.trimLeft();
```

### string String.trimRight()
Returns a new string with all of the right-hand whitespace characters removed.
```
myStr := "Hello World\n\n\n";
myStr := myStr.trimRight();
```

### number String.length
Property that represents how long the string is.
```
println(args[0].length);
```
