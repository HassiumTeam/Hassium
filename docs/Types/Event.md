## ```class Event```

### ```func new ()```
Creates a new instance of the Event class.

### ```func new (func1 : func, func2 : func, func3 : func, ...)```
Creates a new instance of the Event class, using the given invokables.

### ```func add (f : func) : func```
Adds the given func to the list of events.

### ```func clear () : null```
Clears all of the events.

### ```func fire () : list```
Fires all of the events and puts their return values in a resulting list.

### ```func remove (f : func) : null```
Removes the given func from the list of events.
