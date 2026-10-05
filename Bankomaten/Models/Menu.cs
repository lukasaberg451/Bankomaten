using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bankomaten.Models
{
    internal class Menu
    {
        private static List<Account> AllAccounts = new List<Account>()
        {
            new Account(3, "Lukas konto 1", 1000, Account.accountType.Sparkonto),
            new Account(3, "Lukas konto 2", 2000, Account.accountType.Lönekonto),
            new Account(3, "Lukas konto 3", 3000, Account.accountType.Depåkonto),
            new Account(4, "Marco konto 1", 100000, Account.accountType.Sparkonto)
        };
        private static string? accountname;

        public static void BankMenu(User user)
        {
            List<Account> currentUserAccounts = AllAccounts.Where(a => a.OwnerId == user.UserId).ToList();

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
                        user.ShowUserInfo();
                        Bank.CheckAmount(user);
                        break;

                    // Överföring
                    case 2:
                        Console.WriteLine("TODO");
                        break;

                    // Sätta in pengar
                    case 3:
                        Console.WriteLine("TODO");
                        break;

                    // Ta ut pengar
                    case 4:
                        Bank.CheckAmount(user, AllAccounts);
                        Console.WriteLine("Hur mycket skall du ta ut?");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                        accountCode = Console.ReadLine();
                        Bank.WithdrawAmount(accountCode, amount);
                        break;

                    // Logga ut
                    case 5:
                        Console.WriteLine("Du är utloggad");
                        logOut = 5;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        break;
                }
             }
        }

        public static void ShowMenuOptions()
        {
            string[] options = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Sätta in pengar" , "4.Ta ut pengar", "5. Logga ut" };

            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
