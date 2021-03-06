﻿using DynInvoke.Classes;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            C c = new C();

            try
            {
                Console.WriteLine(InvokeHello(a, "World"));
                Console.WriteLine(InvokeHello(b, "Bagette"));
                Console.WriteLine(InvokeHello(c, "Ninja"));
                Console.WriteLine(InvokeHello(c, null));
            }
            catch (TargetException e)
            {
                Console.WriteLine("TargetException: " + e.Message);
            }
            catch (TargetInvocationException e)
            {
                Console.WriteLine("TargetInvocationException: " + e.Message);
            }
            

            Console.ReadLine();
        }

        private static string InvokeHello(object obj, string message)
        {
            string invokedResult = null;
            MemberInfo[] members = null;
            MethodInfo currentMethod = null;
            ParameterInfo[] parameters = null;

            if (obj != null)
            {
                members = obj.GetType().GetMembers(BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.DeclaredOnly);

                foreach (var member in members)
                {
                    if (member.MemberType == MemberTypes.Method)
                    {
                        currentMethod = member as MethodInfo;

                        if (currentMethod != null)
                        {
                            if (currentMethod.Name == "Hello")
                            {
                                parameters = currentMethod.GetParameters();

                                if (parameters.Length == 1)
                                {
                                    if (parameters[0].ParameterType.Equals(typeof(string))
                                        && currentMethod.ReturnType.Equals(typeof(string)))
                                    {
                                        var param = new object[] { message };
                                        invokedResult = (string)currentMethod.Invoke(obj, param);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else {
                throw new ArgumentNullException("obj", "cannot accept a null object");
            }

            return invokedResult;
        }
    }
}
