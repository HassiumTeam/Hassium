## class Web

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class representing a web client for interacting with HTTP servers.

#### ```func downloaddata (url : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Downloads bytes from the specified url and returns them in a list.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param url:``` The string url to download from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of downloaded bytes.

#### ```func downloadfile (url : string, path : string) : null```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Downloads a file from the specified url and saves it to the specified path.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param url:``` The string url to download from.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The string path to save the file to.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` null.

#### ```func downloadstr (url : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Downloads the specified url as a string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param url:``` The url to download from.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The downloaded string.

#### ```func htmldecode (str : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Decodes the specified html encoded string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The html encoded string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The decoded string.

#### ```func htmlencode (str : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Encoded the specified string with html encoding and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to be html encoded.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The encoded string.

#### ```func uploaddata (url : string, data : list) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Uploads the given byte list to the specified url string and returns the response.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param url:``` The url to upload to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param data:``` The list of bytes to upload.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The response from the server as a list of bytes.

#### ```func uploadfile (url : string, path : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Uploads the given file path to the specified url string and returns the response.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param url:``` The url to upload to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param path:``` The file path to upload.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The response from the server as a list of bytes.

#### ```func uploadstr (url : string, str : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Uploads the given string to the specified url string and returns the response.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param url:``` The url to upload to.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to upload.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The response from the server as a list of bytes.

#### ```func urldecode (url : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Decodes the specified url encoded string and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The url encoded string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The decoded string.

#### ```func urlencode (url : string) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Encoded the specified string with url encoding and returns it.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param str:``` The string to be url encoded.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The encoded string.

