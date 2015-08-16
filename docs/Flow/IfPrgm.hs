$SUMMARY: Checks if the user entered a number between one and 10$

print("Enter a number between 1 and 10: ");
num := tonum(input()); $NOTE: It is not nessecary to convert the input() string into a num, but it is
			good practice to do so, as some functions might be picky about the input they
			are given and it's also easier to see what you are working with.$

if (num < 1)
	print("That is less than one!");
else if (num > 10)
	print("That is greater than 10!");
else
	print("Good job!");

print("\n");

exit(0);
