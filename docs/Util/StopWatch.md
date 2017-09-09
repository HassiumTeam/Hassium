## class StopWatch

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a StopWatch object.

#### ```hours { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly hours that have passed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The elapsed hours as int.

#### ```func new () : StopWatch```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new StopWatch object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new StopWatch object.

#### ```isrunning { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly boolean indicating if the stopwatch is running.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if this StopWatch is running, otherwise false.

#### ```milliseconds { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly milliseconds that have passed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The elapsed milliseconds as int.

#### ```minutes { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly minutes that have passed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The elapsed minutes as int.

#### ```func restart () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Restarts this stopwatch.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func reset () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Resets this stopwatch.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```seconds { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly seconds that have passed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The elapsed seconds as int.

#### ```func start () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Starts this stopwatch.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func stop () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Stops this stopwatch.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```ticks { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly ticks that have passed.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The elapsed ticks as int.

