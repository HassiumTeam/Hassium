$SUMMARY: Raises the user entered number to the user entered power$

print("Enter a number: ");
num := input();

print("Enter a power to raise ", num, " to: ");
power := input();

print(num, "^", power, " = ", pow(num, power), "\n");

exit(0);
