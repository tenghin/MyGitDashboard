using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace I_am_bored
{
    public class User
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? Role { get; set; }
    }

    public class AccountManager
    {
        private string accountsPath;

        public AccountManager(string rootPath)
        {
            accountsPath = Path.Combine(rootPath, "System", "accounts.json");
        }

        public List<User> LoadUsers()
        {
            if (!File.Exists(accountsPath))
            {
                return new List<User>();
            }

            string json = File.ReadAllText(accountsPath);

            List<User>? users =
                JsonSerializer.Deserialize<List<User>>(json);

            return users ?? new List<User>();
        }
    }
}
