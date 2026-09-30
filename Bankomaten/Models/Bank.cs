using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
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

    }
}
