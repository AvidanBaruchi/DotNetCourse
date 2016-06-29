using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rationals
{
    public struct Rational
    {
        public Rational(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            checkValidity();
        }

        public Rational(int numerator)
            : this(numerator, 1)
        {

        }

        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        public double GetDouble
        {
            get
            {
                return ((double)Numerator) / Denominator;
            }
        }

        public Rational Add(Rational number)
        {
            int numerator = 0;
            int denominator = 0;
            Rational returnedValue;

            if(this.Denominator == number.Denominator)
            {
                returnedValue = new Rational(this.Numerator + number.Numerator, this.Denominator);
            }
            else
            {
                denominator = this.Denominator * number.Denominator;
                numerator = (this.Numerator * number.Denominator) + (number.Numerator * this.Denominator);
                returnedValue = new Rational(numerator, denominator);
            }

            return returnedValue;
        }

        public Rational Mul(Rational number)
        {
            return new Rational(this.Numerator * number.Numerator, this.Denominator * number.Denominator);
        }

        public void Reduce()
        {
            int gcd = ComputeGCD();

            Numerator /= gcd;
            Denominator /= gcd;
        }

        public override string ToString()
        {
            bool isMinus = (GetDouble < 0);
            string str = null;
            str = String.Format("{0}/{1} = {2}", Math.Abs(Numerator), Math.Abs(Denominator), GetDouble);

            if (isMinus)
            {
                str = "-" + str;
            }

            return str;
        }

        // Im aware we didnt learn Reflections, but i think it is the only way
        // to avoid runtime errors of value type casting (can't use 'is' or 'as' operators..).
        public override bool Equals(object obj)
        {
            Rational input;

            if(!obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            input = (Rational)obj;

            return input.GetDouble == this.GetDouble;
        }

        private int ComputeGCD()
        {
            int temp = 0;
            int a = Math.Abs(Numerator);
            int b = Math.Abs(Denominator);
            
            while(b != 0)
            {
                temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        private void checkValidity()
        {
            if(Denominator == 0)
            {
                Console.WriteLine("Rational Numbers Cannot Contain a Zero Value of Denominator!!{0}Denominator has Setted to 1!",
                    System.Environment.NewLine);
                Denominator = 1;
            }
        }
    }
}
