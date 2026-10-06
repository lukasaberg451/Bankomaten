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

        // Method för att ta input från användaren och validera mot lista med användare
        public static User AuthenticateUser()
        {
            bool isAuthenticated = false;
            int failedAttempts = 0;
            int allowedAttempts = 3;
            User user;

            do
            {
                Console.Write("Användarnamn: ");
                string username = Console.ReadLine();

                Console.Write("Pinkod: ");
                string pin = Console.ReadLine();

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
            while (failedAttempts < 3 && isAuthenticated != true);

            if (failedAttempts == 3)
            {
                Console.WriteLine("Du har nått max antal försök");
            }

            return user!;
        }

        // Method för att hitta användare i listan
        public static User FindUser(string username, string pin)
        {
            User user = UserList.FirstOrDefault(u => u.UserName == username && u.PassWord == pin)!;
            return user!;
        }
    }
}