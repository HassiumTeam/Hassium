$SUMMARY: Gets a path and contents from the user and writes the contents to the path$

print("Enter a path: ");
path := input();

print("Enter text to put in file: ");
contents := input();

puts(path, contents);

exit(0);
