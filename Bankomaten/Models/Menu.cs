using static Bankomaten.Models.Account;

namespace Bankomaten.Models
{
    internal class Menu
    {
        // Bank meny
        public static void BankMenu(User user)
        {
            //Här kommer datan in, varesig den kommer från lokal fil eller Account klassen
            string path = "accounts.txt";
            List<Account> AllAccounts = InitializeAccounts(path);
            List<Account> currentUserAccounts = AllAccounts.Where(a => a.ownerId == user.UserId).ToList();

            //Dictionary numrerar de aktuella kontona för användaren
            Dictionary<int, Account> accountsDict = new(indexAccounts(currentUserAccounts));

            bool logOut = false;

            while (logOut != true)
            {
                ShowMenuOptions();
                
                if (!Utilities.GenericParse(out int userSelection, "Ogiltigt val")) continue;

                switch (userSelection)
                {
                    // Se konton och saldo
                    case 1:
                        Utilities.CheckAmount(accountsDict);
                        break;

                    // Överföring mellan två konton
                    case 2:
                        Utilities.CheckAmount(accountsDict);

                        Console.WriteLine("Vilket konto skall du sätta in på?");
                        if (!Utilities.GenericParse(out int outAcc)) continue;

                        Console.WriteLine("Vilket konto skall du ta ut från?");
                        if (!Utilities.GenericParse(out int inAcc)) continue;

                        Console.WriteLine("Hur mycket skall du föra över?");
                        if (!Utilities.GenericParse(out decimal amountTransfer)) continue;

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
                        if (!Utilities.GenericParse(out int inAccId)) continue;

                        Console.WriteLine("Hur mycket skall du sätta in? ");
                        if (!Utilities.GenericParse(out decimal inAmount)) continue;

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
                        if (!Utilities.GenericParse(out int accId)) continue;

                        Console.WriteLine("Hur mycket skall du ta ut? ");
                        if (!Utilities.GenericParse(out decimal amount)) continue;

                        var chosenAccount = accountsDict[accId];
                        if (chosenAccount == null)
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Utilities.WithdrawAmount(user, amount, chosenAccount);
                        break;

                    // Öppna nytt konto
                    case 5:
                        Console.WriteLine("Namn på kontot");
                        string newAccountName = Console.ReadLine();
                        Console.WriteLine("Välj kontotyp");
                        Console.WriteLine("1. Sparkonto");
                        Console.WriteLine("2. Lönekonto");
                        Console.WriteLine("3. Depåkonto");

                        if (!Utilities.GenericParse(out int choice)) continue;
                        accountType newAccountType = (accountType)(choice - 1);
                        Account newAccount = new Account(user.UserId, newAccountName, 0, newAccountType);
                        AllAccounts.Add(newAccount);
                        currentUserAccounts.Add(newAccount);
                        accountsDict = indexAccounts(currentUserAccounts);
                        Console.WriteLine($"{newAccountType} skapades med kontonummer {newAccount.accountNumber}");
                        break;

                    // Logga ut
                    case 6:
                        {
                            Console.WriteLine("Du är utloggad");
                            logOut = true;

                            //När användaren loggar ut, skriv alla kontona till filen så att de sparas
                            WriteAccountFile(path, AllAccounts);
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
            string[] options = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Sätt in pengar", "4. Ta ut pengar", "5. Öppna Konto", "6. Logga ut" };

            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
