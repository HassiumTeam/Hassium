## ```class WebClient```

### ```func downloadData (url : string) : list```
Downloads all the bytes from the specified url and returns them in a list.

### ```func downloadFile (url : string, path : string) : null```
Downloads the data from the specified url and saves it to the path.

### ```func downloadString (url : string) : string```
Downloads the data from the url and returns it as a string.

### ```func uploadData (data : list, url : string) : string```
Uploads the data from the list to the specified url using POST and returns the result.
### ```func uploadData (data : list, url : string, method : string) : string```
Uploads the data from the list to the specified url using the specified method and returns the result.

### ```func uploadFile (path : string, url : string) : string```
Uploads the file to the specified url using the POST method and returns the result.
### ```func uploadFile (path : string, url : string, method : string) : string```
Uploads the file to the specified url using the specified method and returns the result.

### ```func uploadString (str : string, url : string) : string```
Uploads the string to the specified url using the POST method and returns the result.
### ```func uploadString (str : string, url : string, method : string) : string```
Uploads the string to the specified url using the specified method and returns the result.
