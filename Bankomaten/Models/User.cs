using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Text;
using static Bankomaten.Models.Account;

namespace Bankomaten.Models
{
    internal class User
    {

       

        // Lagrar användarens användarnamn
        public string UserName { get; set; }

        // Lagrar användarens lösenord
        public string PassWord { get; set; }
        
        //lagrar ID för använadre
        public int UserId { get; set; }


        // Konstruktor
        public User(int userId, string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
            UserId = userId;
        }

      
    }
}