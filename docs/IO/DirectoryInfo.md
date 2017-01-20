## ```class DirectoryInfo```

### ```func new (path : string)```
Creates a new instance of the DirectoryInfo class using the specified path string.

### ```creationTime { get { return this.creationTime; } set { this.creationTime = value; } }```
Gets or sets the DateTime creation time of the directory.

### ```extension { get { return this.extension; } }```
Gets the extension of the directory.

### ```func getDirectories () : list```
Returns a new string list of all the sub directories of the directory.

### ```func getFiles () : list```
Returns a new string list of all the files in the directory.

### ```func move (dest : string) : null```
Moves the directory and the contents to the specified destination path.

### ```name { get { return this.name; } }```
Gets the string name of the directory.

### ```parent { get { return this.parent; } }```
Gets the DirectoryInfo for the parent directory.

### ```root { get { return this.root; } }```
Gets the DirectoryInfo for the root directory.
