## class DNS

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` A class containing methods for resolving IP addresses and hostnames through the DNS protocol.

#### ```func gethost (IPAddrOrStr : object) : string```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the first hostname of the specified Net.IPAddr object or string ip.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param IPAddrOrStr:``` The Net.IPAddr object or string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The first hostname as a string.

#### ```func gethosts (IPAddrOrStr : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a list of hostnames for the specified Net.IPAddr or string ip.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param IPAddrOrStr:``` The Net.IPAddr object or string ip address.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The list of hostnames.

#### ```func getip (host : string) : IPAddr```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets the first ip address of the specified hostname as a Net.IPAddr object.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param host:``` The hostname as a string.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new Net.IPAddr object.

#### ```func getips (host : string) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Gets a list of Net.IPAddr ip addresses for the specified hostname.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of Net.IPAddr objects.

