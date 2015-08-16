$SUMMARY: Downloads a file from a user's input to a path of the user's input$

print("Enter the URL to a file: ");
url := input();

print("Enter the path where you want the file to save: ");
path := input();

if (fexists(path)) {
	print("File already exists!");
	exit(1);
}

dowfile(url, path);

if (fexists(path)) {
	print("File downloaded successfully");
	exit(0);
} else {
	print("Something went wrong and the file didn't download!");
	exit(1);
}
