## ```class thread```

### ```isAlive { get { return this.isAlive; } }```
Returns a bool indicating if the thread is active or not.

### ```returnValue { get { return this.returnValue; } }```
Gets the return value of the executing thread.

### ```func start () : null```
Starts the thread. If the thread has a return value, then the ```returnValue```
property will contain it.

### ```func stop () : null```
Stops the thread.
