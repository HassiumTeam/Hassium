## class Thread

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` @desc A class representing a separate thread from the main entry point.

#### ```isalive { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly bool representing if this thread is currently running.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the thread is alive, otherwise false.

#### ```returns { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly return value of this thread (post run).

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The return value of this thread as object.

#### ```func start () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Starts this thread.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func stop () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Stops this thread.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

