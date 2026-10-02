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

        
        
        // Konstruktor
        public User(string userName, string passWord, int userId)
        {
            UserName = userName;
            PassWord = passWord;
            UserId = userId;
            Accounts = new List<Account>();
        }



        static void AddUser()
        {
         List<User> listUser = new List<User>();

        User user1 = new User("Ammar", "1234", 1);
        User user2 = new User("Mattias", "1234", 2);
        User user3 = new User("Lukas", "1234", 3);
        User user4 = new User("Marco", "1234", 4);
        User user5 = new User("Kalle", "1234", 5);
        }




        public void ShowUserInfo()
        {
            Console.WriteLine($"Namn : {UserName} | AnvändarID : {UserId} ");

            foreach (Account account in Accounts)
            {
                Console.WriteLine($" - Konto: {account.name} | Saldo: {account.balance}");
            }




        }
        public User FindUser(string userName , string passWord, List<User> listUser)
        {
            User user = listUser.FirstOrDefault(u => u.UserName == userName && u.PassWord == passWord);
            return user;
        }


    }
}
