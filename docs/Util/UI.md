## class UI

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` 

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` 

#### ```backcolor { get; }```

#### ```backcolor { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string representing the background color of the terminal.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The background color string.

#### ```func beep () : null```

#### ```func beep (freq : int, milliseconds : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Causes the terminal to beep, optionally specifying the millisecond length and frequency.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param freq:``` The frequency as int.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param milliseconds:``` The milliseconds as int.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```capslock { get; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a readonly boolean indicating if the capslock is on.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if capslock is on, otherwise false.

#### ```func clear () : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Clears the terminal.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```cursorleft { get; }```

#### ```cursorleft { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int representing the left cursor position.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The left cursor position on the terminal.

#### ```cursorsize { get; }```

#### ```cursorsize { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int representing the cursor size.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The cursor size as int.

#### ```cursortop { get; }```

#### ```cursortop { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int representing the top cursor position.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The top cursor position on the terminal.

#### ```cursorvisible { get; }```

#### ```cursorvisible { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a mutable boolean indicating if the cursor is visible.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the cursor is visible, otherwise false.

#### ```forecolor { get; }```

#### ```forecolor { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable string representing the foreground color of the terminal.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The foreground color string.

#### ```title { get; }```

#### ```title { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable title of the terminal.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The title of the terminal as string.

#### ```windowheight { get; }```

#### ```windowheight { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable height of the terminal window.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The height of the terminal window as int.

#### ```windowleft { get; }```

#### ```windowleft { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int representing the left size of the terminal window.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The left size of the terminal window.

#### ```windowtop { get; }```

#### ```windowtop { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int representing the top size of the terminal window.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The top size of the terminal window.

#### ```windowwidth { get; }```

#### ```windowwidth { set; }```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the mutable int representing the width of the terminal window.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The width of the terminal window.

