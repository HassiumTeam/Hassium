## class OS

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` @desc A class containing methods for interacting with the operating system, environment, and processes.

#### ```func exec (path : string, params args) : Process```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Starts a new process at the specified path with the given args and returns the OS.Process object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path of the executable to execute.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params args:``` The arguments to start the process with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new OS.Process object that owns the started process.

#### ```func exit () : null```

#### ```func exit (code : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Exits Hassium with the optionally specified exitcode, default 0.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional code:``` The int exitcode.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```exitcode { get; }```

#### ```exitcode { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the exit code.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The exit code as int.

#### ```func getvar (var : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the environment variable at the specified var.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The environment variable string at the var.

#### ```func getvars () : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a new list containing all of the environment variables.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of all the environment variables.

#### ```machinename { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly name of this machine.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The machine name as string.

#### ```netversion { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly .NET version.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The .NET version as string.

#### ```newline { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly string of the system newline separator.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The system's newline separator.

#### ```func setvar (var : string, val : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Sets the specified environment variable name with the specified value.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param var:``` The string variable name to set.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The string value.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```username```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly logged on username.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string username.

#### ```version```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly OS version.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The OS version as string.

