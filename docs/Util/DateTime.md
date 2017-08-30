## class DateTime

#### ```day { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int day.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The day as int.

#### ```dayofweek { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int dayofweek (1-7).

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The day of week as int.

#### ```dayofyear { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int dayofyear.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The day of year as int.

#### ```hour { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly hour.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The hour as int.

#### ```func new (year : int, month : int, day : int) : DateTime```

#### ```func new (year : int, month : int, day : int, hour : int, min : int, sec : int) : DateTime```

#### ```func new (year : int, month : int, day : int, hour : int, min : int, sec : int, millisecond : int) : DateTime```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new DateTime object using the specified year, month, day, and optional hour, min, second, and millisecond integers.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param year:``` The int year.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param month:``` The int month (1-12).

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param day:``` The int day.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional hour:``` The int hour.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional minute:``` The int minute.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional second:``` The int second.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional millisecond:``` The int millisecond.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new DateTime object.

#### ```millisecond { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly millisecond.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The millisecond as int.

#### ```minute { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly minute.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The minute as int.

#### ```month { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly month.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The month as int.

#### ```now { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a new DateTime object with the values for date and time based off of the system clock.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new DateTime object with the current date and time.

#### ```second { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly second.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The second as int.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the string value of this date and time.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the DateTime.

#### ```year { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly year.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The year as int.

