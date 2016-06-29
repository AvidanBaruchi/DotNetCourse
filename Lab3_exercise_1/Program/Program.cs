using AccountsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Account a1 = AccountFactory.CreateAccount(500);
            Account a2 = AccountFactory.CreateAccount(500);
            Account a3 = AccountFactory.CreateAccount(500);
            bool done = false;
            int choice = 0;

            Console.WriteLine("Hello Account");

            while (!done)
            {
                ShowMenu();
                choice = (int)GetNumber();
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("The Account has {0:N} amount of money", a1.Balance);
                        break;
                    case 2:
                        DepositToAccount(a1);
                        break;
                    case 3:
                        WithdrawFromAccount(a1);
                        break;
                    case 0:
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choice!");
                        break;
                }                
            }


            Console.WriteLine("performing a money transfer...");
            Console.WriteLine("first account has {0} amount of money", a2.Balance);
            Console.WriteLine("second account has {0} amount of money", a3.Balance);
            a2.Transfer(a3, 250m);
            Console.WriteLine("first account has {0} amount of money", a2.Balance);
            Console.WriteLine("second account has {0} amount of money", a3.Balance);

            Console.ReadLine();
        }

        static void ShowMenu()
        {
            Console.WriteLine("Please Choose");
            Console.WriteLine(" 1. Query Account");
            Console.WriteLine(" 2. Deposit");
            Console.WriteLine(" 3. Withdraw");
            Console.WriteLine(" 0. Exit");
        }

        static void DepositToAccount(Account account)
        {
            decimal amount = 0;

            Console.WriteLine("How much money do you like to deposit?");
            amount = GetNumber();
            account.Deposit(amount);
        }

        static void WithdrawFromAccount(Account account)
        {
            decimal amount = 0;

            Console.WriteLine("How much money do you like to withdraw?");
            amount = GetNumber();
            account.Withdraw(amount);
        }

        static decimal GetNumber()
        {
            string input = null;
            bool parsed = false;
            decimal number = 0;

            while (!parsed)
            {
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out number);

                if (!parsed)
                {
                    Console.WriteLine("Wrong input, please enter again!");
                }
            }

            return number;
        }

        /// <summary>
        /// my test method. not part of the task.
        /// </summary>
        private static void Test()
        {
            Account a1 = AccountFactory.CreateAccount(-25m);
            Account a2 = AccountFactory.CreateAccount(200.32m);
            Account a3 = AccountFactory.CreateAccount(200);

            PrintAccount(a1, "a1");
            PrintAccount(a2, "a2");
            PrintAccount(a3, "a3");

            Console.WriteLine("transfer from a2 to a3, 50$");
            a2.Transfer(a3, 50);
            PrintAccount(a2, "a2");
            PrintAccount(a3, "a3");

            Console.WriteLine("a2 goes shopping, draining its money");
            while (a2.Balance > 10)
            {
                a2.Withdraw(10);
            }

            PrintAccount(a2, "a2");
            Console.WriteLine("a2 overdraft..");
            a2.Withdraw(2);
            Console.WriteLine("try to add money to a2 with the withdraw method..");
            a2.Withdraw(-10);
            Console.WriteLine("Add Enourmous amount of money to a2");
            PrintAccount(a2, "a2");
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            a2.Deposit(9999999999999999999);
            PrintAccount(a2, "a2");
        }

        /// <summary>
        /// used by my test method that is not part of the task.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        private static void PrintAccount(Account account, string name)
        {
            if (account != null)
            {
                Console.WriteLine(name + " id = {0}, balance = {1:N2}", account.ID, account.Balance);
            }
            else
            {
                Console.WriteLine(name + " is null!");
            }
        }
    }
}
