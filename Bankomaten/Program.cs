using Bankomaten.Models;
using static Bankomaten.Models.Account;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {

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
