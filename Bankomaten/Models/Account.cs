using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        private int Id;
        private int AccountNumber;
        private string AccountName;
        //private User User;
        private decimal Balance;

        //Account Type - testar enum för skojs
        public enum accountType
        {
            Sparkonto,
            Lönekonto,
            Depåkonto
        }
        private accountType AccountType;

        // Constructor
        public Account(string accountname, decimal balance, accountType accounttype)
        {
            Id = id;
            Random randomId = new Random();
            AccountNumber = randomId.Next(100000000, 999999999); //Kontot får ett slumpmässigt "kontonummer"
            AccountName = accountname;
            //User = user;
            Balance = balance;
            AccountType = accounttype;
        }

        // Properties
        public int id
        {
            get { return Id; }
            //Inget set - användaren får inte ändra kontots Id
        }
        public int accountNumber
        {
            get { return AccountNumber; }
            //Inget set - användaren får inte ändra kontotnummer
        }
        public string name
        {
            get { return AccountName; }
            set { AccountName = value; }
        }
        public decimal balance
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
