## class ArgLengthException

#### ```expected { get; }```


```@desc:``` Gets the readonly int representing how many args were expected.

```@returns:``` The expected arg length as int.

#### ```function { get; }```


```@desc:``` Gets the readonly object who was passed improper args.

```@returns:``` The function object.

#### ```given { get; }```


```@desc:``` Gets the readonly int representing how many args were given.

```@returns:``` The given arg length as int.

#### ```func new (fn : object, expected : int, given : int) : ArgLengthException```


```@desc:``` Constructs a new ArgLengthException using the specified function object, the specified given arg length, and the specified expected arg length.

	```@param fn:``` The function object that was given improper args.
	```@param expected:``` The int representing how many args were expected.
	```@param given:``` The int representing how many args were actually given.
```@returns:``` The new ArgLengthException object.

#### ```func tostring () : string```


```@desc:``` Returns the string value of the exception, including the message and callstack.

```@returns:``` The string value of the exception.

