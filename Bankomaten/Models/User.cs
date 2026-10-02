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

        //Lista på användarens konton
        public List<Account> Accounts { get; set; }

        private static List<User> UserList = new List<User>
        {
            new User("Ammar", "1234", 1),
            new User("Mattias", "1234", 2),
            new User("Lukas", "1234", 3),
            new User("Marco", "1234", 4),
            new User("Kalle", "1234", 5)
        };
          
        
        
        // Konstruktor
        public User(string userName, string passWord, int userId)
        {
            UserName = userName;
            PassWord = passWord;
            UserId = userId;
            Accounts = new List<Account>();
        }

        public void ShowUserInfo()
        {
            Console.WriteLine($"Namn : {UserName} | AnvändarID : {UserId} ");

            foreach (Account account in Accounts)
            {
                Console.WriteLine($" - Konto: {account.name} | Saldo: {account.balance}");
            }
        }

        public static User FindUser(string username, string password)
        {
            User user = UserList.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            return user;
        }
    }
}