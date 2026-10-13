using System.Security.AccessControl;
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

            //
            Stack<Transactions> currentTransactions = new Stack<Transactions>();

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
                        Utilities.enterPress();
                        break;

                    // Överföring mellan två konton
                    case 2:
                        Utilities.CheckAmount(accountsDict);

                        Console.WriteLine("Vilket konto skall du sätta in på?");
                        if (!Utilities.GenericParse(out int fromAccount)) continue;

                        Console.WriteLine("Vilket konto skall du ta ut från?");
                        if (!Utilities.GenericParse(out int toAccount)) continue;

                        Console.WriteLine("Hur mycket skall du föra över?");
                        if (!Utilities.GenericParse(out decimal amountTransfer)) continue;
                        
                        if (!accountsDict.TryGetValue(fromAccount, out Account? fromChosenAccount) ||
                            !accountsDict.TryGetValue(toAccount, out Account? toChosenAccount))
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }                 

                        Utilities.Transfer(amountTransfer, fromChosenAccount, toChosenAccount);
                        Transactions transaction1 = new Transactions(fromChosenAccount, amountTransfer, '-');
                        Transactions transaction2 = new Transactions(toChosenAccount, amountTransfer, '+');
                        currentTransactions.Push(transaction1);
                        currentTransactions.Push(transaction2);
                        Utilities.enterPress();
                        break;

                    // Sätta in pengar
                    case 3:
                        Utilities.CheckAmount(accountsDict);
                        Console.WriteLine("Vilket konto skall du sätta in på? ");
                        if (!Utilities.GenericParse(out int inAccId)) continue;

                        Console.WriteLine("Hur mycket skall du sätta in? ");
                        if (!Utilities.GenericParse(out decimal inAmount)) continue;

                        if (!accountsDict.TryGetValue(inAccId, out Account? inChosenAccount))
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Utilities.Deposit(inAmount, inChosenAccount);
                        Transactions transaction3 = new Transactions(inChosenAccount, inAmount, '+');
                        currentTransactions.Push(transaction3);
                        Utilities.enterPress();
                        break;

                    // Ta ut pengar
                    case 4:
                        Utilities.CheckAmount(accountsDict);
                        Console.WriteLine("Vilket konto skall du ta ut från ");
                        if (!Utilities.GenericParse(out int accId)) continue;

                        Console.WriteLine("Hur mycket skall du ta ut? ");
                        if (!Utilities.GenericParse(out decimal amount)) continue;

                       
                        if (!accountsDict.TryGetValue(accId,out Account? chosenAccount))
                        {
                            Console.WriteLine("Kontot hittades inte, försök igen.");
                            continue;
                        }

                        Utilities.WithdrawAmount(user, amount, chosenAccount);
                        Transactions transaction4 = new Transactions(chosenAccount, amount, '-');
                        currentTransactions.Push(transaction4);
                        Utilities.enterPress();
                        break;

                    // Öppna nytt konto
                    case 5:
                        Console.WriteLine("Skriv namn på kontot :");
                        string newAccountName = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(newAccountName))
                        {
                            Console.WriteLine("Ogiltigt kontonamn.");
                            continue;
                        }

                        // Skriv ut alla enums och numrera dem till menyn
                        writeAccountTypes();

                        if (!Utilities.GenericParse(out int choice) || choice < 1 || choice > 5)
                        {
                            Console.WriteLine("Vänligen välj en kontotyp från listan.");
                            continue;
                        }

                        accountType newAccountType = (accountType)(choice - 1);

                        // Det nya kontot skapas och läggs till i våra listor och dictionary
                        Account newAccount = new Account(user.UserId, newAccountName, 0, newAccountType);
                        AllAccounts.Add(newAccount);
                        currentUserAccounts.Add(newAccount);
                        accountsDict = indexAccounts(currentUserAccounts);
                        Console.WriteLine($"{newAccountType} skapades med kontonummer {newAccount.accountNumber}");
                        Utilities.enterPress();
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
                if (currentTransactions.Count == 0)
                {
                    //Gör ingenting
                }
                else
                {
                    Transactions.ShowTransactions(currentTransactions);
                }
            }
        }

        // Metod för att skriva ut en array med meny alternativ
        public static void ShowMenuOptions()
        {
            string[] options = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Sätt in pengar", "4. Ta ut pengar", "5. Öppna Konto", "6. Logga ut" };

            Console.WriteLine();

            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
