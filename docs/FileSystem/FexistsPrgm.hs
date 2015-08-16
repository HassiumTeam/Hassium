$SUMMMARY: Determins if the file entered by the user exists$

print("Enter a file path to see if it exists: ");
path := input();

print("Does file exist? ", fexists(path), "\n");

exit(0);
