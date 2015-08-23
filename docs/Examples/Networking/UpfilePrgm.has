$SUMMARY: Uploads a file to a specific url$

print("Enter a url to an upload script: ");
url := input();

print("Enter a path: ");
path := input();

if (fexists(path)) {
	result := upfile(url, path);
	print("Uploaded file, response from server was: ", result, "\n")
	exit(0);
} else {
	print("Specified file path does not exist!\n");
	exit(1);
}
