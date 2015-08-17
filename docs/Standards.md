# Hassium Coding Standards and Good Practices

## Intro

Hassium programming is designed to give the developer as much freedom as possible
when it comes to how they like to create their program. However there are some
best practices that are wise to keep in mind when coding in Hassium that I think
is the best for maintainability and for your code to be as readable as possible.

## Variable Naming

Variables in Hassium are created like:
```
myVar := 4;
```

Variable names should be pascelCase always. This means that variable names
containing more than one word shouldLookLikeThis, where the first word is
lower case and the rest of the words start with uppercase.

Good: this thisIs thisIsATest
Bad: This THISIS thisisatest

## Functions

All functions in Hassium are completely lower case. Some examples are:
```
strcat();
getfcol();
sstr();
getch();
```

## Spacing

Every progrmmer has their own ideas on what spacing should look like, and for
the most part, that's okay, and in Hassium it shouldn't be that big of a deal
either, but here are the best practices in my mind on spacing.

When assigning a variable there should be a space between the variable name, the
assignment operator, and the expression being assigned to it, like so:
```
thisVar := (4 + 20) - 10;
```

When you are making a function call, there should be no space between the function
name and the opening parentheses ( like so:
```
print("Hello, World!");
```

Also in functions where you are specifying multiple arguments there should be a space
after the comma that seperates the arguments:
```
strcat("Hello", "there");
```

When creating an if statement or while loop, have a space between the keyword and the
opening parentheses:
```
if (x > 4) {
	print("x is greater than 4");
} else {
	print("x is not greater than 4");
}
```

```
while (x < 10) {
	print("Do something");
	x := x + 1;
}
```

## Brackets

Brackets (or curly braces, if you are a terrorist) { }, are used to create a code block
around different expressions or statements. Brackets are used in if statements as well
as while loops. The opening bracket  { should be on the first line of the statements,
then after the last statement, press return and add the closing curly bracket. If you
wish to have an else to your if staement or loop then the else and it's opening bracket
should be on the same line as the closing bracket }, like so:
```
if (msg = "Hassium is the greatest") {
	print("yeah");
} else if (msg = "Hassium REALLY is the greatest") {
	print("w00");
} else {
	print("But... it's the greatest");
}
```

When you are creating an if statement or loop it's worth noting that you do not nessecarily
need to have brackets if you just want to have one statement. For example:
```
if (3 = 2)
	print("This is valid");
else
	print("This is valid");
	print("This is not part of the else statement, and there should be brackets surrounding us");
```

## Indentation

All programs in Hassium start on the very left of the screen, when you are inside of a code block
or if statement/loop, indent one over, for example:
```
if (x = y) {
	print("Going into loop\n");
	while (x < (y * 10)) {
		print("loop\n");
		x := x + 1;
	}

	print("Out of that loop\n");
}

print("All done\n");
```

## Expressions

For the most part, your expressions are going to be mathematical. They will be a mix of numbers and
variables with any amount of mathematical operations or funcions acting upon them. Here's an example
expression:
```
5 * ((4 / 2) + ((x - 3 * z) / 4) - 1)
```

Every differant piece of the expression should have a space inbetween it, except for parentheses, which
should directly "touch"  the variable or number they are against. Also with conditional expressions
such as those used as the conditions for if statements and loops should have spaces
between the comparison binary operators like so:
```
if (((x * 3) = 5) || (4 - 2) = 2)
```
