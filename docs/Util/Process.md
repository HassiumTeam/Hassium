## class Process

#### ```args { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable argument string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The argument string.

#### ```args { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable argument string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The argument string.

#### ```createwindow { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable boolean indicating if a window will be created.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if a new window will be created, otherwise false.

#### ```createwindow { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable boolean indicating if a window will be created.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if a new window will be created, otherwise false.

#### ```func new (path : string, args : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new Process object using the specified process path and the given argument string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to the executable.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param args:``` The argument string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new Process object.

#### ```path { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string to the executable path.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The path to the executable as string.

#### ```path { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string to the executable path.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The path to the executable as string.

#### ```shellexecute { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable boolean indicating if the process will use a shell execute.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the process will use shell execute, otherwise false.

#### ```shellexecute { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable boolean indicating if the process will use a shell execute.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the process will use shell execute, otherwise false.

#### ```func start () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Starts the process.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func stop () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Stops the process.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```username { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string username for the process to execute with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string username.

#### ```username { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string username for the process to execute with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string username.

