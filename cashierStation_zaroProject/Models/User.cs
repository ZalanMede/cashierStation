using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cashierStation_zaroProject.Models;

namespace cashierStation_zaroProject.Models
{
    internal class User
    {
        public User()
        {
        }

        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [Unique]
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string RoleNev => Enum.GetName(typeof(Role), Role) ?? "Unknown";

        public User(string username, string fullName, int role)
        {
            Username = username;
            FullName = fullName;
            Role = role;
        }

        public User(string username, string fullName, int role, string password)
        {
            Username = username;
            FullName = fullName;
            Password = password;
            Role = role;
        }
    }
}
