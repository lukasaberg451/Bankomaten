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


        public static void CheckAmount(List<Account> accounts  )
        {
            
                Console.WriteLine("\n --- Saldo -----");
        
                if (accounts.Count == 0)
                {
                    Console.WriteLine("Du har inga konton.");
                    return;
                }

                Console.WriteLine("Dina konton:");
                foreach (var acc in accounts)
                {
                    Console.WriteLine( $"KontoId: {acc.accountId} Konto: {acc.name}, Saldo: {acc.balance:C}, Typ: {acc.accType}");
                }
          
        }


        public static void WithdrawAmount(decimal amount, Account account)
        {

            if (amount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return;
            }

            if (amount > account.balance)
            {
                Console.WriteLine("Uttag misslyckades: Det finns inte tillräckligt med pengar på kontot.");
                return;
            }

            account.balance -= amount;
            Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {account.balance:C}");
            
        }


        public static void Deposit(decimal amount, Account account)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Du försöker lägga in ett minustal på beloppet eller noll");
            }
            else
            {
                account.balance += amount;
                Console.WriteLine($"Det nya saldot på kontot: {account.balance}");
            }
        }

        public static void Transfer(decimal amount,Account accountIn, Account account)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return;
            }

            if (amount > accountIn.balance)
            {
                Console.WriteLine("Uttag misslyckades: Det finns inte tillräckligt med pengar på kontot.");
                return;
            }

            accountIn.balance -= amount;
            account.balance += amount;
            Console.WriteLine($"Uttag: {amount:C}. Nytt saldo: {account.accType} {account.balance:C} kr och nytt saldo {accountIn.accType} {accountIn.balance:C} kr");
        }
    }

}

