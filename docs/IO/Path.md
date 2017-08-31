## class Path

#### ```func combine (params paths) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Combines the given file paths together and returns the file string.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@optional params paths:``` The list of paths to combine.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The resulting path string.

#### ```func getappdata () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the path to the ApplicationData folder.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The ApplicationData folder.

#### ```func getdocuments () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the path to the MyDocuments folder.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The MyDocuments folder.

#### ```func gethome () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the home folder of the currently logged in user.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The home folder.

#### ```func getstartup () : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the startup folder of the currently logged in user.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The startup folder.

#### ```func parsedir (path : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Parses the directory name of the specified path string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to parse.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The directory name of the path.

#### ```func parseext (path : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Parses The extension of the specified path string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to parse.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The extension of the file path.

#### ```func parsefilename (path : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Parses the file name of the specified path string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to parse.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The file name of the file path.

#### ```func parseroot (path : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Parses the root directory of the specified path string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param :``` path The path to parse.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The root directory of the file path.

