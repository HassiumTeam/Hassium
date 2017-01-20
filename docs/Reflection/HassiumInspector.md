## ```class HassiumInspector```

### ```func compileModuleFromSource (source : string) : module```
Runs the Hassium compiler on the given source code and returns the module produced.

### ```func getCurrentModule () : module```
Returns the currently executing module in the VM.

### ```func new (obj : object)```
Creates a new instance of the HassiumInspector class.

### ```func getObjectsByType (type : TypeDefinition) : list```
Returns a list of all the object attributes in the Object that are of the specified type.

### ```func getParent () : object```
Returns the parent object of the Object.
