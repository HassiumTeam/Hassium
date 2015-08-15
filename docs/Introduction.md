# Introduction to Hassium

## Part 1: Setting up Hassium

Hassium is a simple programming language written in C# with a syntax similar to C and Python. It is easy
to get Hassium set up on your computer and running your first program.

Your first step although probably optional but recommended is to compile Hassium. If you are on Windows
(and on *nix systems) you can open the .sln file in Visual Studio/Monodevelop and compile it. If you
prefer to use the command line on Linux you can invoke xbuild Hassium.sln to compile it. The resulting
EXE should be in the bin/Debug folder. If you would like to add Hassium to your PATH on linux simply
create the following shell script in /usr/bin/Hassium:
```
#!/bin/bash

mono ~/Hassium/src/Hassium/bin/Debug/Hassium.exe $1
```
Change the path accordingly to where your Hassium.exe is located. On windows you will just move the
Hassium.exe to your Desktop folder where you will then create your scripts.

## Part 2: Running Your First Program

Now it's time to create our first program. In the folder where Hassium is located open up your favorite
text editor (I reccomend vim for *nix) and type the code in HelloWorldPrgm.hs:
```
$SUMMARY: Shows a greeting to the user$

print("Hello, World!");
```

Save this file then execute it with either Hassium.exe HelloWorldPrgm.hs (on Windows) or 
mono Hassium.exe HelloWorldPrgm.hs (on *nix). You should see the text Hello, World! on your console
window.

You should note the different parts of the program that are shown here. We have the first line which
is a comment. In Hassium comments start with a $ and they end with a $, much like /* and */ in mainstream
languagaes. The next part of the code is the function call print(). This invokes a builtin function called
print whose job is to write text to the console. Inside of the print function is called arguments.
Arguments are essentially specifications to the function about what it should be doing. Since it's print's
job to write to the screen, the argument "Hello, world!" will cause it to specifically write the text
Hello, World! to the screen. Also note the double-quotes around Hello, World!, this is because the
message we are writing is a string, or series of characters. Strings are surrounded by double quotes in
Hassium, NOT single ' ' quotes. Now after the function call there is a semicolon ;. Semicolons indicate
the end of the line/statement that we are running, and should not be forgotten.

## Part 3: Variables

If you've ever programmed before or if you have taken 8th grade algebra you should know about
variables. Variables are data represented by an identifier that you specify. Take the following
program VariablesPrgm.hs:
```
$SUMMARY: Uses variables$

myVar := "Hassium is free as in free beer and in freedom!";
print(myVar, "\n");
```

Now again let's look at the different parts of this program. We have the comment at the beginning that
tells us what our program is going to do. Then the next line we have something new. We have the identifier
myVar, which is the name of our variable, then the symbols :=. The := is an assignment operator in Hassium,
meaning that it stores data from the right hand side to the left. In this case it is storing the string
"Hassium is free as in free beer and in freedom!" in the variable myVar. So when we call print() it will
print that string onto the console. But when we look at our print() call again we can notice something
new. After the variable myVar there is a comma, and then another string! The job of the comma is to
seperate differant arguments in a function call. Also you may notice that when the program is outputed
it doesn't print \n to the screen after the first string. \n is a special character that tells Hassium
to print a newline (like pressing the enter key) to the console. \n can go anywhere inside a string and
when encountered will create a newline on the console.

Let's take a look at another program that uses another function call and demonstrates variable 
reassignment. We'll call this Variables2Prgm.hs:
```
$SUMMARY: Uses variable reassignment and concatanation";

myVar := "Hassium is free as in free beer and in freedom!";
print(myVar);
myVar := strcat(myVar, " The github is https://github.com/JacobMisirian/Hassium");
print("\n", myVar);
```

The first part of this should look the same to you, myVar gets assigned to a string and is then printed
out to the console. But then on the third line of code things are a little differant. myVar is assigned
again to another string. On the right hand side of := you can see that the string it's getting asigned 
to starts with another function call, this time to strcat(). Strcat() is another builtin function whose
job it is to take in multiple strings and concatanate them into one result string. The arguments we
give strcat in this example is the variable myVar and another string. These get added together by
strcat into one final string and then passed to myVar. On the next line is a call to print with the
arguments of a newline and then myVar.

Hopefully by now you have a pretty good grasp of how variables are assigned and how function calls
are made. If not please go back to the start and try and read over it again. I also reccomend that throughout
all of this that you are trying the examples and making your own similar programs, as that is truely
the only effective way to learn to program.

## Part 4: Reading User Input and the Rest Of the Console Functions

