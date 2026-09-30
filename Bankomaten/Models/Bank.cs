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
        public void CheckAmount()
        {
            try
            {

                Console.WriteLine("\n --- Saldo -----");
                Console.Write("Vilket konto vill du kontrollera:");

                int accountnumber = int.Parse(Console.ReadLine());

                Account? account = Accounts.?????(accountnumber);

                if (account != null)
                {
                    Console.WriteLine($"Saldo på kontot: {Account.balance}");
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

        public void WithdrawAmount()
        {
            try
            {
                Console.Write("Hur mycket vill du lägga in: ");
                decimal sum = decimal.Parse(Console.ReadLine());
                decimal accounttotal = Accounts.balance;

                if (accounttotal > sum)
                {
                    decimal total = accounttotal - sum;
                    Accounts.balance = total;
                    Console.WriteLine($"Det nya saldot på kontot: {Accounts.balance}" );

                }
                else
                {
                    Console.WriteLine("Det finns för lite på kontot");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input: Inmatningen var inte ett giltigt tal");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod: {ex.Message}");
            }

        }
        }

    }
}
