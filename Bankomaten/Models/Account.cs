namespace Bankomaten.Models
{
    internal class Account
    {
        //Fields
        private int OwnerId;
        private int AccountNumber;
        private string AccountName;
        private decimal Balance;

        //Account Type - Enumeration - en data type som gör det enkelt för oss att utöka kontotyper, om det skulle behövas
        public enum accountType
        {
            Sparkonto,
            Lönekonto,
            Räkningskonto,
            Fondkonto,
            Depåkonto
        }
        private accountType AccountType;

        // Constructors

        // Standard constructor för att skapa konton med en räknare och slumpmässigt kontonummer
        public Account(int ownerid, string accountname, decimal balance, accountType accounttype)
        {
            OwnerId = ownerid;
            Random randomId = new Random();
            AccountNumber = randomId.Next(100000000, 999999999); //Kontot får ett slumpmässigt "kontonummer"
            AccountName = accountname.Replace(";", "");
            Balance = balance;
            AccountType = accounttype;
        }

        // Constructor overload som används till när vi läser befintliga konton från filen
        public Account(int ownerid, int accountnumber, string accountname, decimal balance, accountType accounttype)
        {
            OwnerId = ownerid;
            AccountNumber = accountnumber;
            AccountName = accountname;
            Balance = balance;
            AccountType = accounttype;
        }

        // Properties
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

        // Metod som initialiserar data, antingen befintlig data från fil, eller standardvärden från Account, och returnerar en lista
        public static List<Account> InitializeAccounts(string path)
        {
            //Kolla om accounts.txt filen finns, om ej, skapa den med standardvärden från Account
            if (!File.Exists(path))
            {

                List<Account> AllAccounts = AddAccountData(path); //Här kommer standardvärden in från Account
                WriteAccountFile(path, AllAccounts);
                return AllAccounts;
            }
            //Om accounts.txt filen finns, läs kontona från filen och lägg datan därifrån i en list
            else
            {
                List<Account> AllAccounts = new List<Account>();
                using StreamReader reader = new StreamReader(path);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    AllAccounts.Add(new Account(int.Parse(parts[0]), int.Parse(parts[1]), parts[2], decimal.Parse(parts[3]), (Enum.Parse<accountType>(parts[4]))));
                }
                return AllAccounts;
            }
        }

        //Metod som returnerar en list, för att initiera data med standardvärden, om fil inte existerar
        public static List<Account> AddAccountData(string path)
        {
            List<Account> AllAccounts = new List<Account> { 
            // Ammar konton
            new Account(1, "Ammar Sparkonto", 1000, accountType.Sparkonto),
            new Account(1, "Ammar Lönekonto", 2000, accountType.Lönekonto),
            new Account(1, "Ammar Depåkonto", 3000, accountType.Depåkonto),
            new Account(1, "Ammar Värdepapper", 20000, accountType.Depåkonto),
            new Account(1, "Mina Fonder", 10000, accountType.Fondkonto),

            // Mattias konton
            new Account(2, "Mattias Sparkonto", 1000, accountType.Sparkonto),
            new Account(2, "Mattias Lönekonto", 2000, accountType.Lönekonto),
            new Account(2, "Mattias Depåkonto", 3000, accountType.Depåkonto),

            // Lukas konton
            new Account(3, "Lukas Sparkonto", 1000, accountType.Sparkonto),
            new Account(3, "Lukas Lönekonto", 2000, accountType.Lönekonto),
            new Account(3, "Lukas Depåkonto", 3000, accountType.Depåkonto),
            new Account(3, "Lukas Semesterkonto", 40000, accountType.Sparkonto),

            // Marco konton
            new Account(4, "Marco Sparkonto", 1000, accountType.Sparkonto),

            // Kalle konton
            new Account(5, "Kalle Sparkonto", 1000, accountType.Sparkonto),
            new Account(5, "Kalle Lönekonto", 2000, accountType.Lönekonto)
            };
            return AllAccounts;
        }

        // Skriver alla kontona till fil
        public static void WriteAccountFile(string path, List<Account> AllAccounts)
        {
            using StreamWriter userfile = new StreamWriter(path);
            foreach (Account acc in AllAccounts)
            {
                userfile.WriteLine($"{acc.ownerId};{acc.accountNumber};{acc.name};{acc.balance};{acc.accType}");
            }
        }

        // Dictionary som numrerar användares konton
        public static Dictionary<int, Account> indexAccounts(List<Account> userAccounts)
        {
            Dictionary<int, Account> accountsDict = new Dictionary<int, Account>();
            for (int i = 0; i < userAccounts.Count; i++)
            {
                accountsDict.Add(i + 1, userAccounts[i]);
            }

            return accountsDict;
        }

        // Skriv ut alla kontotyper
        public static void writeAccountTypes()
        {
            Console.WriteLine("Välj kontotyp");
            int i = 1;
            foreach (accountType accountType in Enum.GetValues<accountType>())
            {

                Console.WriteLine($"{i}. {accountType}");
                i++;
            }
        }
    }
}