From this point on we will be focusing less on the program design and more on the functions that are
builtin to Hassium. One of the most important is the input() function call. Input reads user input
from the keyboard and returns it in the form of a string. Take InputPrgm.hs:
```
$SUMMARY: Reads user input and returns it back$

print("Enter some text: ");
in := input();
print("The text you entered was: ", in, "\n");
```

This first call to print() writes some text to the console that asks the user to enter in a string.
Then we have the variable in, which gets assigned the result of a call to the input() function, effectively
storing the user's input inside the variable in. The last line prints out a string, the contents of
the variable in, then a newline character.

The functions print() and input() are all part of the console family of functions. In this documentation
there should be a folder called Console that contains descriptions and examples of these functions. The
source code for them can also be found in the folder src/Hassium/Functions/ConsoleFunctions.cs. These
functions deal primarily with management of the console. We've covered the first two functions and now we'll
deal with the last one, the cls() function.

Cls is a function call that clears the console of all text. Let's look at it in action in ClearPrgm.cs:
```
$SUMMARY: Demonstrates clearing of console$

print("When you press a key next the console will clear");
pause();
print("You should not see me");
cls();
print("The console has been cleared\n");
```

In this program we prompt the user to press enter, and we call pause() to simply hang the program until
a key is pressed. From there there is a print call that is meant to show that cls will erase the text
outputed by that in a few miliseconds when the very next line, cls, is called. The last print call
comes after the cls() and can be seen by the user.

## Part 5: Math in Hassium

Hassium is capable of parsing most mathematical expressions. Take the program SampleMathPrgm.hs:
```
$SUMMARY: Demonstrates math capabilities$

print("Enter the first number: ");
x := input();

print("Enter the second number: ");
y := input();

print(x, "+", y, "=", (x + y), "\n");
print(x, "-", y, "=", (x - y), "\n");
print(x, "*", y, "=", (x * y), "\n");
print(x, "/", y, "=", (x / y), "\n");
```

This program gets two numbers from the user then prints out four different ways the numbers can be
operated upon, being addition, subtraction, multiplication, and division. These can be combined in
many differant ways, for example this line is perfectly valid: 
``` 
print(((3 * 5) / 2) + 1); 
```

Hassium uses PEMDAS to evaluate mathematical expressions such as that above.

Hassium also includes a math family of functions. As this documentation is being written there
isn't that many at the moment but I will demonstrate the ones that are there in the file MathPrgm.hs:
```
$SUMMARY: Shows math family$

print("Enter a number to square root: ");
x := input();

print("Square root is: ", sqrt(x), "\n");

print("Enter a base number: ");
x := input();
print("Enter a power: ");
y := input();

print("Number raised to power is: ", pow(x, y), "\n");
```

The two differant math functions being called here are sqrt() and pow(). Sqrt is a function that
takes the square root of the argument given and returns it. Pow is a function that takes in two
arguments and raises the first argument to the power of the second.

More math functions will continue to roll in by the time you read this, so check in the file
src/Hassium/Functions/MathFunctions.cs or in the documentation for all of the current functions.

## Part 6: File System access in Hassium

Hassium comes with many differant filesystem operations (see src/Hassium/Functions/FileSystemFunctions.cs)
that can preform such operations as creating/deleting files and directories or setting/getting the
current directory or determining if a file/directory exists. Take a gander at FileCreatePrgm.hs:
```
$SUMMARY: Creates file with specified text$

print("Enter a filename: ");
path := input();
print("File already exists: ", fexists(path));

print("\nEnter text to put in file: ");
contents := input();

puts(path, contents);
```

This starts with getting a file path from the user and checking if it exists or not using the
function fexists() to return a boolean true or false depending on if the file already exists.
Then the program prompts the user for some contents to put in the file and uses the puts
function to put the variable contents into the file at variable path. Now look at DeletePrgm.cs:
```
$SUMMARY: Deletes a file we specify$

print("Enter a file path: ");
path := input();

print("File exists: ", fexists(path), "\n");

dfile(path);
```

Another useful function in the filesystem family is the system() function. System can execute
another process on your computer with command line arguments. The syntax is system(path, arguments).
It is important to note that even if the process you are starting requires no arguments that
you need to still provide an empty string "" were the argument for arguments should be.
Here's SystemPrgm.hs:
```
$SUMMARY: Runs an exe with arguments$

print("Enter the path to an exe: ");
path := input();
print("Enter the arguments (optional): ");
args := input();

system(path, args);
```

The program prompts and gets the path and arguments from the user, then uses them as the
arguments for the system function to execute the process.
