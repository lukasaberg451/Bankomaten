namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        static int Id = 0;
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

        // Constructor
        public Account(int ownerid, string accountname, decimal balance, accountType accounttype)
        {
            Id = GenerateId();
            OwnerId = ownerid;
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
            set { Balance = value; }
        }
        public accountType accType
        {
            get { return AccountType; }
            set { AccountType = value; }
        }

        // Metod för att generera ett nytt nummer till konto Id
        static int GenerateId()
        {
            return Id++;
        }

        /* Metod för att skapa ett nytt konto
        public Account CreateAccount(string accountname, decimal balance, accountType accounttype)
        {
            Account account = new Account(accountname, balance, accounttype);
            Console.WriteLine($"Nytt {account.AccountType} har skapats med konto nummer: {account.accountNumber}.");
            return account;
        }*/

    }
}
