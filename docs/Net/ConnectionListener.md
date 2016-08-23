## ```class ConnectionListener```

### ```func new (ip : string, port : int)```
Creates a new instance of the ConnectionListener class on the specified
ip and port.

### ```func acceptConnection () : NetConnection```
Waits until a new connection is encountered and returns the incoming NetConnection.

### ```func pending () : bool```
Returns a bool indicating if the ConnectionListener is pending.

### ```func start () : null```
Starts the ConnectionListener.

### ```func stop () : null```
Stops the ConnectionListener.
