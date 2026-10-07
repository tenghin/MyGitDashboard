using System;
using System.Diagnostics;
using System.IO;


namespace I_am_bored
{
    class FileSystem
    {
        [Command("Create a file")]
        public void add(string currentPath, string fileName)
        {
            string filePath = Path.Combine(currentPath, fileName);

            if (File.Exists(filePath))
            {
                Console.WriteLine("File already exists.");
                return;
            }

            File.WriteAllText(filePath, "");

            Console.WriteLine("File created: " + fileName);
        }

        [Command("Delete a file")]
        public void rm(string currentPath, string fileName)
        {
            string filePath = Path.Combine(currentPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            File.Delete(filePath);

            Console.WriteLine("File deleted: " + fileName);
        }

        [Command("Add a folder")]
        public void mkdir(string currentPath, string folderName)
        {
            string folderPath = Path.Combine(currentPath, folderName);

            if (Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder already exists.");
                return;
            }

            Directory.CreateDirectory(folderPath);

            Console.WriteLine("Folder created: " + folderName);
        }

        [Command("Remove a folder")]
        public void rmdir(string currentPath, string folderName)
        {
            string folderPath = Path.Combine(currentPath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder not found.");
                return;
            }

            Directory.Delete(folderPath);

            Console.WriteLine("Folder deleted: " + folderName);
        }

        [Command("Rename a item")]
        public void rename(string currentPath, string oldName, string newName)
        {
            string oldPath = Path.Combine(currentPath, oldName);
            string newPath = Path.Combine(currentPath, newName);

            // Check if the new name is already being used
            if (File.Exists(newPath) || Directory.Exists(newPath))
            {
                Console.WriteLine("A file or folder with that name already exists.");
                return;
            }

            // Rename file
            if (File.Exists(oldPath))
            {
                File.Move(oldPath, newPath);
                Console.WriteLine($"{oldName} renamed to {newName}");
                return;
            }

            // Rename folder
            if (Directory.Exists(oldPath))
            {
                Directory.Move(oldPath, newPath);
                Console.WriteLine($"{oldName} renamed to {newName}");
                return;
            }

            Console.WriteLine("File or folder not found.");
        }

        [Command("Move an Item. Can also be used to rename")]
        public void mv(string currentPath, string source, string destination)
        {
            string sourcePath = Path.GetFullPath(
                Path.Combine(currentPath, source)
            );

            string destinationPath = Path.GetFullPath(
                Path.Combine(currentPath, destination)
            );

            // Source doesn't exist
            if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
            {
                Console.WriteLine("File or folder not found.");
                return;
            }

            // If destination is an existing folder,
            // move the source INTO that folder
            if (Directory.Exists(destinationPath))
            {
                string sourceName = Path.GetFileName(sourcePath);

                destinationPath = Path.Combine(
                    destinationPath,
                    sourceName
                );
            }

            // Don't overwrite something that already exists
            if (File.Exists(destinationPath) ||
                Directory.Exists(destinationPath))
            {
                Console.WriteLine("Destination already exists.");
                return;
            }

            // Move file
            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, destinationPath);
                Console.WriteLine("File moved.");
                return;
            }

            // Move folder
            if (Directory.Exists(sourcePath))
            {
                Directory.Move(sourcePath, destinationPath);
                Console.WriteLine("Folder moved.");
            }
        }

        [Command("Open a file")]
        public void open(string currentPath, string fileName)
        {
            string filePath = Path.Combine(currentPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}