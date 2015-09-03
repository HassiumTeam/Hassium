# Array functions	

## Base functions

#### ```number Array.length```
*(property)*
Returns the number of elements in the Array.
```go
println("There is ", myArray.length, " elements in the array.");
```

#### ```string Array.toString()```
Converts the array to a readable string with the form ```Array { item1, item2, item3 }```
```go
println(theArray.toString());
```

#### ```void Array.add(object item)```
Adds the specified item to the Array.
```go
theArray.add(myItem);
```

#### ```bool Array.remove(object item)```
Removes the specified item to the Array. Returns true if the item has successfully been removed, otherwise false.
```go
theArray.remove(myItem);
```

#### ```array Array.resize(number length)```
Resizes the array and returns it.
```go
newArray := oldArray.resize(5);
```

#### ```string Array.join(string separator [default: ""])```
Concatenates the elements of the array using a specified separator (empty by default).
```go
println(theArray.join(","));
```

#### ```bool Array.contains(object item)```
Determines if the array contains the specified item.
```go
if (thePeople.contains("George Clooney")) {
	println("George Clooney's inside!");
}
```

## LINQ functions

#### ```object Array.op(func[x, y] f)```
Performs an operation on each element of the array and return the final result.
```go
totalSum := theArray.op([x, y] => x + y);
``` 
Here, ```totalSum``` is the sum of all the elements of ```theArray```.

#### ```array Array.select(func[x] f)```
Projects each element of a sequence into a new form.
```go
cities := employees.select(x => x.city);
```
In this example, ```cities``` is an array containing the city of each employee.

#### ```array Array.where(func[x] f)```
Filters a sequence of values based on a predicate.
```go
newYorkers := students.where(x => x.city = "New York");
```
Here, ```newYorkers``` is an array containing all the students that live in New York.

#### ```bool Array.any(func[x] f)```
Similar to the contains() function, it determines if any element of the array satisfies a contidion.
```go
if (theArray.any(x => tostr(x).length < 7)) {
	println("The numbers must be greater than 7.");
}
```
This example checks if ```theArray``` contains any element whose length is greater than 7.

#### ```object Array.first(func[x] f)```
Returns the first element of the array that matches the specified condition.
```go
firstOne := theArray.first(x => tostr(x).contains("d"));
```
In the code above, ```firstOne``` is the first element of ```theArray``` that contains the letter **d**.

#### ```object Array.last(func[x] f)```
Opposite of the ```Array.first``` function, returns the last element of the array that matches the specified condition.
```go
lastOne := theArray.last(x => tostr(x).contains("f"));
```
In the code above, ```lastOne``` is the last element of ```theArray``` that contains the letter **f**.


#### ```array Array.zip(array other, func[x, y] f)```
Applies a specified function to the elements of two sequences, producing a sequence of results.
```go
test1 := ["A", "B", "C", "D", "E"];
test2 := [1, 2, 3, 4, 5];
result := test1.zip(test2, [x, y] => x + y);
```
Here, the ```zip``` function applies the function ```x + y``` to the elements of test1 and test2. ```x``` corresponds to test1 and ```y``` corresponds to test2 (it will do ```A + 1```, ```B + 2```, etc). So after running the code, result is 
```go
["A1", "B2", "C3", "D4", "E5"];
```