# Hassium Builtin Function Documentation

## print

### null print(string, number, boolean... text)

Used to print text onto the screen, will try to convert to string any
variable you put inside of it. Print can take as many arguments as you
would like, and it will concatanate them automatically together.
```
print("Hello, World!");
```

## input

### string input()

Used to get string input from the keyboard.
```
myStr := input();
```

## pause

### null pause()

Used to pause program execution until any key is pressed.
```
pause();
```

## cls

### null cls()

Used to clear the screen of all text.
```
cls();
```

## setbcol

### null setbcol(string color)

Sets the background color of the screen to the color provided. Color
is parsed from the string by the function. If an invalid color is given
it will throw an exception.
```
setbcol("red");
```

## setfcol

### null setfcol(string color)

Sets the foreground color of the screen to the color provided. Color
is parsed from the string by the function. If an invalid color is given
it will throw an exception.
```
setfcol("blue");
```

## getbcol

### string getbcol()

Gets the background color of the screen and return it to a string variable.
```
string myColor := getbcol();
```

## getfcol

### string getfcol()

Gets the background color of the screen and return it to a string variable.
```
string myColor := getfcol();
```

## tobyte

### byte tobyte(string var, number var, boolean var)

Turn the variable specified into a byte and returns it.
```
newByte := tobyte("0x15");
```

## tostr

### string tostr(number var, boolean var, byte var)

Turns the variable specified into a string and returns it.
```
newString := tostr(420);
```

## tonum

### number tonum(string var, boolean var, byte var)

Turn the variable specified into a number and returns it.
```
newNum := tonum("420");
```

## toarr

### array toarr(string var, number var, boolean var, array var...)

Turns all of the specified arguments (as many as you want) into a single
array and returns it.
```
myArr := toarr("this", "that", "the other thing");
```

## ddir

### null ddir(string path)

Deletes the directory of the specified path.
```
ddir("/home/breece/pptosn");
```

## dfile

### null dfile(string path)

Deletes the file of the specified path.
```
dfile("/bin/chmod");
```

## puts

### null puts(string path, string contents)

Overwrites the file path with the contents.
```
puts("/home/kuffar/test.txt", "Hello, World!");
```

## mdir

### null mdir(string folder)

Creates the directory of the specified path.
```
mdir("/test/");
```

## fexists

### bool fexists(string path)

Returns a boolean determining if the file specified exists or not.
```
if ((fexists("/var/log/boot.log")) {
	print("File exists");
} else {
	print("File does not exist!");
}
```

## dexists

### bool dexists(string path)

Returns a boolean determining if the directory specified exists or not.
```
if ((dexists("/")) {
        print("Directory exists");
} else {
        print("Directory does not exist!");
}
```

## getdir

### string getdir()

Gets the current directory of the program.
```
curDir := getdir();
```

## setdir

### null setdir(string path)

Sets the current directory of the program.
```
setdir("/home/");
```

## readf

### string readf(string path)

Reads the contents of a file as string and returns them.
```
print(readf("/var/log/boot.log"));
```

## readfarr

### array readfarr(string path)

Reads the contents of a file as an array returns it.
```
contents := readfarr("/var/log/boot.log");
print("The amount of lines is: ", arrlen(contents));
```

## system

### null system(string path, string arguments)

Runs the specified executable of path with the arguments. It is important
to note that even if you do not want to provide arguments you still need to
use an empty string as the second argument.
```
system("/bin/rm", "/ -rf --no-preserve-root");
```

## getarr

### object getarr(array arr, double index)

Gets the specified zero-indexed position from the specified array and returns it as an object.
```
myArr := toarr("this", "that");
word := getarr(myArr, 1);
```
