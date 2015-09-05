# Core Features of Hassium

## While Loop

Syntax:
```
while (<condition>)
	<statements>
```

Can be used to execute code as long as the condition
is met and returns true.

Examples:
```
x := 0;
while (x < 4) {
	println(x);
	x++;
} # prints 0 1 2 3
```

```
while (true) {
	println("INFINITE");
} # Infinite loop
```

## For loop

Syntax:
```
for(<statement>; <condition>; <statement>)
	<statements>
```

Runs a statement initially, then runs a statement and a block
of statements as long as the condition returns true.

Examples:
```
for (x := 0; x < 10; x++) {
	println(x);
}
```

```
for (inp := input(); inp != "quit"; print(">")) {
	println("Inp is not equal to quit");
}
```

## Functions

Syntax:
```
func <funcName>(<args>) {
	<statements>
}
```

Contains a code block that can return the result of statements
that can optionally operate on a list of arguuments.

Examples:
```
func add(num1, num2) {
	return num1 + num2;
}

println(add(4, 5));
```
