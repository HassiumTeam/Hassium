#!/bin/bash

if [[ $EUID -ne 0 ]]; then
   echo "This script must be run as root" 
   exit 1
fi

echo "Building Hassium"
xbuild src/Hassium.sln
echo "Moving Hassium.exe to /usr/bin/Hassium.exe"
cp src/Hassium/bin/Debug/Hassium.exe /usr/bin/Hassium.exe
echo "Moving hassium.sh to /usr/bin/hassium"
cp hassium.sh /usr/bin/hassium
echo "Making /usr/bin/hassium executable"
chmod +x /usr/bin/hassium
echo "All done! You can run hassium --help for help"
