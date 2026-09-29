using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        private int Id;
        private string AccName;
        private User User;
        private double Balance;

        //Account Type - testing enum for fun
        public enum AccType
        {
            Savings,
            Salary,
            Funds
        }
        private AccType AccountType;
        // Properties
        public int id
        {
            get { return Id; }
            //No set - user can not change the account Id
        }
        public string name
        {
            get { return AccName; }
            set { AccName = value; }
        }
        public double balance
        {
            get { return Balance; }
            set { Balance = value; }
        }
        public AccType accType
        {
            get { return AccountType; }
            set { AccountType = value; }
        }
    }
}
