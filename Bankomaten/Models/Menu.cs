using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bankomaten.Models
{
    internal class Menu
    {
        // Lista med Bankkonton
        private static List<Account> AllAccounts = new List<Account>()
        {
            // Ammar konton
            new Account(1, "Ammar Sparkonto", 1000, Account.accountType.Sparkonto),
            new Account(1, "Ammar Lönekonto", 2000, Account.accountType.Lönekonto),
            new Account(1, "Ammar Depåkonto", 3000, Account.accountType.Depåkonto),

            // Mattias konton
            new Account(2, "Mattias Sparkonto", 1000, Account.accountType.Sparkonto),
            new Account(2, "Mattias Lönekonto", 2000, Account.accountType.Lönekonto),
            new Account(2, "Mattias Depåkonto", 3000, Account.accountType.Depåkonto),

            // Lukas konton
            new Account(3, "Lukas Sparkonto", 1000, Account.accountType.Sparkonto),
            new Account(3, "Lukas Lönekonto", 2000, Account.accountType.Lönekonto),
            new Account(3, "Lukas Depåkonto", 3000, Account.accountType.Depåkonto),

            // Marco konton
            new Account(4, "Marco Sparkonto", 1000, Account.accountType.Sparkonto),
            new Account(4, "Marco Lönekonto", 2000, Account.accountType.Lönekonto),
            new Account(4, "Marco Depåkonto", 3000, Account.accountType.Depåkonto),

            // Kalle konton
            new Account(5, "Kalle Sparkonto", 1000, Account.accountType.Sparkonto),
            new Account(5, "Kalle Lönekonto", 2000, Account.accountType.Lönekonto),
            new Account(5, "Kalle Depåkonto", 3000, Account.accountType.Depåkonto)
        };
        private static string? accountname;

        // Bank meny
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
                        Console.WriteLine("TODO");
                        //Bank.CheckAmount(user, currentUserAccounts);
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
