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

exit(0);
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
the end of the line/statement that we are running, and should not be forgotten. Also you may notice the
line exit(0) at the end. Exit is the function call that ends the program. Although without the line
the program should terminate peacefully it is still a good idea to call it to make sure that the
exit code is 0 (EXIT_SUCCESS).

## Part 3: Variables

If you've ever programmed before or if you have taken 8th grade algebra you should know about
variables. Variables are data represented by an identifier that you specify. Take the following
program VariablesPrgm.hs:
```
$SUMMARY: Uses variables$

myVar := "Hassium is free as in free beer and in freedom!";
print(myVar, "\n");

exit(0);
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

exit(0);
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

exit(0);
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

exit(0);
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

exit(0);
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

exit(0);
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

exit(0);
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

exit(0);
```

Another useful function in the filesystem family is the system() function. System can execute
another process on your computer with command line arguments. The syntax is system(path, arguments).
It is important to note that even if the process you are starting requires no arguments that
you need to still provide an empty string "" where the argument for arguments should be.
Here's SystemPrgm.hs:
```
$SUMMARY: Runs an exe with arguments$

print("Enter the path to an exe: ");
path := input();
print("Enter the arguments (optional): ");
args := input();

system(path, args);

exit(0);
```

The program prompts and gets the path and arguments from the user, then uses them as the
arguments for the system function to execute the process.

## Part 7: Program Flow with If Statements

So far all of our programs have been pretty static, the user types something in which
causes a set output, and the Hassium Interpreter reads from top to bottom without stopping,
but for a program to truely have functionality it has to have statements to modify the
program flow. One of the most common examples of these are if statements. If statements work
by determining if a condition is true and running a block of code based on that. The basic
format of an if statement is:
```
if (<condition>)
	<statement>
```

When the Hassium interpreter encounters the if statement it first determins if the condition
returns true. If the condition is true then it will procede to execute the code between the 
curly brackets { } called a "code block". There is also optionally an else statement, that
also has a code block, but this code block is only executed if the first condition returns
false. Here's the structure of an if statement with an else:
```
if (<condition>)
	<statement>
else
	<statement>
```

Lastly there is also something called else if, which isn't it's own statement so much as
as combination of the if and else statements, this checks a second condition and, if true,
executes it's own code block. An if-else if-else structure looks like:
```
if (<condition>)
	<statements>
else if (<condition>)
	<statements>
else if (<condition>)
	<statements>
else
	<statements>
```

There can be as many else if statements as you need, so long as they are part of an if
chain, likewise the else statement is not nessecarily needed either, and you can have a
lone if statement or an if-else with out an else. Let's revisit the DeletePrgm.hs
which used the function fexists to determine if a file existed, but the program wouldn't
do anything if the file didn't exist! To see what I mean run the program again and provide
an invalid path, you should see an exception raised about filenotfound. To prevent this
we can use an if statement to see if the file exists, and only attempt to delete the file
if that if statement is true. Here's the revised DeletePrgm.hs:
```
$SUMMARY: Checks if a file exists, and if so, deletes it, otherwise sends a message to the user$

print("Enter a file path: ");
path := input();

if (fexists(path)) {
	dfile(path);
	print("File successfully deleted!");

	exit(0);
} else {
	print("File does not exist!");

	exit(1);
}
```

Let's pick apart this program piece by piece. We start with prompting the user for a
path to a file. Next we have our if statement. This if statement has the condition
fexists(path). How this works is that fexists returns a variable that is of type
boolean (true or false). Since if statements operate on booleans if the fexists function
returns true, and the file does exist, it will execute the code block that deletes
the file and terminates peacefully. If the fexists function returns false then it will
cause the if statement to evaluate to false, meaning that the code in the else block
will execute, printing an error message and exiting with status code 1 (error).

