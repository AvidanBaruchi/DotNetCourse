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
            if(money >= 0)
            {
                Balance += money;
            }
            else
            {
                PrintNegativeValueError();
            }
        }

        public void Withdraw(decimal money)
        {
            if (money >= 0)
            {
                if(Balance - money >= 0)
                {
                    Balance -= money;
                }
                else
                {
                    Console.WriteLine("throws.. cannot go into overdraft!!!");
                }
            }
            else
            {
                PrintNegativeValueError();
            }
        }

        public void Transfer(Account account, decimal amount)
        {
            if(amount >= 0)
            {
                if(account != null)
                {
                    if(Balance - amount > 0)
                    {
                        this.Withdraw(amount);
                        account.Deposit(amount);
                    }
                    else
                    {
                        Console.WriteLine("throws... Not Enough money in this account to transfer another...");
                    }
                }
                else
                {
                    Console.WriteLine("throws... cannot Access null account");
                }
            }
            else
            {
                PrintNegativeValueError();
            }
        }

        private void PrintNegativeValueError()
        {
            Console.WriteLine("Throws... Cannot Accept a Negative Value!");
        }
    }
}
