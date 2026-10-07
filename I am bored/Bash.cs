using I_am_bored.Applications;
using I_am_bored.bin.Debug.net8._0.Storage_C.Applications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace I_am_bored
{

    class Shell
    {
        private FileSystem fileSystem = new FileSystem();

        private readonly string rootPath = Path.GetFullPath(@"Storage C");
        private string currentPath;

        public void Run()
        {   
            currentPath = rootPath;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                //Console.WriteLine("");
                Console.Write($"{GetVirtualPath()}> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                string[] parts = input.Split(' ', 2);

                string command = parts[0];

                switch (command)
                {
                    case "dir":
                        Dir();
                        break;

                    case "cd":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: cd <directory>");
                        }
                        else
                        {
                            Cd(parts[1]);

                        }
                        break;

                    case "add":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: add <filename>");
                        }
                        else
                        {
                            fileSystem.add(currentPath, parts[1]);
                        }
                        break;

                    case "rm":
                    case "delete":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: delete <filename>");
                        }
                        else
                        {
                            fileSystem.rm(currentPath, parts[1]);
                        }
                        break;

                    case "mkdir":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: mkdir <folder name>");
                        }
                        else
                        {
                            fileSystem.mkdir(currentPath, parts[1]);
                        }
                        break;

                    case "open":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: open <filename>");
                        }
                        else
                        {
                            fileSystem.open(currentPath, parts[1]);
                        }
                         break;

                    case "rmdir":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: rmdir <folder name>");
                        }
                        else
                        {
                            fileSystem.rmdir(currentPath, parts[1]);
                        }
                        break;

                    case "rename":
                        if (parts.Length < 3)
                        {
                            Console.WriteLine("Usage: rename <item name>");
                        }
                        else
                        {
                            fileSystem.rename(currentPath, parts[1], parts[2]);
                        }
                        break;
                    case "mv":

                        if (parts.Length < 3)
                        {
                            Console.WriteLine("Usage: mv <source> <destination>");
                            break;
                        }

                        fileSystem.mv(currentPath, parts[1], parts[2]);
                        break;

                    case "gemini":
                        AI ai = new AI();
                        ai.Run().GetAwaiter().GetResult();
                        break;

                    case "tetris":
                        Tetris tetris = new Tetris();
                        tetris.Run();
                        break;

                    case "doom":
                        Doom doom = new Doom();
                        doom.Run();
                        break;

                    case "pepega":
                                Console.WriteLine("⠁⠁⠁⠁⠁⠁⠐⢶⣶⣶⣶⣤⣤⡀⠁⠁⣠⣀⣀⠁⠁⠁⠁⠁⠁⠁⠁⠁⠁⠁");
                                Console.WriteLine("⠁⠁⠁⠁⠁⠁⠁⠁⠙⢿⣯⣠⣶⣦⣤⣤⣌⣛⠻⢇⣠⣤⣤⠁⠁⠁⠁⠁⠁⠁");
                                Console.WriteLine("⠁⠁⠁⠁⠁⠁⠁⠁⠁⠁⠻⣿⣿⣿⡟⢉⡤⢤⣤⣤⡍⠛⢡⢖⣥⣶⣦⣀⠁⠁");
                                Console.WriteLine("⠁⠁⠁⠁⠁⠁⠁⠁⠁⠁⣠⣿⣿⣿⡏⣭⣶⣿⣿⠟⢿⣦⡡⣿⣿⡇⠁⡙⣷⡀");
                                Console.WriteLine("⠁⠁⠁⠁⠁⠁⠁⣀⣴⣿⣿⣿⣿⣿⣿⡞⣿⣿⡟⢀⡀⣿⣿⢻⣿⣿⣀⣁⣿⠏");
                                Console.WriteLine("⠁⠁⠁⢀⣠⣶⣿⣿⣿⣿⣿⣿⣿⣿⣟⢰⢻⣿⣇⣈⣴⣿⠟⢨⣛⠛⠛⠉⠁⠁");
                                Console.WriteLine("⠁⣠⣶⣿⣿⡟⢋⠤⣤⠘⢿⣿⣧⡙⠻⠌⠒⠙⠛⢛⣫⣥⣿⣦⡈⠉⣡⣴⣾⠇");
                                Console.WriteLine("⢰⣿⣿⣿⣿⠁⡇⠁⠙⠷⣤⡙⠻⢿⣿⣶⣶⣶⣿⣿⣿⣿⣿⣿⣿⠿⠟⠋⠁⠁");
                                Console.WriteLine("⠘⣿⣿⣿⣿⣆⠻⣄⠁⣀⡀⠉⠙⠒⠂⠉⠍⠉⠉⠉⠉⣩⣍⣁⣂⡈⠠⠂⠁⠁");
                                Console.WriteLine("⠁⠘⢿⣿⣿⣿⣦⡉⠳⢬⣛⠷⢦⡄⠁⠁⠁⠁⠁⣀⣼⣿⣿⠿⠛⠋⠁⠁⠁⠁");
                                Console.WriteLine("⠁⠁⠁⠉⠻⢿⣿⣿⣷⣦⣬⣍⣓⡒⠒⣒⣂⣠⡬⠽⠓⠂⠁⠁⠁⠁⠁⠁⠁⠁");
                                break;

                    case "help":
                        // Print the command name instead of using a collection expression
                        help();
                        break;

                    case "exit":
                        Console.WriteLine($"Good Bye");
                        
                        return;

                    case "logout":
                        Global.CurrentUser = null;
                        Console.WriteLine("Logged out.");
                        return;

                    default:
                          Console.WriteLine("Command not found.");
                          break;
                    }

                //break a line after each command executed
                Console.WriteLine("");
            }
        }
        


        private string GetVirtualPath()
        {
            string relativePath = Path.GetRelativePath(rootPath, currentPath);

            if (relativePath == ".")
            {
                return @"C:\";
            }

            return @"C:\" + relativePath;
        }

        [Command("Change directory")]

        private void Cd(string target)
        {
            string newPath = Path.GetFullPath(
                Path.Combine(currentPath, target)
            );

            bool isAdmin =
                Global.CurrentUser?.Role == "Admin";

            // Normal users cannot escape Storage C
            if (!isAdmin)
            {
                string relativePath =
                    Path.GetRelativePath(rootPath, newPath);

                if (relativePath == ".." ||
                    relativePath.StartsWith(
                        ".." + Path.DirectorySeparatorChar
                    ))
                {
                    Console.WriteLine("Access denied.");
                    return;
                }
            }

            if (!Directory.Exists(newPath))
            {
                Console.WriteLine("Directory not found.");
                return;
            }

            currentPath = newPath;
        }

        [Command("Show files and folders")]

        private void Dir()
        {
            string[] entries = Directory.GetFileSystemEntries(currentPath);

            if (entries.Length == 0)
            {
                Console.WriteLine("system: This directory is empty.");
                return;
            }

            foreach (string entry in entries)
            {
                if (Directory.Exists(entry))
                {
                    Console.WriteLine("[DIR]  " + Path.GetFileName(entry));
                }
                else
                {
                    Console.WriteLine("[FILE] " + Path.GetFileName(entry));
                }
            }
        }

        private void help()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine();

            Console.WriteLine("COMMAND    | DESCRIPTION");
            Console.WriteLine("-----------+----------------------------------------");

            ShowCommands(typeof(Shell));
            ShowCommands(typeof(FileSystem));

        }

        private void ShowCommands(Type type)
        {
            var methods = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<CommandAttribute>();

                if (attribute != null)
                {
                    Console.WriteLine(
                        $"{method.Name,-10} | {attribute.Description}"
                    );
                }
            }
        }
    }
}
