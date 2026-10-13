namespace Bankomaten.Models
{
    internal class Transactions
    {
        // Fields

        private Account Konto;
        private decimal Pengar;
        private char Operator;

        // Properties
        public Account konto
        {
            get { return Konto; }
            set { Konto = value; }
        }
        public decimal pengar
        {
            get { return Pengar; }
            set { Pengar = value; }
        }
        public char _operator
        {
            get { return Operator; }
            set { Operator = value; }
        }

        // Constructor
        public Transactions(Account transaktionsKonto, decimal transaktionsPengar, char op)
        {
            konto = transaktionsKonto;
            pengar = transaktionsPengar;
            _operator = op;
        }
        public static void ShowTransactions(Stack<Transactions> transactionsStack)
        {
            Console.WriteLine("--- Senaste Transaktioner ---");
            foreach (Transactions trans in transactionsStack)
            {
                Console.WriteLine($"{trans.konto.name} {trans._operator}{trans.pengar:C}");
            }
            Console.WriteLine("-----------------------------");
        }
    }
}
