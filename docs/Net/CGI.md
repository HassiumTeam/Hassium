## ```class CGI```

### ```get : dictionary, read-only```
Returns a dictionary containing all of the GET variables passed to the script via the URL parameters.

### ```post : dictionary, read-only```
Returns a dictionary containing all of the POST variables passed to the script via the HTTP POST method

### ```documentRoot : string, read-only```
Returns a string containing the path of the document root directory under which the current script is executing, as defined in the server's configuration file.

### ```remote : object, read-only```
Returns an object containing the following properties:
#### ```ip : string, read-only```
The IP address from which the user is viewing the current page.
#### ```host : string, read-only```
The Host name from which the user is viewing the current page.
#### ```port : string, read-only```
The port being used on the user's machine to communicate with the web server.
#### ```user : string, read-only```
Name of the remote user.
#### ```ident : string, read-only```
Name of the remote user (as returned by identd).

### ```request : object, read-only```
Returns an object containing the following properties:
#### ```method : string, read-only```
Which request method was used to access the page; i.e. 'GET', 'HEAD', 'POST', 'PUT'.
#### ```uri : string, read-only```
The URI which was given in order to access this page; for instance, '/index.html'.
#### ```scheme : string, read-only```
The protocol of the request (generally "http" or "https").

### ```server : object, read-only```
Returns an object containing the following properties:
#### ```admin : string, read-only```
The value given to the SERVER_ADMIN (for Apache) directive in the web server configuration file. If the script is running on a virtual host, this will be the value defined for that virtual host.
#### ```address : string, read-only```
The IP address of the server under which the current script is executing.
#### ```name : string, read-only```
The name of the server host under which the current script is executing. If the script is running on a virtual host, this will be the value defined for that virtual host.
#### ```port : string, read-only```
The port on the server machine being used by the web server for communication. For default setups, this will be '80'; using SSL, for instance, will change this to whatever your defined secure HTTP port is.
#### ```protocol : string, read-only```
Name and revision of the information protocol via which the page was requested; i.e. 'HTTP/1.0';
#### ```software : string, read-only```
Server identification string, given in the headers when responding to requests.
#### ```signature : string, read-only```
String containing the server version and virtual host name which are added to server-generated pages, if enabled.

### ```http : object, read-only```
Returns an object containing the following properties:
#### ```accept : object, read-only```
Returns an object containing the following properties:
##### ```mimeType : string, read-only```
Contents of the Accept: header from the current request, if there is one.
##### ```encoding : string, read-only```
Contents of the Accept-Encoding: header from the current request, if there is one. Example: 'gzip'.
##### ```language : string, read-only```
Contents of the Accept-Language: header from the current request, if there is one. Example: 'en'.
#### ```cacheControl : string, read-only```
Contains the value of the "Cache-Control" header received from the client.
#### ```connection : string, read-only```
Contents of the Connection: header from the current request, if there is one. Example: 'Keep-Alive'.
#### ```cookie : string, read-only```
Contains the raw value of the "Cookie" header send by the user agent.
#### ```doNotTrack : bool, read-only```
True if the client has enabled Do Not Track.
#### ```userMail : string, read-only```
The email address of the user making the request; most browsers do not pass this information, since it is considered an invasion of the user's privacy.
#### ```host : string, read-only```
Contents of the Host: header from the current request, if there is one.
#### ```referer : string, read-only```
The address of the page (if any) which referred the user agent to the current page. This is set by the user agent. Not all user agents will set this, and some provide the ability to modify HTTP_REFERER as a feature. In short, it cannot really be trusted.
#### ```userAgent : string, read-only```
Contents of the User-Agent: header from the current request, if there is one. This is a string denoting the user agent being which is accessing the page. A typical example is: Mozilla/4.5 [en] (X11; U; Linux 2.2.9 i586). 
#### ```upgradeInsecureRequests : bool, read-only```
todo

todo
