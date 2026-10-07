using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_am_bored
{
    internal class Login
    {
        private readonly string rootPath;

        public Login(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public User? UserPrompt() {
            Console.WriteLine("Please Login:");

            Console.Write("Username:");
            string? username = Console.ReadLine();

            Console.Write("Password:");
            string? password = Console.ReadLine();

            return UserAuthentication(username, password);
        }

        private User? UserAuthentication(string? username, string? password) 
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return null;
            } 

            AccountManager accountManager = new AccountManager(rootPath);

            List<User> users = accountManager.LoadUsers();

            User? user = users.FirstOrDefault(
                u => string.Equals(
                    u.Username,
                    username,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }

            if (user.PasswordHash == null || user.Salt == null)
            {
                Console.WriteLine("Account password data is invalid.");
                return null;
            }

            bool passwordCorrect = PasswordHasher.VerifyPassword(
                password,
                user.PasswordHash,
                user.Salt
            );

            if (!passwordCorrect)
            {
                Console.WriteLine("Incorrect password.");
                return null;
            }

            Console.WriteLine($"Welcome, {user.Username}");

            return user;
        }

    }
}
