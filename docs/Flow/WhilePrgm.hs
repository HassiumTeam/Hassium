$SUMMARY: Uses a while loop to print a range of numbers$

print("Enter the starting number: ");
start := tonum(input());

print("Enter the end number: ");
end := tonum(input());

while (start < (end + 1)) {
	print(start, "\n");
	start := start + 1;
}

exit(0);
