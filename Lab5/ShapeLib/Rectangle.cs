using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Rectangle : Shape, IPersist, IComparable
    {
        private double _width = 0;
        private double _height = 0;

        public Rectangle(double width, double height, ConsoleColor color)
            : base(color)
        {
            Width = width;
            Height = height;
        }

        public Rectangle(double width, double height)
            : this(width, height, _defaultShapeColor)
        { }

        public Rectangle() : base()
        { }

        public double Width
        {
            get { return _width; }
            set
            {
                if(value >= 0)
                {
                    _width = value;
                }
                else
                {
                    Console.WriteLine("Width must have a positive value!");
                }
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if(value >= 0)
                {
                    _height = value;
                }
                else
                {
                    Console.WriteLine("Height must have a positive value!");
                }
            }
        }

        public override double Area
        {
            get
            {
                return Height * Width;
            }
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return string.Format("Rectangle: Width = {0}, Height = {1}", Width, Height);
        }

        public void Write(StringBuilder sb)
        {
            if(sb != null)
            {
                sb.AppendLine(this.ToString());
            }
        }

        public int CompareTo(object obj)
        {
            Rectangle other = obj as Rectangle;

            if (other == null) throw new ArgumentException("Object is not a Rectangle");

            return (int)(Area - other.Area);
        }
    }
}
