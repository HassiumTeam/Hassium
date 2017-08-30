## class Bitmap

#### ```func getpixel (x : int, y : int) : Color```


```@desc:``` Returns a new Drawing.Color object with the value of the pixel at the specified x and y coordinates.

&nbsp;&nbsp;&nbsp;&nbsp;```@param x:``` The x coordinate.

&nbsp;&nbsp;&nbsp;&nbsp;```@param y:``` The y coordinate.

```@returns:``` The color at (x, y).

#### ```height { get; }```


```@desc:``` Gets the readonly height of the Bitmap in pixels.

```@returns:``` Height as int.

#### ```hres { get; }```


```@desc:``` Gets the readonly horizontal resolution of the Bitmap.

```@returns:``` Horizontal resolution as int.

#### ```func new (path : string) : Bitmap```


```@desc:``` Constructs a new Bitmap with either the specified string name or the specified height and width.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional path:``` The file path on disc to the bitmap.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional height:``` The height in pixels for the new bitmap.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional width:``` The width in pixels for the new bitmap.

```@returns:``` The new Bitmap object.

#### ```func new (height : int, width : int) : Bitmap```


```@desc:``` Constructs a new Bitmap with either the specified string name or the specified height and width.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional path:``` The file path on disc to the bitmap.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional height:``` The height in pixels for the new bitmap.

&nbsp;&nbsp;&nbsp;&nbsp;```@optional width:``` The width in pixels for the new bitmap.

```@returns:``` The new Bitmap object.

#### ```func save (path : string) : null```


```@desc:``` Saves this Bitmap to the specified path on disc.

&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The path to save the bitmap to.

```@returns:``` null.

#### ```func setpixel (x : int, y : int, col : Color) : null```


```@desc:``` Sets the value of the pixel at the specified x and y coorinates to the given Drawing.Color object.

&nbsp;&nbsp;&nbsp;&nbsp;```@param x:``` The x coordinate.

&nbsp;&nbsp;&nbsp;&nbsp;```@param y:``` The y coordinate.

&nbsp;&nbsp;&nbsp;&nbsp;```@param col:``` The Drawing.Color

```@returns:``` null.

#### ```func setres (x : float, y : float) : null```


```@desc:``` Sets the resolution to the given horitontal and vertical resolutions.

&nbsp;&nbsp;&nbsp;&nbsp;```@param x:``` The horizontal resolution.

&nbsp;&nbsp;&nbsp;&nbsp;```@param y:``` The vertical resolution.

```@returns:``` null.

#### ```vres { get; }```


```@desc:``` Gets the readonly vertical resolution of the Bitmap.

```@returns:``` Vertical resolution as int.

#### ```width { get; }```


```@desc:``` Gets the readonly width of the Bitmap in pixels.

```@returns:``` Width as int.

