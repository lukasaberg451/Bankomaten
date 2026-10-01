using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Menu
    {
        public static void BankMenu(bool isAuthenticated)
        {
            while (isAuthenticated)
            {
                ShowMenuOptions();

                if (!int.TryParse(Console.ReadLine(), out int userSelection))
                {
                    Console.WriteLine("Ogiltig inmatning, försök igen");
                    break;
                }

                switch (userSelection)
                {
                    case 1:
                        //CheckAmount()
                        Console.WriteLine("TODO, konto och saldo"); // Se konto och saldo
                        break;

                    case 2:
                        Console.WriteLine("TODO, överföring"); // Överföring
                        break;

                    case 3:
                        //WithdrawAmount()
                        Console.WriteLine("TODO, ta ut pengar"); // Ta ut pengar
                        break;

                    case 4:
                        Console.WriteLine("TODO, logga ut"); // Logga ut
                        isAuthenticated = false;
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
