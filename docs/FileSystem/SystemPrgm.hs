$SUMMARY: Runs the specified file with specified arguments by user$

print("Enter the path to an executable: ");
path := input();

print("Enter the arguments (if any): ");
args := input();

system(path, args);

exit(0);
