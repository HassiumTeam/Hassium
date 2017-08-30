## class AES

#### ```func decryptbytes (key : list, iv : list, dataStrOrList : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Decrypts the given byte string or list using the specified 16 byte key and iv, returning the result.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The 16 byte long AES key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param iv:``` The 16 byte long AES iv.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param dataStrOrList:``` The data string or list to decrypt.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of decrypted bytes.

#### ```func decryptfilebytes (key : list, iv : list, file : File) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Decrypts the given File object using the specified 16 byte key and iv, returning the result.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The 16 byte long AES key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param iv:``` The 16 byte long AES iv.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The IO.File object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of decrypted bytes.

#### ```func encryptbytes (key : list, iv : list, dataStrOrList : object) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Encrypts the given byte string or list using the specified 16 byte key and iv, returning the result.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The 16 byte long AES key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param iv:``` The 16 byte long AES iv.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param dataStrOrList:``` The data string or list to decrypt.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of encrypted bytes.

#### ```func encryptfilebytes (key : list, iv : list, file : File) : list```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Encrypts the given File object using the specified 16 byte key and iv, returning the result.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param key:``` The 16 byte long AES key.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param iv:``` The 16 byte long AES iv.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param file:``` The IO.File object.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` A new list of encrypted bytes.

