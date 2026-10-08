using static Bankomaten.Models.Account;

namespace Bankomaten.Models
{
    internal class Menu
    {
        // Lista med Bankkonton

        // Metod som initialiserar data, antingen befintlig data från fil, eller standardvärden från Account, och returnerar en lista
        private static List<Account> InitializeAccounts()
        {
            string path = "accounts.txt";
            //Kolla om accounts.txt filen finns, om ej, skapa den med standardvärden från Account
            if (!File.Exists(path))
            {
                Account AccountFile = new Account();
                List<Account> AllAccounts = AccountFile.AddAccountData(); //Här kommer standardvärden in från Account

                using StreamWriter userfile = new StreamWriter(path);
                foreach (Account acc in AllAccounts)
                {
                    userfile.WriteLine($"{acc.accountId};{acc.ownerId};{acc.accountNumber};{acc.name};{acc.balance};{acc.accType}");
                }
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
                    AllAccounts.Add(new Account(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), parts[3], decimal.Parse(parts[4]), (Enum.Parse<accountType>(parts[5]))));
                }
                return AllAccounts;
            }

        }

        // Bank meny
        public static void BankMenu(User user)
        {
            List<Account> AllAccounts = InitializeAccounts(); //Här kommer datan in, varesig den kommer från lokal fil eller Account klassen
            List<Account> currentUserAccounts = AllAccounts.Where(a => a.ownerId == user.UserId).ToList();

            Dictionary<int, Account> accountsDict = new();
            for (int i = 0; i < currentUserAccounts.Count; i++)
            {
                accountsDict.Add(i+1, currentUserAccounts[i]);
            }

            int logOut = 0;

            while (logOut != 5)
            {
                ShowMenuOptions();

                if (!Utilities.IntParse(out int userSelection, "Ogiltigt val")) continue;

                switch (userSelection)
                {
                    // Se konton och saldo
                    case 1:
                        Utilities.CheckAmount(accountsDict);
                        break;

                    // Överföring mellan två konton
                    case 2:
                        Utilities.CheckAmount(accountsDict);
                        
                        Console.WriteLine("Vilket konto skall du sätta in på? ");
                        if (!Utilities.IntParse(out int outAcc)) continue;

                        Console.WriteLine("Vilket konto skall du ta ut från? ");
                        if (!Utilities.IntParse(out int inAcc)) continue;

                        Console.WriteLine("Hur mycket skal du föra över? ");
                        if (!Utilities.DecimalParse(out decimal amountTransfer)) continue;

                        var outChosenAccount = accountsDict[outAcc];
                        var innChosenAccount = accountsDict[inAcc];
                        if (innChosenAccount == null || outChosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Utilities.Transfer(amountTransfer, innChosenAccount, outChosenAccount);
                        break;

                    // Sätta in pengar
                    case 3:
                        Utilities.CheckAmount(accountsDict);
                        Console.WriteLine("Vilket konto skall du sätta in på? ");
                        if (!Utilities.IntParse(out int inAccId)) continue;

                        Console.WriteLine("Hur mycket skall du sätta in? ");
                        if (!Utilities.DecimalParse(out decimal inAmount)) continue;

                        var inChosenAccount = accountsDict[inAccId];
                        if (inChosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Utilities.Deposit(inAmount, inChosenAccount);
                        break;

                    // Ta ut pengar
                    case 4:
                        Utilities.CheckAmount(accountsDict);
                        Console.WriteLine("Vilket konto skall du ta ut från ");
                        if (!Utilities.IntParse(out int accId)) continue;

                        Console.WriteLine("Hur mycket skall du ta ut? ");
                        if (!Utilities.DecimalParse(out decimal amount)) continue;

                        var chosenAccount = accountsDict[accId];
                        if (chosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Utilities.WithdrawAmount(amount, chosenAccount);
                        break;

                    // Logga ut
                    case 5:
                        {
                            Console.WriteLine("Du är utloggad");
                            logOut = 5;

                            //När användaren loggar ut, skriv alla kontona till filen så att de sparas
                            string path = "accounts.txt";
                            using StreamWriter userfile = new StreamWriter(path);
                            foreach (Account acc in AllAccounts)
                            {
                                userfile.WriteLine($"{acc.accountId};{acc.ownerId};{acc.accountNumber};{acc.name};{acc.balance};{acc.accType}");
                            }
                            break;
                        }

                    default:
                        Console.WriteLine("Ogiltigt val");
                        break;
                }
            }
        }

        // Metod för att skriva ut en array med meny alternativ
        public static void ShowMenuOptions()
        {
            string[] options = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Sätt in pengar", "4. Ta ut pengar", "5. Logga ut" };

            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
