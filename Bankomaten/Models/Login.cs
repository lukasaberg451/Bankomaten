using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Models
{
    internal class Login
    {
        private bool isAuthenticated = false;
        public bool AuthenticateUser(string userName, string password)
        {
            isAuthenticated = true;
            return isAuthenticated;
        }

        



    }
}
