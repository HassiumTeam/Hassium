$SUMMARY: Demonstrates try catch$

try {
	print(getarr(args, 0), "\n");
} catch {
	print("Not enough command line arguments specified!\n");
}

exit(0);
