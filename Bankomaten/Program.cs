using Bankomaten.Models;
using static Bankomaten.Models.Account;

namespace Bankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account1 = new Account("testkonto", 20300, accountType.Sparkonto);
            Account account2 = new Account("testkonto2", 10600, accountType.Depåkonto);
            Account account3 = new Account("testkonto3", 20030, accountType.Lönekonto);

        }
    }
}
