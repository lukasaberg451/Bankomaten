using System;
using System.Collections.Generic;
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

        //Lista på användarens konton
        public List<Account> Accounts { get; set; }


        public User(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            Accounts = new List<Account>();
        }

        public User()
        {
        }

        public void ShowUserInfo()
        {
            Console.WriteLine($"Användare : {UserName}");

            foreach (Account account in Accounts)
            {
                Console.WriteLine($" - Konto: {account.name} | Saldo: {account.balance}");
            }


        }

        public Account GetAccount(int accountnumber)
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
