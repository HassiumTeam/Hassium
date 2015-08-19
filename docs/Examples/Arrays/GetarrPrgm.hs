$SUMMARY: Gets the user specified number from array$

names := toarr("John", "Michael", "Smith");
maxNum := arrlen(names);
print(type(maxNum));
print("You have ", maxNum, " choices: ");
choice := input();

if ((choice > maxNum) || (choice <= 0)) {
	print("You idiot, that's an invalid option!");
	exit(1);
}

print("The name you selected was: ", getarr(names, (choice - 1)), "\n");

exit(0);