Now let's make a program that uses if-else if-else to work on. This program will
prompt the user to enter two numbers and an operation to evaluate them on. We will use
our if-else if-else statements to determine which statement to evaluate and display
the results to the user. We'll call this ArithmeticPrgm.hs:
```
$SUMMARY: Gets two numbers and an operation from the user, then evaluates the math involved$

print("Enter the first number: ");
x := input();

print("Enter the second number: ");
y := input();

print("Enter the operation: ");
op := input();

print("Result: ");

if (op = "+") {
	print(x + y);
} else if (op = "-") {
	print(x - y);
} else if (op = "*") {
	print(x * y);
} else if (op = "/") {
	print(x / y);
} else {
	print("Unrecognized operation: ", op, "\n");
	exit(1);
}

print("\n");

exit(0);
```

This program should be fairly straight-forward. We get the two numbers and operation, then
evaluate based on that operation, meaning if the operation is + we add the two numbers, etc.
If the user's input wasn't a +, -, *, or / then we display an error and exit.

## Part 8: Looping with While Loops

Another important feature of program flow control and of all programming is the loop, a basic
loop works by evaluating if a condition is true, and executing a statement multiple times until
that condition is false. The basic syntax of a while loop is:
```
while (<condition>)
	<statement>
```

While loops can also have an else section, just like our if statements, that executes if the
initial condition retuns false. This structure looks like:
```
while(<condition>)
	<statement>
else
	<statement>
```

When using loops it is important to make sure you don't enter an infinite loop, this means
that you need to have some ability for the condition in your loop to return false so the code can
continue to execute.

Let's take a look at a program that displays the numbers one-ten without looping, called LamePrgm.hs:
```
$SUMMARY: Prints 1-10$

print("1\n");
print("2\n");
print("3\n");
print("4\n");
print("5\n");
print("6\n");
print("7\n");
print("8\n");
print("9\n");
print("10\n");

exit(0);
```

As you can see, this code is really long and boring, and if you wanted to iterate to 100 it would take
a LONG time to code. Let's revisit this program with while loops. NumPrgm.hs:
```
$SUMMARY: Increments and displays 1-10$

x := 1;

while(x < 11) {
	print(x, "\n");
	x := x + 1;
}

print("All done.\n");

exit(0);
```

This code starts by initializing the variable x with the value 1, then a while loop is encountered which
first evaluates if x is less than 11, which it is. It then executes the print statement and the variable 
incrementation. After those two lines are executed it reevaluates if x is less than 11, which it is again, and will
execute the same statements again. But the variable x is incrementing each time, so when it gets to the point
where x equals 11 the statements will stop exxecuting and the loop will terminate, running the line after which
prints the "all done" message onto the screen.

Sometimes it is useful to have an else statement on a while loop for if the condition initially returned
false. Here's an example of Num2Prgm.hs:
```
$SUMMARY: Increments and displays numbers from two points of user input$

print("Enter the initial number: ");
x := input();

print("Enter the end number: ");
y := input();

while (x < (y + 1)) {
	print(x, "\n");
	x := x + 1;
} else {
	print("The initial number is not less than the end number dummy!");
}

exit(0);
```

If the user entered 20 and 18, the else statement would execute since 20 is not less than 18, otherwise
the program would run normally.

## Part 9: Arrays

Arrays are data structures in prograamming that can store differant variables inside of it to be
referanced later in the program.

Arrays can be initialized eaither with a series of values or with a nnumber indicating how many
entries are to be in the array. Here are examples of both:
```
myArr := newarr(6); $Creates a blank array with 6 values inside$

anotherArr := toarr("this", "that", "the", "other"); $Creates an array with 4 string values$
```

From there you can get elements from the array with the getarr function:
```
anotherArr := toarr("this", "that", "the", "other");

print(getarr(anotherArr, 2)); $Returns "the"$
```

To change the value of an entry in the array you can use the setarr function to return a new
array with the modified value:
```
anotherArr := toarr("this", "that", "the", "other");
anotherArr := setarr(anotherArr, "thing", 3); $Changes "other" to "thing"$
```

As always, arrays are zero indexed, meaning that when you get entry 2 from an array you are
really getting the 3rd element in that array. Length of arrays, as shown in this next example
are not zero referenced, and contain the normal count of the elements in the array. Let's
take a look at the arrlen function:
```
anotherArr := toarr("this", "that", "the", "other");
print(arrlen(anotherArr)); $Displays 4$
```

