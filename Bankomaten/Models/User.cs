using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Text;
using static Bankomaten.Models.Account;

namespace Bankomaten.Models
{
    internal class User
    {
        // Lagrar användarens användarnamn
        public string Username { get; set; }

        // Lagrar användarens lösenord
        public string Password { get; set; }
        
        //lagrar ID för använadre
        public int UserId { get; set; }

        // Konstruktor
        public User(int userId, string username, string password)
        {
            Username = username;
            Password = password;
            UserId = userId;
        }

        // Lista med aktiva användare
        public static List<User> UserList { get; private set; } = new List<User>
        {
            new User(1, "Ammar", "1234"),
            new User(2, "Mattias", "1234"),
            new User(3, "Lukas", "1234"),
            new User(4, "Marco", "1234"),
            new User(5, "Kalle", "1234")
        };
    }
}
