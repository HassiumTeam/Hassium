$SUMMARY: Converts string to number and shows conversion$

print("Enter a number: ");
in := input();

print("Input is of type: ", type(in), "\n");

in := tonum(in);

print("Input is now of type: ", type(in), "\n");

exit(0);
