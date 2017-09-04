## class globals

#### ```func clone (obj : object) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Creates a clone of the given object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to clone.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The cloned object.

#### ```func eval (src : string) : module```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Compiles the given string of Hassium source and returns a module.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param src:``` The string Hassium source.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The compiled Hassium module.

#### ```func format (fmt : string, params obj) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` C# formats the given format string with a list of arguments.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param fmt:``` The format string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params obj:``` The list of format arguments.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The resulting formatted string.

#### ```func getattrib (obj : object, attrib : string) : object```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the specified attribute from the given object by name.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object containing the attributes.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param attrib:``` The name of the desired attribute

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The value of the attribute.

#### ```func getattribs (obj : object) : dict```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a dict containing the attributes in { string : object } format of the given object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object to get attributes from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A dictionary with the attributes.

#### ```func getdocdesc (obj : object) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the @desc parameter of documentation for a function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get documentation for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The documentation description.

#### ```func getdocoptparams (obj : object) list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the @optional parameters of documentation for a function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get documentation for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list of the documentation optional parameters.

#### ```func getdocreqparams (obj : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the @param parameters of documentation for a function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get documentation for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list of the documentation parameters.

#### ```func getdocreturns (obj : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the @returns parameter of documentation for a function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get documentation for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The documentation returns.

#### ```func getparamlengths (obj : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a list of possible parameter lengths for the given function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get parameter lengths for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The parameter lengths.

#### ```func getsourcerep (obj : object) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the string source representation for the given function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get source representation for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The source representation.

#### ```func getsourcereps (obj : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a list of the possible source representations for the given function.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get source representations for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The source representations.

#### ```func hasattrib (obj : object, attrib : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a bool indicating if the given object contains the specified attribute.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object whose attributes to check.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param attrib:``` The attribute name to chech.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if obj contains attrib, otherwise false.

#### ```func help (obj : object) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Prints to stdout a helpdoc for the given function using the sourcerep and docs.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The function to get help for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func input () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Reads a line from stdin and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string line.

#### ```func map (l : list, f : func) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Iterates over the given list, adding to a new list the result of invoking F() with each list element.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param l:``` The list of input values.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param f:``` The function to operate with.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new list of results.

#### ```func print (params obj) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the string value of the given objects to stdout.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params obj:``` List of objects to print.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func printf (strf : string, params obj) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the string value of the result of formatting the given format string with the given format args.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param strf:``` The format string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params obj:``` The list of format arguments.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func println (params obj) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Writes the string value of the given objects to stdout, each followed by a newline.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params obj:``` List of objects to print.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func range (upper : int) : list```

#### ```func range (lower : int, upper : int) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a list of every number between 0 or a specified lower bound, and the specified upper bound.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param upper:``` The upper bound (non-inclusive).

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional lower:``` The lower bound (inclusive).

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The new list of values.

#### ```func setattrib (obj : object, attrib : string, val : object) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Sets the value of specified attribute to the specified value in the given object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object whose attributes to modify.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param attrib:``` The name of the attribute to set.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param val:``` The value of the attribute to set.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func sleep (milliseconds : int) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Stops the current thread for the specified number of milliseconds.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param milliseconds:``` The amount of milliseconds to sleep for.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func type (obj : object) : typedef```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the typedef for the given object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object whose type to get.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The typedef of the object.

#### ```func types (obj : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a list of typedefs for the given object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param obj:``` The object whose types to get.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of typedefs.

