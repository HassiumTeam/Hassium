## ```class Process```

### ```func getProcessByID (id : int) : Process```
Returns a process that has the specified id value.

### ```func getProcessByName (name : string) : Process```
Returns a process that has the specified name.

### ```func getProcessList () : list```
Returns a list of the currently executing Processes.

### ```ID { get { return this.ID; } set { this.ID = value; } }```
Gets the ID of the process, sets the ID of the process.

### ```func isProcessRunning (id : int) : bool```
Returns a bool indicating if the process at the specified id is running.
### ```func isProcessRunning (name : string) : bool```
Returns a bool indicating if the process with the specified name is running.

### ```func kill () : null```
Kills the process.

### ```func killProcess (name : string) : bool```
Tries to kill the process with the specified name and returns a bool indicating
if it successfully killed.

### ```name { get { return this.name; } }```
Gets the name of the process.

### ```func start (path : string) : null```
Starts the process at the specified path.
### ```func start (context : ProcessContext) : null```
Starts the process using the specified ProcessContext.
### ```func start (path : string, arguments : string) : null```
Starts the process at the specified path, using the specified argument string.

### ```func toString () : string```
Returns the string name of the Process.
