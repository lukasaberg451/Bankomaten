using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Login
    {
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

                user = User.FindUser(username, pin);

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
    }
}