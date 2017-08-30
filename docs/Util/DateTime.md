## class DateTime

#### ```day { get; }```

```@desc:``` Gets the readonly int day.

```@returns:``` The day as int.

#### ```dayofweek { get; }```

```@desc:``` Gets the readonly int dayofweek (1-7).

```@returns:``` The day of week as int.

#### ```dayofyear { get; }```

```@desc:``` Gets the readonly int dayofyear.

```@returns:``` The day of year as int.

#### ```hour { get; }```

```@desc:``` Gets the readonly hour.

```@returns:``` The hour as int.

#### ```func new (year : int, month : int, day : int) : DateTime```

```@desc:``` Constructs a new DateTime object using the specified year, month, day, and optional hour, min, second, and millisecond integers.

&nbsp;&nbsp;&nbsp;&nbsp;```@param year:``` The int year.

&nbsp;&nbsp;&nbsp;&nbsp;```@param month:``` The int month (1-12).

&nbsp;&nbsp;&nbsp;&nbsp;```@param day:``` The int day.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional hour:``` The int hour.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional minute:``` The int minute.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional second:``` The int second.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional millisecond:``` The int millisecond.

```@returns:``` The new DateTime object.

#### ```func new (year : int, month : int, day : int, hour : int, min : int, sec : int) : DateTime```

```@desc:``` Constructs a new DateTime object using the specified year, month, day, and optional hour, min, second, and millisecond integers.

&nbsp;&nbsp;&nbsp;&nbsp;```@param year:``` The int year.

&nbsp;&nbsp;&nbsp;&nbsp;```@param month:``` The int month (1-12).

&nbsp;&nbsp;&nbsp;&nbsp;```@param day:``` The int day.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional hour:``` The int hour.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional minute:``` The int minute.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional second:``` The int second.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional millisecond:``` The int millisecond.

```@returns:``` The new DateTime object.

#### ```func new (year : int, month : int, day : int, hour : int, min : int, sec : int, millisecond : int) : DateTime```

```@desc:``` Constructs a new DateTime object using the specified year, month, day, and optional hour, min, second, and millisecond integers.

&nbsp;&nbsp;&nbsp;&nbsp;```@param year:``` The int year.

&nbsp;&nbsp;&nbsp;&nbsp;```@param month:``` The int month (1-12).

&nbsp;&nbsp;&nbsp;&nbsp;```@param day:``` The int day.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional hour:``` The int hour.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional minute:``` The int minute.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional second:``` The int second.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional millisecond:``` The int millisecond.

```@returns:``` The new DateTime object.

#### ```millisecond { get; }```

```@desc:``` Gets the readonly millisecond.

```@returns:``` The millisecond as int.

#### ```minute { get; }```

```@desc:``` Gets the readonly minute.

```@returns:``` The minute as int.

#### ```month { get; }```

```@desc:``` Gets the readonly month.

```@returns:``` The month as int.

#### ```now { get; }```

```@desc:``` Returns a new DateTime object with the values for date and time based off of the system clock.

```@returns:``` The new DateTime object with the current date and time.

#### ```second { get; }```

```@desc:``` Gets the readonly second.

```@returns:``` The second as int.

#### ```func tostring () : string```

```@desc:``` Gets the string value of this date and time.

```@returns:``` The string value of the DateTime.

#### ```year { get; }```

```@desc:``` Gets the readonly year.

```@returns:``` The year as int.

