using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        private int Id;
        private string AccountName;
        private User User;
        private double Balance;

        //Account Type - testar enum för skojs
        public enum accountType
        {
            Sparkonto,
            Lönekonto,
            Depåkonto
        }
        private accountType AccountType;

        // Constructor
        public Account(string accountname, User user, double balance, accountType accounttype)
        {
            Random randomId = new Random();
            Id = randomId.Next(100000000, 999999999); //Kontot får ett slumpmässigt "kontonummer"
            AccountName = accountname;
            User = user;
            Balance = balance;
            AccountType = accounttype;
        }

        // Properties
        public int id
        {
            get { return Id; }
            //Inget set - användaren får inte ändra kontots Id
        }
        public string name
        {
            get { return AccountName; }
            set { AccountName = value; }
        }
        public double balance
        {
            get { return Balance; }
            set { Balance = value; }
        }
        public accountType accType
        {
            get { return AccountType; }
            set { AccountType = value; }
        }
    }
}
