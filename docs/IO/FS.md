## ```class FS```

### ```func combinePath (str1 : string, str2 : string, str3 : string ...) : string```
Combines the string paths given and returns the combined path.

### ```func createDirectory (path : string) : null```
Creates a directory at the specified path.

### ```func createFile (path : string) : null```
Creates a file at the specified path.

### ```currentDirectory { get { return this.currentDirectory; } set { this.currentDirectory = value; } }```
Returns the string current working directory, sets the current working directory to the value.

### ```func delete (path : string) : null```
Tries to delete a file at the path, then a directory at path.

### ```func deleteDirectory (path : string) : null```
Deletes the directory at the specified path.

### ```func deleteFile (path : string) : null```
Deletes the file at the specified path.

### ```func exists (path : string) : bool```
Returns true if there is an existing file or directory at the specified path.

### ```func fileExists (path : string) : bool```
Returns a bool indicating if there is a file at the specified path.

### ```func getDirectoryList (path : string) : list```
Returns a list of strings containing the paths of the directories at the specified path.

### ```func getFileList (path : string) : list```
Returns a list of strings containing the paths of the files at the specified path.

### ```func getTempFile () : string```
Returns a temporary file path.

### ```func getTempPath () : string```
Returns a temporary directory path.

### ```func readBytes (path : string) : list```
Returns a list of chars containing all the bytes at the specified path.

### ```func readLines (path : string) : list```
Returns a list of strings containing all the newline speerated lines at the specified path.

### ```func readString (path : string) : string```
Returns the string of all the text at the specified path.

### ```func parseDirectoryName (path : string) : string```
Returns the directory name contained inside the path string.

### ```func parseExtension (path : string) : string```
Returns the extension contained inside the path string.

### ```func parseFileName (path : string) : string```
Returns the file name contained inside the path string.

### ```func parseFileNameWithoutExtension (path : string) : string```
Returns the file name without the extension inside the path string.

### ```func parseRoot (path : string) : string```
Returns the root of the path string.

### ```func writeBytes (path : string, bytes : list) : null```
Writes all of the elements in the list as bytes to the file at path.

### ```func writeLines (path : string, lines : list) : null```
Writes all of the elements in the list as string lines to the file at path.

### ```func writeString (path : string, text : string) : null```
Writes the string to the file at path.
