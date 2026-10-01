using Bankomaten.Models;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login loginUser = new();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Välkommen till Sharp Legend Bank ---");
            Console.ResetColor();

            Console.Write("Användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Lösenord: ");
            string password = Console.ReadLine();

            bool isAuthenticated = loginUser.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                Menu.BankMenu(isAuthenticated);
            }
            else
            {
                Console.WriteLine("Fel användarnamn eller lösenord");
            }
        }
    }
}
