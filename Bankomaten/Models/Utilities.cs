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
        /// </summary>
        /// <param name="amount">summa som skall dras</param>
        /// <param name="withDrawAccount">vilket konto det gäller</param>
        public static void WithdrawAmount(decimal amount, Account withDrawAccount)
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

            withDrawAccount.balance -= amount;
            Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {withDrawAccount.balance:C}");

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
                Console.WriteLine("Insättningen misslyckades");
            }
            Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {sourceAccount.accType} {sourceAccount.balance:C} och nytt saldo {targetAccount.accType} {targetAccount.balance:C}");
        }

        // Metod för parse input med Int
        public static bool IntParse(out int result, string errorMessage = "Ogiltig inmatning, försök igen")
        {
            if (!int.TryParse(Console.ReadLine(), out result))
            {
                return true;
                
            }
            Console.WriteLine(errorMessage);
            return false;
        }

        // Metod för parse input med Double
        public static bool DoubleParse(out double result, string errorMessage = "Ogiltig inmatning, försök igen")
        {
            if (!double.TryParse(Console.ReadLine(), out result))
            {
                return true;
            }
            Console.WriteLine(errorMessage);
            return false;
        }

        // Metod för parse input med Decimal
        public static bool DecimalParse(out decimal result, string errorMessage = "Ogiltig inmatning, försök igen")
        {
            if (!decimal.TryParse(Console.ReadLine(), out result))
            {
                return true;
            }
            Console.WriteLine(errorMessage);
            return false;
        }
    }

}

