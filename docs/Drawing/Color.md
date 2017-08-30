## class Color

#### ```a { get; }```


```@desc:``` Gets the readonly alpha value.

```@returns:``` a as int.

#### ```argb { get; }```


```@desc:``` Gets the readonly argb value.

```@returns:``` argb as int.

#### ```b { get; }```


```@desc:``` Gets the readonly blue value.

```@returns:``` b as int.

#### ```g { get; }```


```@desc:``` Gets the readonly green value.

```@returns:``` g as int.

#### ```func new (colIntOrStr : object) : Color```


```@desc:``` Constructs a new Color with either the specified color name, argb, specified r, g, b, or specified a, r, g, b.

```	@optional: colIntOrStr:``` The color name string or argb int.
```	@optional: a:``` The alpha value.
```	@optional: r:``` The red value.
```	@optional: g:``` The green value.
```	@optional: b:``` The blue value.
```@returns:``` The new Color object.

#### ```func new (r : int, g : int, b : int) : Color```


```@desc:``` Constructs a new Color with either the specified color name, argb, specified r, g, b, or specified a, r, g, b.

```	@optional: colIntOrStr:``` The color name string or argb int.
```	@optional: a:``` The alpha value.
```	@optional: r:``` The red value.
```	@optional: g:``` The green value.
```	@optional: b:``` The blue value.
```@returns:``` The new Color object.

#### ```func new (a : int, r : int, g : int, b : int) : Color```


```@desc:``` Constructs a new Color with either the specified color name, argb, specified r, g, b, or specified a, r, g, b.

```	@optional: colIntOrStr:``` The color name string or argb int.
```	@optional: a:``` The alpha value.
```	@optional: r:``` The red value.
```	@optional: g:``` The green value.
```	@optional: b:``` The blue value.
```@returns:``` The new Color object.

#### ```r { get; }```


```@desc:``` Gets the readonly red value.

```@returns:``` r as int.

