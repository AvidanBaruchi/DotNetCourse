using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Circle : Ellipse
    {
        public Circle(double radius, ConsoleColor color) : base(radius, radius, color)
        { }

        public Circle(double radius) : base(radius, radius)
        { }

        public Circle() : base(0, 0)
        { }

        public override double Width
        {
            get
            {
                return base.Width;
            }

            set
            {
                base.Width = value;
                base.Height = value;
            }
        }

        public override double Height
        {
            get
            {
                return base.Height;
            }

            set
            {
                base.Height = value;
                base.Width = value;
            }
        }



        // Basically there is no need to override, base.Display() is used where there is no overriding
        public override void Display()
        {
            base.Display();
        }

        public override string ToString()
        {
            return string.Format("Circle: Radius = {0}", Height);
        }
    }
}
