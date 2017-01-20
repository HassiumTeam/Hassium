## ```class OS```

### ```func exit (code : int) : null```
Exits the application using the given exit code.

### ```exitCode { get { return this.exitCode; } set { this.exitCode = value; } }```
Returns the exit code, sets the exit code to the value.

### ```getCommandLineArgs () : list```
Returns a new list of all the command line arguments used to start Hassium.

### ```getEnvironmentVariable (var : string) : string```
Returns the variable value for the specified variable.

### ```getEnvironmentVariables () : dictionary```
Returns a new dictionary containing all of the variables and values for environment variables.

### ```machineName { get { return this.machineName; } }```
Returns the name of the machine,

### ```newLine { get { return this.newLine; } }```
Returns the newline terminator for the OS.

### ```OSVersion { get { return this.OSVersion; } }```
Returns the Operating System version.

### ```processorCount { get { return this.processorCount; } }```
Returns the processor count.

### ```userName { get { return this.userName; } }```
Returns the name of the currently logged on user.

### ```version { get { return this.version; } }```
Returns the version of .NET.
