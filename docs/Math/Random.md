## class Random

#### ```func new () : Random```


```@desc:``` Constructs a new Random object using the optionally specified seed.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional seed:``` The int seed for the random object.
```@returns:``` The new Random object.

#### ```func new (seed : int) : Random```


```@desc:``` Constructs a new Random object using the optionally specified seed.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional seed:``` The int seed for the random object.
```@returns:``` The new Random object.

#### ```func randbytes (count : int) : list```


```@desc:``` Returns a new list with the specified count, filled with random bytes.

&nbsp;&nbsp;&nbsp;&nbsp;```@param count:``` The amount of random bytes to get.
```@returns:``` A new list of random bytes.

#### ```func randfloat () : float```


```@desc:``` Returns a random float.

```@returns:``` The random float.

#### ```func randint () : int```


```@desc:``` Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional low:``` The inclusive lower bound.
&nbsp;&nbsp;&nbsp;&nbsp;```@optional up:``` The non-inclusive upper bound.
```@returns:``` The random int.

#### ```func randint (up : int) : int```


```@desc:``` Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional low:``` The inclusive lower bound.
&nbsp;&nbsp;&nbsp;&nbsp;```@optional up:``` The non-inclusive upper bound.
```@returns:``` The random int.

#### ```func randint (low : int, up : int) : int```


```@desc:``` Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional low:``` The inclusive lower bound.
&nbsp;&nbsp;&nbsp;&nbsp;```@optional up:``` The non-inclusive upper bound.
```@returns:``` The random int.

