using Bankomaten.Models;
using static Bankomaten.Models.Account;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account1 = new Account("testkonto", 20300, accountType.Sparkonto);
            Account account2 = new Account("testkonto2", 10600, accountType.Depåkonto);
            Account account3 = new Account("testkonto3", 20030, accountType.Lönekonto);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--- Välkommen till Sharp Legend Bank ---");
                Console.ResetColor();

                User user = Login.AuthenticateUser();

                if (user != null)
                {
                    Menu.BankMenu(user);
                }
                else
                {
                    Console.WriteLine("Locked out!");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}