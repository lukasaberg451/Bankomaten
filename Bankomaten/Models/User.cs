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

        public User(int userId, string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            UserId = userId;
        }

        public void ShowUserInfo()
        {
            Console.WriteLine($"Namn : {UserName} | AnvändarID : {UserId} ");

            //foreach (Account account in Accounts)
            //{
            //    Console.WriteLine($" - Konto: {account.name} | Saldo: {account.balance}");
            //}
        }
    }
}