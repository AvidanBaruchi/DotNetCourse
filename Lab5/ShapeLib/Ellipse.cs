using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Ellipse : Shape, IPersist, IComparable
    {
        private double _width = 0;
        private double _height = 0;

        public Ellipse(double width, double height, ConsoleColor color)
            : base(color)
        {
            Width = width;
            Height = height;
        }

        public Ellipse(double width, double height) 
            : this(width, height, _defaultShapeColor)
        { }

        public Ellipse() : base()
        { }

        public virtual double Width
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

        public virtual double Height
        {
            get { return _height; }
            set
            {
                if (value >= 0)
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
                return Math.PI * Height * Width;
            }
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return string.Format("Ellipse: Width = {0}, Height = {1}", Width, Height);
        }

        public void Write(StringBuilder sb)
        {
            if (sb != null)
            {
                sb.AppendLine(this.ToString());
            }
        }

        public int CompareTo(object obj)
        {
            Ellipse other = obj as Ellipse;

            if (other == null) throw new ArgumentException("Object is not an Ellipse");

            return (int)(Area - other.Area);
        }
    }
}
