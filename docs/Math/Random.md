## class Random

#### ```func new () : Random```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new Random object using the optionally specified seed.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional seed:``` The int seed for the random object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new Random object.

#### ```func new (seed : int) : Random```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new Random object using the optionally specified seed.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional seed:``` The int seed for the random object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new Random object.

#### ```func randbytes (count : int) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new list with the specified count, filled with random bytes.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param count:``` The amount of random bytes to get.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of random bytes.

#### ```func randfloat () : float```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a random float.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The random float.

#### ```func randint () : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional low:``` The inclusive lower bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional up:``` The non-inclusive upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The random int.

#### ```func randint (up : int) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional low:``` The inclusive lower bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional up:``` The non-inclusive upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The random int.

#### ```func randint (low : int, up : int) : int```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional low:``` The inclusive lower bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional up:``` The non-inclusive upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The random int.

