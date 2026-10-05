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

        private static List<Account> AllAccounts = new List<Account>();

        //public static List<Account> AllAccounts => _allAccounts;

        public static void CheckAmount(User user)
        {
            
            var accounts = AllAccounts.Where(a => a.OwnerId == user.UserId).ToList();


                Console.WriteLine("\n --- Saldo -----");
                       

                if (accounts.Count == 0)
                {
                    Console.WriteLine("Du har inga konton.");
                    return;
                }

                Console.WriteLine("Dina konton:");
                foreach (var acc in accounts)
                {
                    Console.WriteLine( $"Konto: {acc.name}, Saldo: {acc.balance:C}, Typ: {acc.accType}");
                }
          
        }



        public void WithdrawAmount(Account account, decimal withdrawAmount)
        {
            if (withdrawAmount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return;
            }

            if (withdrawAmount > account.balance)
            {
                Console.WriteLine("Det finns för lite på kontot");
                return;
            }

            account.balance -= withdrawAmount;
            Console.WriteLine($"Uttag: {withdrawAmount:C}. Nytt saldo: {account.balance:C}");
        }


        //public static void Deposit(decimal amount)
        //{
        //    if (amount <= 0)
        //    {
        //        Console.WriteLine("Du försöker lägga in ett minustal på beloppet eller så är det noll");
        //    }
        //    else
        //    {
        //        Account.balance += amount;
        //        Console.WriteLine($"Det nya saldot på kontot: {Account.balance}");
        //    }
        //}


    }

}

