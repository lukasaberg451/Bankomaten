using System.Diagnostics.Metrics;
using static Bankomaten.Models.Account;

namespace Bankomaten.Models
{
    internal class User
    {
        // Lagrar användarens användarnamn
        public string UserName { get; set; }

        // Lagrar användarens lösenord
        public string PassWord { get; set; }

        //Lista på användarens konton
        public List<Account> Accounts { get; set; }





        static void AddUser()
        {
            List<User> listUser = new List<User>();

            User user1 = new User("Ammar", "1234");
            User user2 = new User("Mattias", "1234");
            User user3 = new User("Lukas", "1234");
            User user4 = new User("Marco", "1234");
            User user5 = new User("Kalle", "1234");
        }



        public User(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            Accounts = new List<Account>();
            //Account acc new Account()

            Account account1 = new Account(1, "testkonto", 20300, accountType.Sparkonto);
            Account account2 = new Account(1, "testkonto2", 10600, accountType.Depåkonto);
            Account account3 = new Account(1, "testkonto3", 20030, accountType.Lönekonto);

            Accounts.Add(account1);
            Accounts.Add(account2);
            Accounts.Add(account2);
        }

        public void ShowUserInfo()
        {
            Console.WriteLine($"Användare : {UserName}");

            foreach (Account account in Accounts)
            {
                Console.WriteLine($" - Konto: {account.name} | Saldo: {account.balance}");
            }
        }

        public User FindUser(string userName, string passWord, List<User> listUser)
        {
            User user = listUser.FirstOrDefault(u => u.UserName == userName && u.PassWord == passWord);
            return user;
        }


    }
}
