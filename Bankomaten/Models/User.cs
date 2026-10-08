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
        public string UserName { get; set; }

        // Lagrar användarens lösenord
        public string PassWord { get; set; }
        
        //lagrar ID för använadre
        public int UserId { get; set; }

        public static List<User> UserList { get; private set; } = new List<User>
        {
            new User(1, "Ammar", "1234"),
            new User(2, "Mattias", "1234"),
            new User(3, "Lukas", "1234"),
            new User(4, "Marco", "1234"),
            new User(5, "Kalle", "1234")
        };


        // Konstruktor
          public User(int userId, string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            UserId = userId;
        }

      
    }
}