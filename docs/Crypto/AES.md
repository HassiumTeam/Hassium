## class AES

#### ```func decryptbytes (key : list, iv : list, dataStrOrList : object) : list```


```@desc:``` Decrypts the given byte string or list using the specified 16 byte key and iv, returning the result.

	```@param key:``` The 16 byte long AES key.
	```@param iv:``` The 16 byte long AES iv.
	```@param dataStrOrList:``` The data string or list to decrypt.
```@returns:``` A new list of decrypted bytes.

#### ```func decryptfilebytes (key : list, iv : list, file : File) : list```


```@desc:``` Decrypts the given File object using the specified 16 byte key and iv, returning the result.

	```@param key:``` The 16 byte long AES key.
	```@param iv:``` The 16 byte long AES iv.
	```@param file:``` The IO.File object.
```@returns:``` A new list of decrypted bytes.

#### ```func encryptbytes (key : list, iv : list, dataStrOrList : object) : list```


```@desc:``` Encrypts the given byte string or list using the specified 16 byte key and iv, returning the result.

	```@param key:``` The 16 byte long AES key.
	```@param iv:``` The 16 byte long AES iv.
	```@param dataStrOrList:``` The data string or list to decrypt.
```@returns:``` A new list of encrypted bytes.

#### ```func encryptfilebytes (key : list, iv : list, file : File) : list```


```@desc:``` Encrypts the given File object using the specified 16 byte key and iv, returning the result.

	```@param key:``` The 16 byte long AES key.
	```@param iv:``` The 16 byte long AES iv.
	```@param file:``` The IO.File object.
```@returns:``` A new list of encrypted bytes.

