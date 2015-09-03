# Date functions

## Properties

#### ```number Date.year```
Returns the year parameter of the date object.
```go
println(date().year);
```

#### ```number Date.month```
Returns the month parameter of the date object.
```go
println(date().month);
```

#### ```number Date.day```
Returns the day parameter of the date object.
```go
println(date().day);
```

#### ```number Date.hour```
Returns the hour parameter of the date object.
```go
println(date().hour);
```

#### ```number Date.minute```
Returns the minute parameter of the date object.
```go
println(date().minute);
```

#### ```number Date.second```
Returns the second parameter of the date object.
```go
println(date().second);
```

#### ```bool Date.isLeapYear```
Returns true if the date is a leap year, false otherwise.
```go
if(date().isLeapYear)
{
	println("The current year is leap");
}
```