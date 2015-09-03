# Dictionary functions	

## Base functions

#### ```number Dictionary.length```
*(property)*
Returns the number of elements in the Dictionary.
```go
println("There is ", myDict.length, " elements in the dictionary.");
```

#### ```string Dictionary.toString()```
Converts the dictionary to a readable string with the form ```Dictionary { [key] => value, [key] => value }```
```go
println(theDict.toString());
```

#### ```void Dictionary.add(keyValuePair item)```
Adds the specified item to the Dictionary.
```go
theDict.add(myItem);
```

#### ```bool Dictionary.remove(keyValuePair item)```
Removes the specified item to the Dictionary. Returns true if the item has successfully been removed, otherwise false.
```go
theDict.remove(myItem);
```

#### ```dictionary Dictionary.resize(number length)```
Resizes the dictionary and returns it.
```go
newDict := oldDict.resize(5);
```

#### ```bool Dictionary.contains(keyValuePair item)```
Determines if the dictionary contains the specified item.
```go
if (thePeople.contains(myKvp)) {
	println("Yay!");
}
```

#### ```bool Dictionary.containsKey(object item)```
Determines if the dictionary contains the an item with the specified key.
```go
if (thePeople.containsKey("George Clooney")) {
	println("George Clooney's inside!");
}
```

#### ```bool Dictionary.containsValue(object item)```
Determines if the dictionary contains the an item with the specified key.
```go
if (!thePeople.containsValue("Cake")) {
	println("The cake is a lie.");
}
```

## LINQ functions

#### ```object Dictionary.op(func[x, y] f)```
Performs an operation on each element of the dictionary and return the final result.
```go
totalSum := theDict.op([x, y] => x.value + y.value);
``` 
Here, ```totalSum``` is the sum of all the elements of ```theDict```.

#### ```dictionary Dictionary.select(func[x] f)```
Projects each element of a sequence into a new form.
```go
cities := employees.select(x => x.city);
```
In this example, ```cities``` is an dictionary containing the city of each employee.

#### ```dictionary Dictionary.where(func[x] f)```
Filters a sequence of values based on a predicate.
```go
newYorkers := students.where(x => x.city = "New York");
```
Here, ```newYorkers``` is an dictionary containing all the students that live in New York.

#### ```bool Dictionary.any(func[x] f)```
Similar to the contains() function, it determines if any element of the dictionary satisfies a contidion.
```go
if (theDict.any(x => tostr(x).length < 7)) {
	println("The numbers must be greater than 7.");
}
```
This example checks if ```theDict``` contains any element whose length is greater than 7.

#### ```object Dictionary.first(func[x] f)```
Returns the first element of the dictionary that matches the specified condition.
```go
firstOne := theDict.first(x => tostr(x).contains("d"));
```
In the code above, ```firstOne``` is the first element of ```theDict``` that contains the letter **d**.

#### ```object Dictionary.last(func[x] f)```
Opposite of the ```Dictionary.first``` function, returns the last element of the dictionary that matches the specified condition.
```go
lastOne := theDict.last(x => tostr(x).contains("f"));
```
In the code above, ```lastOne``` is the last element of ```theDict``` that contains the letter **f**.


#### ```dictionary Dictionary.zip(dictionary other, func[x, y] f)```
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