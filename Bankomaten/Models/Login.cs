using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Login
    {
        // Lista med användare
        private static List<User> UserList = new List<User>
        {
            new User(1, "Ammar", "1234"),
            new User(2, "Mattias", "1234"),
            new User(3, "Lukas", "1234"),
            new User(4, "Marco", "1234"),
            new User(5, "Kalle", "1234")
        };
        
        // Metod för att ta input från användaren och validera mot lista med användare  
        public static User? AuthenticateUser()
        {
            bool isAuthenticated;
            int failedAttempts = 0;
            int allowedAttempts = 3;
            User? user = null;

            do
            {
                Console.Write("Användarnamn: ");
                string? username = Console.ReadLine();

                Console.Write("Pinkod: ");
                string? pin = Utilities.GetConsolePassword();

                user = FindUser(username, pin);

                if (user != null)
                {
                    isAuthenticated = true;
                }
                else
                {
                    isAuthenticated = false;
                    failedAttempts++;
                    Console.WriteLine($"Fel användarnamn eller lösenord, du har {allowedAttempts - failedAttempts} försök kvar");
                }
            }
            while (failedAttempts < allowedAttempts && isAuthenticated != true);

            // Lockout om användaren har anget felaktiga uppgifter 3 gånger
            if (failedAttempts == allowedAttempts)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Du har nått max antal försök");

                LockoutClock();
                Console.ResetColor();
            }

            return user;
        }

         // Metod för att hitta användare i listan av användare
        public static User? FindUser(string? username, string? pin)
        {
            User? user = UserList.FirstOrDefault(u => u.UserName == username && u.PassWord == pin);
            return user;
        }

        // Metod för att visa en klocka på 3 minuter om användaren skriver fel uppgifter 3 gånger
        public static void LockoutClock()
        {
            DateTime lockoutEnd = DateTime.Now.AddMinutes(3);
            
            Console.WriteLine("Du kan försöka logga in igen om 3 minuter");
            
            while (lockoutEnd > DateTime.Now)
            {
                TimeSpan lockoutTimeRemaining = lockoutEnd - DateTime.Now;
                
                Console.Write($"\r{lockoutTimeRemaining.Minutes}:{lockoutTimeRemaining.Seconds:D2}");
                Thread.Sleep(1000);
            }
        }
    }
}