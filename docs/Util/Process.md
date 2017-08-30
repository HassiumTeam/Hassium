## class Process

#### ```args { get; }```

```@desc:``` Gets the mutable argument string.

```@returns:``` The argument string.

#### ```args { set; }```

```@desc:``` Gets the mutable argument string.

```@returns:``` The argument string.

#### ```createwindow { get; }```

```@desc:``` Gets the mutable boolean indicating if a window will be created.

```@returns:``` true if a new window will be created, otherwise false.

#### ```createwindow { set; }```

```@desc:``` Gets the mutable boolean indicating if a window will be created.

```@returns:``` true if a new window will be created, otherwise false.

#### ```func new (path : string, args : string```

```@desc:``` Constructs a new Process object using the specified process path and the given argument string.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to the executable.

&nbsp;&nbsp;&nbsp;&nbsp;```@param args:``` The argument string.

```@returns:``` The new Process object.

#### ```path { get; }```

```@desc:``` Gets the mutable string to the executable path.

```@returns:``` The path to the executable as string.

#### ```path { set; }```

```@desc:``` Gets the mutable string to the executable path.

```@returns:``` The path to the executable as string.

#### ```shellexecute { get; }```

```@desc:``` Gets the mutable boolean indicating if the process will use a shell execute.

```@returns:``` true if the process will use shell execute, otherwise false.

#### ```shellexecute { set; }```

```@desc:``` Gets the mutable boolean indicating if the process will use a shell execute.

```@returns:``` true if the process will use shell execute, otherwise false.

#### ```func start () : null```

```@desc:``` Starts the process.

```@returns:``` null.

#### ```func stop () : null```

```@desc:``` Stops the process.

```@returns:``` null.

#### ```username { get; }```

```@desc:``` Gets the mutable string username for the process to execute with.

```@returns:``` The string username.

#### ```username { set; }```

```@desc:``` Gets the mutable string username for the process to execute with.

```@returns:``` The string username.

