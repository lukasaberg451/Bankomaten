using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;


namespace Bankomaten.Models
{
    internal class Bank
    {

        /// <summary>
        /// Kontroller konton, summa, namn och kontotyp
        /// </summary>
        /// <param name="accounts">Alla dina konton om det finns några</param>
        public static void CheckAmount(List<Account> accounts  )
        {
            
        
                if (accounts.Count == 0)
                {
                    Console.WriteLine("Du har inga konton.");
                    return;
                }
                Console.WriteLine("\n --- Saldo -----");
                Console.WriteLine("Dina konton:");
                foreach (var acc in accounts)
                {
                    Console.WriteLine( $"KontoId: {acc.accountId} Konto: {acc.name}, Saldo: {acc.balance:C}, Typ: {acc.accType}");
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
        public static void Transfer(decimal amount,Account sourceAccount, Account targetAccount)
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
            finally 
            { sourceAccount.balance = originalBalance; }
            Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {sourceAccount.accType} {sourceAccount.balance:C} kr och nytt saldo {targetAccount.accType} {targetAccount.balance:C} kr");
        }
    }

}

