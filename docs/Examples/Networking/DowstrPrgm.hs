$SUMMARY: Downloads a string from the web and displays it$

print("We are going to download from http://raw.githubusercontent.com/JacobMisirian/Hassium/master/src/Hassium/MyClass.cs");

result := dowstr("https://raw.githubusercontent.com/JacobMisirian/Hassium/master/src/Hassium/MyClass.cs");

print("\n\nThe result is:\n\n", result);

exit(0);
