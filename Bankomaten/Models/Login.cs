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
            new User("Ammar", "1234", 1),
            new User("Mattias", "1234", 2),
            new User("Lukas", "1234", 3),
            new User("Marco", "1234", 4),
            new User("Kalle", "1234", 5)
        };

        // 
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

            return user;
        }

        // Method för att hitta användare i listan
        public static User FindUser(string username, string password)
        {
            User user = UserList.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            return user;
        }
    }
}