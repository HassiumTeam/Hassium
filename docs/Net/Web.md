## class Web

#### ```func downloaddata (url : string) : list```


```@desc:``` Downloads bytes from the specified url and returns them in a list.

    ```@param url:``` The string url to download from.
```@returns:``` A new list of downloaded bytes.

#### ```func downloadfile (url : string, path : string) : null```


```@desc:``` Downloads a file from the specified url and saves it to the specified path.

    ```@param url:``` The string url to download from.
    ```@param path:``` The string path to save the file to.
```@returns:``` null.

#### ```func downloadstr (url : string) : string```


```@desc:``` Downloads the specified url as a string and returns it.

    ```@param url:``` The url to download from.
```@returns:``` The downloaded string.

#### ```func htmldecode (str : string) : string```


```@desc:``` Decodes the specified html encoded string and returns it.

    ```@param str:``` The html encoded string.
```@returns:``` The decoded string.

#### ```func htmlencode (str : string) : string```


```@desc:``` Encoded the specified string with html encoding and returns it.

    ```@param str:``` The string to be html encoded.
```@returns:``` The encoded string.

#### ```func uploaddata (url : string, data : list) : list```


```@desc:``` Uploads the given byte list to the specified url string and returns the response.

    ```@param url:``` The url to upload to.
    ```@param data:``` The list of bytes to upload.
```@returns:``` The response from the server as a list of bytes.

#### ```func uploadfile (url : string, path : string) : list```


```@desc:``` Uploads the given file path to the specified url string and returns the response.

    ```@param url:``` The url to upload to.
    ```@param path:``` The file path to upload.
```@returns:``` The response from the server as a list of bytes.

#### ```func uploadstr (url : string, str : string) : string```


```@desc:``` Uploads the given string to the specified url string and returns the response.

    ```@param url:``` The url to upload to.
    ```@param str:``` The string to upload.
```@returns:``` The response from the server as a list of bytes.

#### ```func urldecode (url : string) : string```


```@desc:``` Decodes the specified url encoded string and returns it.

    ```@param str:``` The url encoded string.
```@returns:``` The decoded string.

#### ```func urlencode (url : string) : string```


```@desc:``` Encoded the specified string with url encoding and returns it.

    ```@param str:``` The string to be url encoded.
```@returns:``` The encoded string.

