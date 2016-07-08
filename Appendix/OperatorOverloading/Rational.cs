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
            if(denominator == 0)
            {
                throw new DivideByZeroException("Attempted to divide by zero.");
            }

            Numerator = numerator;
            Denominator = denominator;
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

        public override bool Equals(object obj)
        {
            Rational input;

            if(obj == null || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            input = (Rational)obj;

            return input.GetDouble == this.GetDouble;
        }

        public override int GetHashCode()
        {
            return (int)GetDouble;
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

        public static Rational operator +(Rational a, Rational b)
        {
            return a.Add(b);
        }

        public static Rational operator -(Rational a, Rational b)
        {
            return a.Add(new Rational(-b.Numerator, b.Denominator));
        }

        public static Rational operator *(Rational a, Rational b)
        {
            return a.Mul(b);
        }

        public static Rational operator /(Rational a, Rational b)
        {
            return a.Mul(new Rational(b.Denominator, b.Numerator));
        }

        public static implicit operator Rational(int fromInt)
        {
            return new Rational(fromInt);
        }

        public static explicit operator double(Rational rational)
        {
            return rational.GetDouble;
        }
    }
}
