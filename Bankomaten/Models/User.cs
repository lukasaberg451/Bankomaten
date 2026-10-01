using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class User
    {
       // Lagrar användarens användarnamn
        public string UserName { get; set; }
        
        // Lagrar användarens lösenord
        public string PassWord { get; set; }

        //Lista på användarens konton
        public List<Account> Accounts { get; set; }

        
        public User(string userName,string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            Accounts = new List<Account>();
        }

        public void ShowUserInfo()
        {
            Console.WriteLine($"Användare : {UserName}");

            foreach (Account account in Accounts)
            {
                Console.WriteLine($" - Konto: {account.Name} | Saldo: {account.balance}");
            }
           
           
        }
        

        
    }
}
