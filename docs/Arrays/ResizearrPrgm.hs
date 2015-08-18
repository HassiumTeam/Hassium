$SUMMARY: Creates array, displays length, extends it, then proves extention$

myArr := toarr("Hello", "There");

print("Original array length: ", arrlen(myArr), "\nOriginal array contents: ", concatarr(myArr));

myArr := resizearr(myArr, 4);
myArr := setarr(myArr, "My", 2);
myArr := setarr(myArr, "People", 3);

print("\nNew array length: ", arrlen(myArr), "\nNew array contents: ", concatarr(myArr));
