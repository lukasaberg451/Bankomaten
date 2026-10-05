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
        private int accountNumber;

        // Lagrar användarens användarnamn
        public string UserName { get; set; }

        // Lagrar användarens lösenord
        public string PassWord { get; set; }
        
        //lagrar ID för använadre
        public int UserId { get; set; }

        //Lista på användarens konton
        public List<Account> Accounts { get; set; }





        static void AddUser()
        {
            List<User> listUser = new List<User>();

            User user1 = new User("Ammar", "1234");
            User user2 = new User("Mattias", "1234");
            User user3 = new User("Lukas", "1234");
            User user4 = new User("Marco", "1234");
            User user5 = new User("Kalle", "1234");
        }



        public User(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            UserId = userId;
            Accounts = new List<Account>();
            //Account acc new Account()

            Account account1 = new Account(1, "testkonto", 20300, accountType.Sparkonto);
            Account account2 = new Account(1, "testkonto2", 10600, accountType.Depåkonto);
            Account account3 = new Account(1, "testkonto3", 20030, accountType.Lönekonto);

            Accounts.Add(account1);
            Accounts.Add(account2);
            Accounts.Add(account2);
        }

        public User()
        {
        }

        public void ShowUserInfo()
        {
            Console.WriteLine($"Namn : {UserName} | AnvändarID : {UserId} ");

            foreach (Account account in Accounts)
            {
                Console.WriteLine($" - Konto: {account.name} | Saldo: {account.balance}");
            }
        }

        public User FindUser(string userName, string passWord, List<User> listUser)
        {
            foreach (Account account in Accounts)
            {
                if (accountNumber == accountnumber)
                {
                    return account;
                }

            }
            return null;
        }

        

    }
}