using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsLib
{
    public class Account
    {
        internal Account(int id)
        {
            ID = id;
        }

        public int ID { get; private set; }

        public decimal Balance { get; private set; }

        public void Deposit(decimal money)
        {
            CheckPositive(money);
            Balance += money;
        }

        public void Withdraw(decimal money)
        {
            CheckPositive(money);

            if (Balance - money >= 0)
            {
                Balance -= money;
            }
            else
            {
                throw new InsufficientFundsException("Cannot Go Into 0verdraft");
            }
        }

        public void Transfer(Account account, decimal amount)
        {
            decimal previousBalance = Balance;

            try
            {
                Withdraw(amount);
                account.Deposit(amount);
            }
            finally
            {
                Console.WriteLine("  A Transfer Attempt has been Made!");
                Console.WriteLine("   Previous Balance: {0}{1}   Current Balance: {2}", previousBalance, System.Environment.NewLine, Balance);
            }
        }

        private void CheckPositive(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(null, "Cannot Accept a Negative Value!");
            }
        }
    }
}
