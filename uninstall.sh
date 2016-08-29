#!/bin/bash

if [[ $EUID -ne 0 ]]; then
   echo "This script must be run as root" 
   exit 1
fi

echo "Removing /usr/bin/Hassium.exe"
rm /usr/bin/Hassium.exe
echo "Removing /usr/bin/hassium"
rm /usr/bin/hassium
echo "All done!"
