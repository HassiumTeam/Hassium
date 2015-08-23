$SUMMARY: Demonstrates threading using while loops$

NUMCONST := 20000;

for (x := 0; x < NUMCONST; x := x + 1) {
	print("");
}
print("First non-threaded loop\n");

for (x := 0; x < NUMCONST; x := x + 1) {
	print("");
}
print("Second non-threaded loop\n");

print("Press any key to move to threaded loops\n");
pause();

thread {
	for (x := 0; x < NUMCONST; x := x + 1) {
		print("");
	}

	print("First threaded loop\n");
}

thread {
	for (x := 0; x < NUMCONST; x := x + 1) {
		print("");
	}

	print("Second threaded loop\n");
}

input();
