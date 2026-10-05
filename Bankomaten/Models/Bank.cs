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
        private static Account _account;

        public Bank(Account account) { 
        
            _account = account;
        }
        public void CheckAmount(User user)
        {
            var user = new User();
            
            try
            {

                Console.WriteLine("\n --- Saldo -----");
                Console.Write("Vilket konto vill du kontrollera:");

                int accountnumber = int.Parse(Console.ReadLine());

                Account? account = user.GetAccount(accountnumber);

                if (account != null)
                {
                    Console.WriteLine($"Saldo på kontot: {account.balance}");
                }
                else
                {
                    Console.WriteLine("Account not found!");
                }
            }

            catch (FormatException)
            {
                Console.WriteLine("Invalid account number: Inmatningen var inte ett giltigt heltal");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod: {ex.Message}");
            }
        }

        public static void WithdrawAmount(decimal withdrawamount, ref decimal balance)
        {
             
                if (balance <= withdrawamount)
                {
                    Console.WriteLine("Det finns för lite på kontot");
                }
                else
                {
                    decimal total = balance - withdrawamount;
                    balance = total;
                    Console.WriteLine($"Det nya saldot på kontot: {balance}");

                }
        }

        public static void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Du försöker lägga in ett minustal på beloppet eller så är det noll");
            }
            else
            {
                _account.balance += amount;
                Console.WriteLine($"Det nya saldot på kontot: {_account.balance}");
            }
        }
    }

}

