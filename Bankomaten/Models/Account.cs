namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        private static int Counter = 0;
        private int OwnerId;
        private int AccountNumber;
        private string AccountName;
        private decimal Balance;

        //Account Type - Enumeration - en data type som gör det enkelt för oss att utöka kontotyper, om det skulle behövas
        public enum accountType
        {
            Sparkonto,
            Lönekonto,
            Depåkonto
        }
        private accountType AccountType;
        internal readonly object UserId;

        // Constructors
        public Account()
        {
            //Tom constructor som används till att kalla på metoden AddAccountData() utifrån
        }

        // Standard constructor för att skapa konton med en räknare och slumpmässigt kontonummer
        public Account(int ownerid, string accountname, decimal balance, accountType accounttype)
        {
            accountId = ++Counter;
            OwnerId = ownerid;
            Random randomId = new Random();
            AccountNumber = randomId.Next(100000000, 999999999); //Kontot får ett slumpmässigt "kontonummer"
            AccountName = accountname;
            Balance = balance;
            AccountType = accounttype;
        }

        // Constructor overload som används till när vi läser befintliga konton från filen
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

        //Metod som returnerar en list, för att initiera data med standardvärden, om fil inte existerar
        public List<Account> AddAccountData()
        {
            List<Account> AllAccounts = new List<Account> { 
            // Ammar konton
            new Account(1, "Ammar Sparkonto", 1000, accountType.Sparkonto),
            new Account(1, "Ammar Lönekonto", 2000, accountType.Lönekonto),
            new Account(1, "Ammar Depåkonto", 3000, accountType.Depåkonto),

            // Mattias konton
            new Account(2, "Mattias Sparkonto", 1000, accountType.Sparkonto),
            new Account(2, "Mattias Lönekonto", 2000, accountType.Lönekonto),
            new Account(2, "Mattias Depåkonto", 3000, accountType.Depåkonto),

            // Lukas konton
            new Account(3, "Lukas Sparkonto", 1000, accountType.Sparkonto),
            new Account(3, "Lukas Lönekonto", 2000, accountType.Lönekonto),
            new Account(3, "Lukas Depåkonto", 3000, accountType.Depåkonto),

            // Marco konton
            new Account(4, "Marco Sparkonto", 1000, accountType.Sparkonto),
            new Account(4, "Marco Lönekonto", 2000, accountType.Lönekonto),
            new Account(4, "Marco Depåkonto", 3000, accountType.Depåkonto),

            // Kalle konton
            new Account(5, "Kalle Sparkonto", 1000, accountType.Sparkonto),
            new Account(5, "Kalle Lönekonto", 2000, accountType.Lönekonto),
            new Account(5, "Kalle Depåkonto", 3000, accountType.Depåkonto)
            };
            return AllAccounts;
        }
    }
}