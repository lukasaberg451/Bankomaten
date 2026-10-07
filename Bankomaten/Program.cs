using Bankomaten.Models;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO, system overview, modules etc

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n--- Välkommen till Sharp Legend Bank ---");
                Console.ResetColor();

                // Kontrollera om användaren finns
                User? user = Login.AuthenticateUser();

                // Om användaren finns, öppna menyn
                if (user != null)
                {
                    Menu.BankMenu(user);
                }
            }
        }
    }
}