# Array functions	

## LINQ functions

### ```object array.op(func[x, y] f)```
Performs an operation on each element of the array and return the final result.
```go
totalSum := theArray.op([x, y] => x + y);
``` 
Here, ```totalSum``` is the sum of all the elements of ```theArray```.

### ```array array.select(func[x] f)```
Projects each element of a sequence into a new form.
```go
elementsLength := theArray.select(x => tostr(x).length);
```
In this example, ```elementsLength``` is an array containing all the lengths of the elements of ```theArray```.

### ```array array.where(func[x] f)```
Filters a sequence of values based on a predicate.
```go
whereResult := theArray.where(x => tostr(x).contains("e"));
```
Here, ```whereResult``` is an array containing all the elements of ```theArray``` that contains the letter **e**.

### ```bool array.any(func[x] f)```
Similar to the contains() function, it determines if any element of the array satisfies a contidion.
```go
containsGreaterThan7 := theArray.any(x => tostr(x).length > 7);
```
This example checks if ```theArray``` contains any element whose length is greater than 7.

### ```object array.first(func[x] f)```
Returns the first element of the array that matches the specified condition.
```go
firstOne := theArray.first(x => tostr(x).contains("d"));
```
In the code above, ```firstOne``` is the first element of ```theArray``` that contains the letter **d**.

### ```object array.last(func[x] f)```
Opposite of the ```array.first``` function, returns the last element of the array that matches the specified condition.
```go
lastOne := theArray.last(x => tostr(x).contains("f"));
```
In the code above, ```lastOne``` is the last element of ```theArray``` that contains the letter **f**.


### ```array array.zip(array other, func[x, y] f)```
Applies a specified function to the elements of two sequences, producing a sequence of results.
```go
test1 := [1, 2, 3, 4, 5];
test2 := [6, 7, 8, 9, 10];
result := test1.zip(test2, [x, y] => x + y);
```
Here, the ```zip``` function applies the function ```x + y``` to the elements of test1 and test2. ```x``` corresponds to test1 and ```y``` corresponds to test2 (it will do ```1 + 6```, ```2 + 7```, ```3 + 8```, etc). So, after running the code, ```result``` will be an array containing containing the sum of the elements of test1 and test2.