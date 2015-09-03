# IO Functions

## File Class

#### null File.writeText(string path, string contents)
Writes the contents to the file path.
```
File.writeText("/home/default/test.txt", "These are some file contents");
```

#### string File.readText(string path)
Reads the text from the file path and returns it.
```
contents := File.readText("/home/default/test.txt");
println("Contents are: ", contents);
```

#### array File.readLines(string path)
Returns the lines from a file into an array.
```
lines := File.readLines("/home/default/test.txt");
println("The file is: ", lines.length, " lines long.");
```

#### bool File.exists(string path)
Returns true if the file exists or returns false if the file does not exist.
```
if (File.exists(args[0]))
	println("File exists!");
else
	println("File does not exist!");
```

#### null File.create(string path)
Creates a file at the specified path.
```
File.create("/home/default/test.txt");
```

#### null File.append(string path, string text)
Appends the specified text to the file at the path.
```
File.append("/home/default/test.txt", "This is now the last line.");
```

#### null File.appendLines(string path, array lines)
Appends the lines from the string array to the file path.
```
File.appendLines("/home/default/test.txt", args);
```

#### null File.copy(string sourcePath, string destinationPath)
Copies the file from the specified source path to the specified destination path.
```
File.copy("/home/default/test.txt", "/home/default/testCopy.txt");
```

#### null File.move(string sourcePath, string destinationPath)
Moves the file from the specified source path to the specified destination path.
```
File.copy("/home/default/test.txt", "/home/default/newTest.txt");
```

#### null File.rename(string path, string newName)
Renames the file at the specified path to the new name specified.
```
File.rename("/home/default/test.tx", "/home/default/test.txt")
```

#### null File.deleteFile(string filePath)
Deletes the file at the specified path.
```
File.deleteFile("/home/default/test.txt");
```

#### null File.deleteDirectory(string folderPath)
Deletes the folder at the specified path.
```
File.deleteDirectory("/");
```

#### string File.getDirectory()
Returns the current working directory.
```
println("Current directory is: ". File.getDirectory());
```

#### null File.setDirectory(string directory)
Changes the current working directory to the specified directory.
```
println("Current directory is: ". File.getDirectory());
File.setDirectory("/");
println("Current directory is: ". File.getDirectory());
```

#### array File.getFiles(string directory)
Returns all of the files in the specified directory in an array.
```
println("The files in the current directory are: ");
foreach (file in File.getFiles(File.getDirectory()))
	println(file);
```

#### array File.getDirectories(string directory)
Returns all of the directories in the specified directory in an array.
```
println("The directories in the current directory are: ");
foreach (dir in File.getDirectories(File.getDirectory()))
	println(dir);
```

## Directory Class

#### bool Directory.exists(string directory)
Returns true if the directory specified exists, otherwise returns false.
```
if (Directory.exists(args[0]))
	println("Directory exists!");
else
	println("Directory does not exist!");
```

#### null Directory.create(string newPath)
Creates a new directory at the specified path.
```
Directory.create("/home/default/pr0nz");
```

#### null Directory.copy(string oldPath, string newPath)
Copies the directory at the specified old path to the specified new path.
```
Directory.copy("/home/default", "/home/newDefault");
```

#### null Directory.rename(string path, string newName)
Renames the directory at the specified path to the new name specified.
```
Directory.rename("/home/defaul", "/home/default");
```

#### null Directory.delete(string path)
Deletes the directory at the specified path.
```
Directory.delete("/");
```
