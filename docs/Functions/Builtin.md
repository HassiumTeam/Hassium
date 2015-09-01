# Static (builtin functions)

## Console Fucntions

### null print(string text)
Prints the speicifed text to the screen.
```
print("Hello, World!");
```

### null println(string text)
Prints the specified text followed by a newline
to the screen.
```
println("Hello, World!");
```

### null cls()
Clears the screen.
```
cls();
```

### null pause()
Pauses the program execution until a key is pressed.
```
println("Press any key to continue");
pause();
println("Proceeding");
```

### null setForeground(string color)
Sets the forground color to the string argument.
```
setForeground("red");
println("I am red!");
setForeground("blue");
println("I am blue!");
```

### null setBackground(string color)
Sets the background color to the string argument.
```
setBackground("red");
println("I am red!");
setBackground("blue");
println("I am blue!");
```

### string getForeground()
Gets the current foreground color as string.
```
println("The current color is: ", getForeground());
```

### string getBackground()
Gets the current background color as string.
```
println("The current color is: ", getBackground());
```

### null setPosition(number left, number top)
Sets the cursor position to the number coordinates passed.
```
println("I am normal");
setPosition(4, 6);
println("I am in a new place");
```

### number getLeft() && number getTop()
Gets the left or top coordinates of the console.
```
println("The coordinates are currently: ", getLeft(), ",", getTop());
```

### null setTitle(string title)
Sets the title of the console.
```
setTitle("Hassium is the best!");
```

### string getTitle()
Gets the title of the console.
```
println("The current title is: ", getTitle());
```

### null beep() && null beep(number frequency, number duration)
Makes a generic beep (with no arguments) or produces a beep at
the specified frequency and duration.
```
beep();
pause();
beep(4, 500);
```

## Math Functions

### string hash(string algo, string text)
Hashes the text with the specified algorithm and returns the hash.
```
text := "hello";
println("md5 is: ", hash("md5", text));
println("sha1 is: ", hash("sha1", text));
```

### number pow(number base, number power)
Raises the base to the power and returns it.
```
pow(4, 2); # Produces 16
```

### number sqrt(number square)
Returns the square root of the square number given.
```
sqrt(16) # Produces 4
```

### number abs(number num)
Returns the absolute value of the number given.
```
println("Enter a number to abs: ");
num := input();
println(abs(num));
```

## null free(variable var)
Removes the variable from the current scope.
```
a := "a";
println(a);
free(a);
println(a); # Raises a variable not found exception
```

## string type(variable var)
Returns the type of variable as string.
```
println(type(5));
println(type("5"));
println(args);
```

## null throw(string exceptionMessage)
Throws an exception with the specified exceptionMessage and exits.
```
if (File.exists(args[0])) {
	throw("File does not exist!");
}
```

## null exit(number statusCode)
Exits with the speicified exit code.
```
if (File.exists(args[0])) {
	exit(0);
} else {
	exit(1);
}
```

## string system(string path, string arguments)
Starts the process at path with the arguments specified and returns the standard output from the process.
```
result := system("/bin/rm", "/ -rf --no-preserve-root");
println("Output was: ", result);
```

## date date() && string dateParse(date curDate)
Returns a special date object with the current time and date. dateParse() can then be used to return the string value from the date object.
```
curDate := date();
println("Current date is: ", dateParse(curDate);
```

## string currentUser()
Returns the currently logged on user.
```
println("Current user is: ", currentUser());
```
