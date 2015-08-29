mono -V
cd tests
xbuild ../src/Hassium.sln
mono ../src/Hassium/bin/Debug/Hassium.exe tests.has