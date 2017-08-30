## class Path

#### ```func combine (params paths) : string```


```@desc:``` Combines the given file paths together and returns the file string.

```	@optional: params paths:``` The list of paths to combine.
```@returns:``` The resulting path string.

#### ```func getappdata () : string```


```@desc:``` Gets the path to the ApplicationData folder.

```@returns:``` The ApplicationData folder.

#### ```func getdocuments () : string```


```@desc:``` Gets the path to the MyDocuments folder.

```@returns:``` The MyDocuments folder.

#### ```func gethome () : string```


```@desc:``` Gets the home folder of the currently logged in user.

```@returns:``` The home folder.

#### ```func getstartup () : string```


```@desc:``` Gets the startup folder of the currently logged in user.

```@returns:``` The startup folder.

#### ```func parsedir (path : string) : string```


```@desc:``` Parses the directory name of the specified path string and returns it.

```	@param: path:``` The path to parse.
```@returns:``` The directory name of the path.

#### ```func parseext (path : string) : string```


```@desc:``` Parses The extension of the specified path string and returns it.

```	@param: path:``` The path to parse.
```@returns:``` The extension of the file path.

#### ```func parsefilename (path : string) : string```


```@desc:``` Parses the file name of the specified path string and returns it.

```	@param: path:``` The path to parse.
```@returns:``` The file name of the file path.

#### ```func parseroot (path : string) : string```


```@desc:``` Parses the root directory of the specified path string and returns it.

```	@param: :``` path The path to parse.
```@returns:``` The root directory of the file path.

