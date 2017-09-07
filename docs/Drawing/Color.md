## class Color

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a Color object.

#### ```a { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly alpha value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` a as int.

#### ```argb { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly argb value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` argb as int.

#### ```b { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly blue value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` b as int.

#### ```g { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly green value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` g as int.

#### ```func new (colIntOrStr : object) : Color```

#### ```func new (r : int, g : int, b : int) : Color```

#### ```func new (a : int, r : int, g : int, b : int) : Color```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new Color with either the specified color name, argb, specified r, g, b, or specified a, r, g, b.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional colIntOrStr:``` The color name string or argb int.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional a:``` The alpha value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional r:``` The red value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional g:``` The green value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional b:``` The blue value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new Color object.

#### ```r { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly red value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` r as int.

