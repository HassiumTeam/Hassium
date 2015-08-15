# Introduction to Hassium

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
