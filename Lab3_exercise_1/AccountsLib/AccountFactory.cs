using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsLib
{
    public static class AccountFactory
    {
        private static int _idCounter = 1;

        public static Account CreateAccount(decimal initialBalance)
        {
            Account account = null;

            if (initialBalance > 0)
            {
                account = new Account(_idCounter++);
                account.Deposit(initialBalance); 
            }
            else
            {
                Console.WriteLine("Cannot Accept a Negative Initial Balance");
            }

            return account;
        }
    }
}
