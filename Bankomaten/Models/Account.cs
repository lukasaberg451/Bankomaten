namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        private static int Counter = 0;
        private int OwnerId;
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
        internal readonly object UserId;

        // Constructor
        public Account(int ownerid, string accountname, decimal balance, accountType accounttype)
        {
            accountId = ++Counter;
            OwnerId = ownerid;
            Random randomId = new Random();
            AccountNumber = randomId.Next(100000000, 999999999); //Kontot får ett slumpmässigt "kontonummer"
            AccountName = accountname;
            //User = user;
            Balance = balance;
            AccountType = accounttype;
        }

        // Constructor overload, used for loading accounts from StreamReader
        public Account(int accountid, int ownerid, int accountnumber, string accountname, decimal balance, accountType accounttype)
        {
            accountId = accountid;
            OwnerId = ownerid;
            AccountNumber = accountnumber;
            AccountName = accountname;
            Balance = balance;
            AccountType = accounttype;
        }

        // Properties
        public int accountId
        {
            get; 
            //Inget set - användaren får inte ändra kontots Id
        }
        public int ownerId
        {
            get { return OwnerId; }
            set { OwnerId = value; }
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
            internal set { Balance = value; }
        }
        public accountType accType
        {
            get { return AccountType; }
            set { AccountType = value; }
        }      
    }
}