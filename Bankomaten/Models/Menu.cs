using static Bankomaten.Models.Account;

namespace Bankomaten.Models
{
    internal class Menu
    {
        // Lista med Bankkonton

        private static List<Account> InitializeAccounts()
        {
            string path = "accounts.txt";
            //Kolla om Accounts filen finns, om ej, skapa den med standard kontona
            if (!File.Exists(path))
            {
                Account AccountFile = new Account();
                List<Account> AllAccounts = AccountFile.AddAccountData();
                using StreamWriter userfile = new StreamWriter(path);
                foreach (Account acc in AllAccounts)
                {
                    userfile.WriteLine($"{acc.accountId};{acc.ownerId};{acc.accountNumber};{acc.name};{acc.balance};{acc.accType}");
                }
                return AllAccounts;
            }
            //Om Accounts filen finns, läs kontona från filen och använd datan därifrån
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
            // Initiera textfilen med konton och lägg in i en lista
            List<Account> AllAccounts = InitializeAccounts();
            List<Account> currentUserAccounts = AllAccounts.Where(a => a.ownerId == user.UserId).ToList();

            int logOut = 0;

            while (logOut != 5)
            {
                ShowMenuOptions();

                if (!int.TryParse(Console.ReadLine(), out int userSelection))
                {
                    Console.WriteLine("Ogiltig inmatning, försök igen");
                    break;
                }

                switch (userSelection)
                {
                    // Se konton och saldo
                    case 1:
                        Bank.CheckAmount(currentUserAccounts);
                        break;

                    // Överföring
                    case 2:
                        Bank.CheckAmount(currentUserAccounts);
                        Console.WriteLine("Vilket konto skall du sätta in på? ");
                        if (!int.TryParse(Console.ReadLine(), out int outAcc))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }
                        Console.WriteLine("Vilket konto skall du ta ut från? ");
                        if (!int.TryParse(Console.ReadLine(), out int inAcc))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }
                        Console.WriteLine("Hur mycket skal du föra över? ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amountTransfer))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }
                        var outChosenAccount = currentUserAccounts.FirstOrDefault(a => a.accountId == outAcc);
                        var innChosenAccount = currentUserAccounts.FirstOrDefault(a => a.accountId == inAcc);
                        if (innChosenAccount == null || outChosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Bank.Transfer(amountTransfer, innChosenAccount, outChosenAccount);
                        break;

                    // Sätta in pengar
                    case 3:
                        Bank.CheckAmount(currentUserAccounts);
                        Console.WriteLine("Vilket konto skall du sätta in på? ");
                        if (!int.TryParse(Console.ReadLine(), out int inAccId))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }
                        Console.WriteLine("Hur mycket skall du sätta in? ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal inAmount))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }

                        var inChosenAccount = currentUserAccounts.FirstOrDefault(a => a.accountId == inAccId);
                        if (inChosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Bank.Deposit(inAmount, inChosenAccount);
                        break;

                    // Ta ut pengar
                    case 4:
                        Bank.CheckAmount(currentUserAccounts);
                        Console.WriteLine("Vilket konto skall du ta ut från ");
                        if (!int.TryParse(Console.ReadLine(), out int accId))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }
                        Console.WriteLine("Hur mycket skall du ta ut? ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine("Ogiltig inmatning, försök igen");
                            continue;
                        }

                        var chosenAccount = currentUserAccounts.FirstOrDefault(a => a.accountId == accId);
                        if (chosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Bank.WithdrawAmount(amount, chosenAccount);
                        break;

                    // Logga ut
                    case 5:
                        {
                            Console.WriteLine("Du är utloggad");
                            logOut = 5;
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

        // Method för att skriva ut en array med meny alternativ
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
