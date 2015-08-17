#!/bin/bash

gmcs ConsoleLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o ConsoleLibrary/ConsoleLibrary.dll
gmcs ConversionLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o ConversionLibrary/ConversionLibrary.dll
gmcs FilesystemLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o FilesystemLibrary/FilesystemLibrary.dll
gmcs MathLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o MathLibrary/MathLibrary.dll
gmcs MiscLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o MiscLibrary/MiscLibrary.dll
gmcs NetworkingLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o NetworkingLibrary/NetworkingLibrary.dll
gmcs StringLibrary/*.cs -r:../src/Hassium/bin/Debug/Hassium.exe -target:library -o StringLibrary/StringLibrary.dll
