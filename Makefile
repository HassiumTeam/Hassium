.PHONY: clean all

all:
	xbuild ./src/Hassium.sln
	chmod 777 * -R

clean:
	cd ~ && rm .Hassium -rf
	rm /usr/bin/Hassium.exe -rf
	rm /usr/bin/hassium -rf
	rm ./src/Hassium/bin/Debug/Hassium.exe -rf

install:
	mkdir -p ~/.Hassium
	cp ./src/Hassium/bin/Debug/Hassium.exe /usr/bin/Hassium.exe
	echo "#! /bin/bash" > /usr/bin/hassium
	echo "/usr/bin/mono /usr/bin/Hassium.exe \$$@" >> /usr/bin/hassium
	chmod +x /usr/bin/hassium
	chmod 777 * -R

install-mac:
	mkdir -p ~/.Hassium
	cp ./src/Hassium/bin/Debug/Hassium.exe /usr/local/bin/Hassium.exe
	echo "#! /bin/bash" > /usr/local/bin/hassium
	echo "/usr/local/bin/mono-boehm /usr/local/bin/Hassium.exe \$$@" >> /usr/local/bin/hassium
	chmod +x /usr/local/bin/hassium
	chmod 777 * -r
