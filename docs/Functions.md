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

### string system(string path, string arguments)

Runs the specified executable of path with the arguments. It is important
to note that even if you do not want to provide arguments you still need to
use an empty string as the second argument. Returns the standard output of the
application ran.
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

## setarr

### array setarr(array arr, var contents, double index)

Sets the specidied zero-indexed position from the specified array to the value of contents, then
returns the new array.
```
myArr := toarr("this", "that", "the other");
println(getarr(myArr, 1);
myArr := setarr(myArr, "new", 1);
println(getarr(myArr, 1);
```

## arrlen

### double arrlen(array arr)

Returns the length of the array specified.
```
println("The number of command line arguments was: ", arrlen(args));
```

## newarr

### array newarr(double length)

Returns a blank array with the number of specified values in it.
```
myArr := newarr(3);
```

## reversearr

### array reversearr(Array arr)

Returns the specified array with it's values reversed.
```
myArr := toarr("this", "that", "the other");
myArr := reversearr(myArr);
```

## pow

### double pow(double base, double power)

Returns the base raised to the power.
```
println("Two to the 6th power is: ", pow(2, 6));
```

## sqrt

### double sqrt(double number)

Returns the square root value of the number.
```
println("The square root of 36 is: ", sqrt(36));
```

## abs

### double abs(double number)

Returns the absolute value of the number.
```
println("The absolute value of 420 is: ", abs(420));
```

## free

### null free(variable var)

Frees the specified variable.
```
myVar := "this variable exists";
println(myVar);
free(myVar);
println(myVar); #This will throw an unknown variable exception.
```

## type

### string type(variable var)

Returns the type in string format of the specified variable.
```
println(type("this is a string"));
println(type(4));
println(type(toarr("this")));
```

## throw

### null throw(string exception)

Throws a C# exception with the specified message. Will also cause the application
to terminate.
```
if (!fexists(getarr(args, 0)))
	throw("The file does not exist!");
```

## dowstr

### string dowstr(string url)

Downloads and returns the string downloaded from the specified string URL
```
string result := dowstr(https://raw.githubusercontent.com/HassiumTeam/Hassium/master/README.md);
println("The README.md is: ", result);
```

## dowfile

### null dowfile(string url, string path)

Downloads to the speicified path the file from the speicified URL.
```
dowfile("https://github.com/HassiumTeam/Hassium/releases/download/1.2.0.0/Hassium.exe", @"C:\Users\Default\Hassium.exe");
```

## upfile

### string upfile(string url, string path)

Uploads the specified file to the URL and returns the server's response.
```
response := upfile("my-site/upload.php", @"C:\Users\Default\test.txt");
println("The server returned the response: ", response);
```

## strcat

### string strcat(string one, string two, string three, ...)

Concatonates all of the strings presented to it and returns the result as a string.
```
println("Result string is: ", strcat("this", "that", "the other"));
```

## strlen

### double strlen(string str)

Returns the length of the specified string.
```
print("Enter a string: ");
println("The length of the string you entered was: ", strlen(input()));
```

## getch

### string getch(string str, double index)

Returns the character (as string) of the specified string at the zero-indexed point.
```
print("Enter a string: ");
println("The 3rd character of the string is: ", getch(input(), 2));
```

## sstr

### string sstr(string str, double startIndex, double length)

Returns the substring of the specified string starting at the first zero-indexed point
and continuing for the specified length.
```
myStr := "Hello, World!";
println(sstr(myStr, 2, 3));
```

## begins

### bool begins(string str, string prefix)

Returns true if the specified string starts with the prefix, otherwise returns false.
```
println("Enter some text that starts with 'abc': ");
str := input();
if (begins(str, "abc"))
	println("Good job!");
else
	println("You failed :(");
```

## ends

### bool ends(string str, string sufix)

Returns true if the specified string ends with the prefix, otherwise returns false.
```
println("Enter some text that ends with 'xyz': ");
str := input();
if (ends(str, "xyz"))
	println("Good job!");
else
	println("You failed :(");
```

## toupper

### string toupper(string str)

Returns the specified string in uppercase.
```
println("Enter some text: ");
str := input();

println("Text is now: ", toupper(str));
```

## tolower

### string tolower(string str)

Returns the specified string in lowercase.
```
println("Enter some text: ");
str := input();

println("Text is now: ", tolower(str));
```

## contains

### bool contains(string str, string text)

Returns true if the specified string contains the text, otherwise returns false.
```
print("Enter some text that has the word 'cookies' in it: ");
str := input();

if (contains(str, "cookies"))
	println("Good job!");
else
	println("You failed!");
```

## split

### array split(string str, string character)

Returns an array with values seperated by the specified character.
```
print("Enter some text: ");
str := input();

words := split(str, " ");
foreach (word in words)
	println("Word: ", word);
```

## replace

### string replace(string str, string oldPart, string newPart)

Returns a new string with the old part replaced with the new part.
```
str := "hello world";
println(replace(str, "hello", "greetings"));
```

## index

### double index(string str, string character)

Returns the first index of the instance of the character in the specified string.
```
str := "hello world";
println("The first index of 'l' is: ", index(str, "l"));
```

## lastindex

### double lastindex(string str, string character)

Returns the last index of the instance of the character in the specified string.
```
str := "hello world":
println("The last index of 'l' is: ", index(str, "l"));
```

## padleft

### string padleft(string str, double length)

Returns a new string with the original string padded with spaces from the left until
the string is the specified length.
```
str := "incomplete string";
println("Padded string is: ", padleft(str, 25));
```

## padright

### string padright(string str, double length)

Returns a new string with the original string padded with spaces from the right until
the string is the specified length.
```
str := "incomplete string";
println("Padded string is: ", padright(str, 25));
```

## remove

### string remove(string str, string oldchar, string newChar)

Returns a new string with the old single character replaced with the new character.
```
str := "hexxo worxd";
println(remove("x", "l"));
```

## trim

### string trim(string str)

Returns a new string that has been trimmed of all whitespace characters.
```
str := "\n  blah  \n\n   ";
println(trim(str));
```

## trimleft

### string trimleft(string str)

Returns a new string that has been trimmed of leading whitespace characters.
```
str := "\n  blah  \n\n   ";
println(trimleft(str));
```

## trimright

### string trimright(string str)

Returns a new string that has been trimmed of ending whitespace characters.
```
str := "\n  blah  \n\n   ";
println(trimright(str));
```

## datetime

### string datetime()

Returns the current date and time according to the system.
```
println("The current time is: ". datetime());
```

## currentuser

### string currentuser()

Returns the corrently logged on user.
```
println("The current user is: ", currentuser());
```
