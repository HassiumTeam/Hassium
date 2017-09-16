## class RegEx

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class containing methods for interacting with regular expressions

#### ```func ismatch (re : string, str : string) : bool```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns a boolean indicating if the specified input string matches the specified regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param re:``` The regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The input string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` true if the string matches the pattern, otherwise false.

#### ```func match (re : string, str : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Returns the matches of the specified input string that meet the specified regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param re:``` The regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` the input string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A list containing the substrings that matched the pattern.

#### ```func replace (re : regex, inp : string, repl : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Replaces in the specified input string with the specified replacement string using the specified regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param re:``` The regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param inp:``` The input string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param repl:``` The replacement string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The string with replaced values.

#### ```func split (re : regex, str : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Splits the specified input string at the specified regex string and returns a list of substrings.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param re:``` The regex string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The input string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of split substrings.