Now if you had an array with 4 elements in it and you tried to change the 5th element, you
would get an exception saying that you are trying to access an element that does not exist.
This is because the array in questioned was initialized with only enough room for 4 values,
and you are trying to get at one that does not exist. Luckily Hassium includes a function
to resize arrays to a new size. Here's how that is used:
```
myArr := toarr("hello", "world");
myArr := resizearr(myArr, 3);
myArr := setarr(myArr, "w00", 2);
print(getarr(myArr, 2)); $Prints w00$
```

Here's an example of a program that uses arrays called ArrayPrgm.hs:
```
$SUMMARY: Get's input from user into arrays then reads it back to them$

first := newarr(5);
last := newarr(5);

x := 0;

while(x < 5) {
	print("Enter a first name: ");
	first := setarr(first, input(), x);
	print("Enter a last name: ");
	last := setarr(last, input(), x);
}

print("The names are:\n");
print(concatarr(first), "\n");
print(concatarr(last), "\n");

exit(0);
```

You should know that by default, Hassium already has an array on runtime, called args
that holds the command line arguments that are being passed to Hassium. For however
many arguments the user passes is how large the array will be. Here's an example
of a program that uses this array called CheckFilePrgm.hs:
```
$SUMMARY: Checks to see if file exists$
$ARGUMENTS: args 0: FILE_PATH$

if (arrlen(args) < 1) {
	print("Not enough arguments! Syntax is: CheckFilePrgm.hs [FILE_PATH]\n");
	exit(1);
}

if (fexists(getarr(args, 0))) {
	print("File exists.\n");
} else {
	print("File does not exist.\n");
}

exit(0);
```

## Part 10: For Loops

For loops are another essential part of most programming languages that make looping a
simpler process. A for loop is similar to a while loop, but inside of the parentheses of
the for loop you can initilize and increment variables.

Here is the basic structure of a for loop:
```
for (<statement>; <condition>; <statement>)
	<statements>;

```

The first statement is usually used for creating a new counter variable. The expression
part of it is like what you would put inside of a while loop, it is the condition that
must return true for the statements in the for loop to execute. The third statement is
generally used to increment the counter variable. Here is an example of a program that
counts from 1 to 10 using a for loop called ForPrgm.hs:
```
$SUMMARY: Counts from 1 to 10 using a for loop$

for (x := 1; x < 11; x := x + 1) {
	print(x, "\n");
}

exit(0);
```

## Part 11: Try Catch

Trying and catching is an extremely useful way to prevent your program from crashing and
creating confusing errors and stack traces that can confuse your user. When an exception
is thrown normally it prints a stack trace and exits the program, you can prevent this from
happening by "catching" the exception. When you catch an exception instead of the program
exiting on it's own, you can to choose the code that you want to execute if some thing goes
wrong.

For instance, say your program took user's input and converted it into a number, but what
if the user types "pig" instead of a number? Well normally this would cause the program to
crash with an exception saying "cannot convert" or similar. But if you catch this exception
you can print out a friendly message saying "Please enter a valid number". We can see this
in ConvertNumPrgm.hs:
```
$SUMMARY: Takes input, converts to number, and displays it + 5$

try {
	num := tonum(input());
} catch {
	print("Enter a valid number!");
	exit(1);
}

print(num + 5);

exit(0);
```

## Part 12: For Each Loops

For each loops are similar to for loops, but more useful if you want to iterate over an Array.

Here is the basic structure of a for loop:
```
foreach (<needle> in <haystack>)
	<statements>;

```

The needle thing is the variable that will, similarly to the for-loop, be changed at every iteration. It's the "current item".
The haystack is the array you will iterate on.
Example from ForEachPrgm.hs:
```
$SUMMARY: Print every item of an array$

myarray := toarr("abcd", 36/3, "defg", 3.1415);

foreach (myvar in myarray) {
	println(myvar);
}

exit(0);
```
This will print :
```
abcd
12
defg
3.1415
```
