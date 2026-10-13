using Bankomaten.Models;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Startpunkt för bankomaten.
            // Flöde: Välkomstmeddelande -> Login -> Menu -> tillbaka till Login vid utloggning.
            // Se README.md för en fullständig översikt över klasser och datastrukturer.

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--- Välkommen till Sharp Legend Bank ---");
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
