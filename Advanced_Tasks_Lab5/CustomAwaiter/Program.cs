﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAwaiter
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper helper = new Helper();

            helper.TestIntAwaiter();
            helper.TestProcessAwaiter();

            Console.ReadLine();
        }
    }
}
