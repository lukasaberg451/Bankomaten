using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Globalization;

namespace Bankomaten.Models
{
    internal class Utilities
    {
        /// <summary>
        /// Kontroller konton, summa, namn och kontotyp
        /// </summary>
        /// <param name="accounts">Alla dina konton om det finns några</param>
        public static void CheckAmount(Dictionary<int, Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("Du har inga konton.");

            }
            Console.WriteLine("\n --- Saldo -----");
            Console.WriteLine("Dina konton:");


            foreach (var acc in accounts)
            {
                Console.WriteLine($"[{acc.Key}] Konto: {acc.Value.name}, Saldo: {acc.Value.balance:C}, Typ: {acc.Value.accType}");
            }
            
        }
        
        
        /// <summary>
        /// Tar ut pengar på ett specifikt konto, men kontrollerar först om det finns tillräckligt 
        /// För att fullföra uttag kontrolleras pin kod.
        /// </summary>
        /// <param name="user">Inloggad användare</param>
        /// <param name="amount">summa som skall dras</param>
        /// <param name="withDrawAccount">vilket konto det gäller</param>
        public static void WithdrawAmount(User user, decimal amount, Account withDrawAccount)
        {

            if (amount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return;
                
            }

            if (amount > withDrawAccount.balance)
            {
                Console.WriteLine("Uttag misslyckades: Det finns inte tillräckligt med pengar på kontot.");
                return;
            }
            Console.WriteLine("Ange pin kod för uttag!");

            bool isAuthenticated;
            int failedAttempts = 0;
            int allowedAttempts = 3;

            do
            {
                string? pinCode = GetConsolePin();

                if (user.Password == pinCode)
                {
                    isAuthenticated = true;
                    withDrawAccount.balance -= amount;
                    Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {withDrawAccount.balance:C}");
                    return;
                }
                else
                {
                    isAuthenticated = false;
                    failedAttempts++;
                    Console.WriteLine($"Fel lösenord, du har {allowedAttempts - failedAttempts} försök kvar");
                    if (allowedAttempts == failedAttempts)
                    {
                        Console.WriteLine("Uttag misslyckades");
                        return;
                    }
                }
            }
            while (failedAttempts < allowedAttempts && isAuthenticated != true);

            
        }

        /// <summary>
        /// Insättning på ett av användarens konto
        /// </summary>
        /// <param name="amount">summa för insättning</param>
        /// <param name="depositAccount">vilket konto det gäller</param>
        public static void Deposit(decimal amount, Account depositAccount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Du försöker lägga in ett minustal på beloppet eller noll");
            }
            else
            {
                depositAccount.balance += amount;
                Console.WriteLine($"Det nya saldot på kontot: {depositAccount.balance}");
            }
            
        }

        /// <summary>
        /// Överför pengar mella två användarkonton. 
        /// Vid misslyckande insättning skall den återställa 
        /// dragningen på konto var summan har dragits med felmeddelande   
        /// </summary>
        /// <param name="amount">Beloppet som skall överföras.</param>
        /// <param name="sourceAccount">Kontot som pengarna dras från.</param>
        /// <param name="targetAccount">kontot som pengarna skal läggas till på.</param>
        public static void Transfer(decimal amount, Account sourceAccount, Account targetAccount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return;
            }

            if (amount > sourceAccount.balance)
            {
                Console.WriteLine("Uttag misslyckades: Det finns inte tillräckligt med pengar på kontot.");
                return;
            }
            var originalBalance = sourceAccount.balance;
            sourceAccount.balance -= amount;
            try
            {
                targetAccount.balance += amount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insättningen misslyckades",ex);
                sourceAccount.balance = originalBalance;
            }
            
            Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {sourceAccount.accType} {sourceAccount.balance:C} och nytt saldo {targetAccount.accType} {targetAccount.balance:C} ");
           
        }

        /// <summary>
        /// Gets the console password.
        /// Hides the characters in password
        /// </summary>
        /// <returns></returns>
        public static string GetConsolePin()
        {

            string pin = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                
                if (key.Key != ConsoleKey.Backspace)
                {
                    pin += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (pin.Length > 0)
                        pin = pin.Substring(0, pin.Length - 1);
                    Console.Write("\b \b");
                }
            }
           
            while (key.Key != ConsoleKey.Enter );
           
            return pin.TrimEnd('\r', '\n');

        }


        // Generic parse för olika datatyper tex int, decimal och double
        public static bool GenericParse<T>(out T result, string errorMessage = "Ogiltig inmatning, försök igen")
            where T : ISpanParsable<T>
        {
            if (T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out result))
            {
                 return true;
            }
            Console.WriteLine(errorMessage);
            return false;
        }

        public static void enterPress()
        {
            Console.Write("Tryck <Enter> för att återgå till meny... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
        }
    }
}
