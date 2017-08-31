## class RSA

#### ```func decrypt (pubmod : BigInt, privkey : BigInt, msg : object)```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Decrypts the given msg using the given public modulus BigInt and private key BigInt, returning the resulting bytes.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param pubmod:``` The public key modulus BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param privkey:``` The private key BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param msg:``` The msg to decrypt.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` The decrypted msg bytes.

#### ```func encrypt (pubmod : object, pube : object, msg : object)```

&nbsp;&nbsp;&nbsp;&nbsp;```@desc:``` Encrypts the given msg using the given public modulus BigInt and public e BigInt, return the resulting bytes.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param pubmod:``` The public key modulus BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param pube:``` The public e BigInt.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```@param msg:``` The msg to encrypt.

&nbsp;&nbsp;&nbsp;&nbsp;```@returns:``` the encrytped msg bytes.

