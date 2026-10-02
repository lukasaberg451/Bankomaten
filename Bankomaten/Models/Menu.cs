using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Menu
    {
        public static void BankMenu(User user)
        {
            int logOut = 0;

            while (logOut != 4)
            {
                ShowMenuOptions();

                if (!int.TryParse(Console.ReadLine(), out int userSelection))
                {
                    Console.WriteLine("Ogiltig inmatning, försök igen");
                    break;
                }

                switch (userSelection)
                {
                    // Se konto och saldo
                    case 1:
                        //Bank.CheckAmount(user);
                        Console.WriteLine("TODO");
                        break;

                    // Överföring
                    case 2:
                        Console.WriteLine("TODO");
                        break;

                    // Ta ut pengar
                    case 3:
                        //Bank.WithdrawAmount(user);
                        Console.WriteLine("TODO");
                        break;

                    // Logga ut
                    case 4:
                        Console.WriteLine("Du är utloggad");
                        logOut = 4;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        break;
                }
            }
        }

        public static void ShowMenuOptions()
        {
            string[] options = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Ta ut pengar", "4. Logga ut" };

            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
