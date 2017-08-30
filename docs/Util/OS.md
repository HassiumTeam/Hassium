## class OS

#### ```func exec (path : string, params args) : Process```


```@desc:``` Starts a new process at the specified path with the given args and returns the OS.Process object.

	```@param path:``` The path of the executable to execute.
	```@optional params args:``` The arguments to start the process with.
```@returns:``` A new OS.Process object that owns the started process.

#### ```func exit () : null```


```@desc:``` Exits Hassium with the optionally specified exitcode, default 0.

	```@optional code:``` The int exitcode.
```@returns:``` null.

#### ```func exit (code : int) : null```


```@desc:``` Exits Hassium with the optionally specified exitcode, default 0.

	```@optional code:``` The int exitcode.
```@returns:``` null.

#### ```exitcode { get; }```


```@desc:``` Gets the exit code.

```@returns:``` The exit code as int.

#### ```exitcode { set; }```


```@desc:``` Gets the exit code.

```@returns:``` The exit code as int.

#### ```func getvar (var : string) : string```


```@desc:``` Gets the environment variable at the specified var.

```@returns:``` The environment variable string at the var.

#### ```func getvars () : list```


```@desc:``` Gets a new list containing all of the environment variables.

```@returns:``` A new list of all the environment variables.

#### ```machinename { get; }```


```@desc:``` Gets the readonly name of this machine.

```@returns:``` The machine name as string.

#### ```netversion { get; }```


```@desc:``` Gets the readonly .NET version.

```@returns:``` The .NET version as string.

#### ```newline { get; }```


```@desc:``` Gets the readonly string of the system newline separator.

```@returns:``` The system's newline separator.

#### ```func setvar (var : string, val : string) : null```


```@desc:``` Sets the specified environment variable name with the specified value.

	```@param var:``` The string variable name to set.
	```@param val:``` The string value.
```@returns:``` null.

#### ```username```


```@desc:``` Gets the readonly logged on username.

```@returns:``` The string username.

#### ```version```


```@desc:``` Gets the readonly OS version.

```@returns:``` The OS version as string.

