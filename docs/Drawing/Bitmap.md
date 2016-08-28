## ```class Bitmap```

### ```func new (path : string)```
Creates a new instance of the Bitmap class from the specified file path.
### ```func new (height : int, width : int)```
Creates a new instance of the Bitmap class from the specified height and width integers.

### ```func getPixel (x : int, y : int) : Color```
Returns the Color at the specified x and y coordinates.

### ```height { get { return this.height; } }```
Gets the integer value of the Bitmap height.

### ```horizontalResolution { get { return this.horizontalResolution; } }```
Gets the float value of the Bitmap horizontal resolution.

### ```func makeTransparent (color : Color) : null```
Makes the specified Color transparent in the Bitmap.

### ```func save (path : string) : null```
Saves the Bitmap to the specified file path.

### ```func setPixel (x : int, y : int, color : Color) : null```
Sets the pixel at the specified x and y coordinates to the specified Color.

### ```func setResolution (x : float, y : float) : null```
Sets the resolution of the bitmap to the specified x and y values.

### ```verticalResolution { get { return this.verticalResolution; } }```
Gets the float value of the Bitmap vertical resolution.

### ```width { get { return this.width; } }```
Gets the integer value of the Bitmap width.
