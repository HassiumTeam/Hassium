## class ArgLengthException

#### ```expected { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int representing how many args were expected.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The expected arg length as int.

#### ```function { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly object who was passed improper args.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The function object.

#### ```given { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the readonly int representing how many args were given.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The given arg length as int.

#### ```func new (fn : object, expected : int, given : int) : ArgLengthException```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Constructs a new ArgLengthException using the specified function object, the specified given arg length, and the specified expected arg length.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param fn:``` The function object that was given improper args.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param expected:``` The int representing how many args were expected.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param given:``` The int representing how many args were actually given.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new ArgLengthException object.

#### ```func tostring () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the string value of the exception, including the message and callstack.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string value of the exception.

