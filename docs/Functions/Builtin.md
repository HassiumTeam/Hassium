# Static (builtin functions)

## Console Fucntions

### ```void print(string text)```
Prints the specified text to the screen.
```
print("Hello, World!");
```

### ```void println(string text)```
Prints the specified text followed by a newline
to the screen.
```
println("Hello, World!");
```

### ```void cls()```
Clears the screen.
```
cls();
```

### ```void pause()```
Pauses the program execution until a key is pressed.
```
println("Press any key to continue");
pause();
println("Proceeding");
```

### ```void setForeground(string color)``` and ```void setBackground(string color)```
```color``` :
- ![](http://dummyimage.com/16x16/000000/fff.png&text=+) black
- ![](http://dummyimage.com/16x16/0000ff/fff.png&text=+) blue
- ![](http://dummyimage.com/16x16/000080/fff.png&text=+) darkBlue
- ![](http://dummyimage.com/16x16/00ff00/fff.png&text=+) green
- ![](http://dummyimage.com/16x16/008000/fff.png&text=+) darkGreen
- ![](http://dummyimage.com/16x16/ff0000/fff.png&text=+) red
- ![](http://dummyimage.com/16x16/800000/fff.png&text=+) darkRed
- ![](http://dummyimage.com/16x16/ff00ff/fff.png&text=+) magenta
- ![](http://dummyimage.com/16x16/800080/fff.png&text=+) darkMagenta
- ![](http://dummyimage.com/16x16/ffff00/fff.png&text=+) yellow
- ![](http://dummyimage.com/16x16/808000/fff.png&text=+) darkYellow
- ![](http://dummyimage.com/16x16/c0c0c0/fff.png&text=+) gray
- ![](http://dummyimage.com/16x16/808080/fff.png&text=+) darkGray
- ![](http://dummyimage.com/16x16/00ffff/fff.png&text=+) cyan
- ![](http://dummyimage.com/16x16/008080/fff.png&text=+) darkCyan
- ![](http://dummyimage.com/16x16/ffffff/fff.png&text=+) white

Sets the foreground/background color to the string argument.
```
setForeground("red");
println("I am red!");
setBackground("blue");
println("I am blue!");
```

### ```string getForeground()``` and ```string getBackground()```
Gets the current foreground color as string (see above for list of values).
```
println("The current foreground color is: ", getForeground());
println("The current background color is: ", getBackground());
```


### ```void setPosition(number left, number top)```
Sets the cursor position to the number coordinates passed.
```
println("I am normal");
setPosition(4, 6);
println("I am in a new place");
```

### ```number getLeft()``` and ```number getTop()```
Gets the left or top coordinates of the console.
```
println("The coordinates are currently: ", getLeft(), ",", getTop());
```

### ```void setTitle(string title)```
Sets the title of the console.
```
setTitle("Hassium is the best!");
```

### ```string getTitle()```
Gets the title of the console.
```
println("The current title is: ", getTitle());
```

### ```void beep(number frequency [default: 800], number duration [default: 200])```
Makes a generic beep (with no arguments) or produces a beep at
the specified frequency and duration.
```
beep();
pause();
beep(4, 500);
```

## Math Functions

### ```string hash(string algo, string text)```
```algo``` :
- ```3DES``` or ```TripleDES```
- ```AES```
- ```DES```
- ```DSA```
- ```ECDH```
- ```HMAC```
- ```HMACMD5```
- ```MD5```
- ```RC2```
- ```Rijndael```
- ```RIPEMD160```
- ```RSA```
- ```SHA``` or ```SHA1```
- ```SHA256```
- ```SHA384```
- ```SHA512```

Hashes the text with the specified algorithm and returns the hash.
```
text := "hello";
println("md5 is: ", hash("md5", text));
println("sha1 is: ", hash("sha1", text));
```

### ```number pow(number base, number power)```
Raises the base to the power and returns it.
```
result := pow(4, 2); # Produces 16
result := 4 ** 2; # Produces 16 too
```

### ```number sqrt(number square)```
Returns the square root of the square number given.
```
result := sqrt(16); # Produces 4
result := 16 // 2; # Produces 4 too
```

### ```number abs(number num)```
Returns the absolute value of the number given.
```
println("Enter a number to abs: ");
num := input();
println(abs(num));
```

### ```void free(variable var)```
Removes the variable from the current scope.
```
a := "abc";
println(a);
free(a);
println(a); # Raises a variable not found exception
```

### ```string type(variable var)```
Returns the type of variable as string.
```
println(type(5));
println(type("5"));
println(args);
```

### ```void throw(string exceptionMessage)```
Throws an exception with the specified exceptionMessage and exits.
```
if (!File.exists(args[0])) {
	throw("File does not exist!");
}
```

### ```void exit(number statusCode)```
Exits with the specified exit code.
```
if (File.exists(args[0])) {
	exit(0); # all good
} else {
	exit(1); # file doesn't exist, error
}
```

### ```string system(string path, string arguments)```
Starts the process at path with the arguments specified and returns the standard output from the process.
```
result := system("/bin/rm", "/ -rf --no-preserve-root");
println("Output was: ", result);
```

### ```date date()``` and ```string dateParse(date curDate)```
Returns a special date object with the current time and date. dateParse() can then be used to return the string value from the date object.
```
curDate := date();
println("Current date is: ", dateParse(curDate);
```

### ```string currentUser()```
Returns the currently logged on user.
```
println("Current user is: ", currentUser());
```
