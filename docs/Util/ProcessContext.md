## ```class ProcessContext```

### ```func new ()```
Creates a new instance of the ProcessContext class.
### ```func new (path : string)```
Creates a new instance of the ProcessContext class using the specified path.
### ```func new (path : string, args : string)```
Creates a new instance of the ProcessContext class using the specified path and args.

### ```arguments { get { return this.arguments; } set { this.arguments = value; } }````
Returns the arguments for the context, sets the arguments for the context.

### ```createNoWindow { get { return this.createNoWindow; } set { this.createNoWindow = value; } }```
Gets the value indicating if the process will create no window, sets the value indicating if the process will create no window.

### ```environmentVariables { get { return this.environmentVariables; } }```
Returns a list of environment variables.

### ```filePath { get { return this.filePath; } set { this.filePath = value; } }```
Gets the file path of the context, sets the file path of the context.

### ```redirectStandardError { get { return this.redirectStandardError; } set { this.redirectStandardError = value; } }```
Gets the value indicating if the process will redirect standard error, sets the value indicating if the process will redirect standard error.

### ```redirectStandardInput { get { return this.redirectStandardInput; } set { this.redirectStandardInput = value; } }```
Gets the value indicating if the process will redirect standard input, sets the valu
e indicating if the process will redirect standard input.

### ```redirectStandardOutput { get { return this.redirectStandardOutput; } set { this.redirectStandardOutput = value; } }```
Gets the value indicating if the process will redirect standard output, sets the value indicating if the process will redirect standard output.

### ```useShellExecute { get { return this.useShellExecute; } set { this.useShellExecute = value; } }```
Gets the value indicating if the process will use shell execute, sets the value indicating if the process will use shell execute.
