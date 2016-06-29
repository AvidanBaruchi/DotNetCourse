using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public abstract class Shape
    {
        protected const ConsoleColor _defaultShapeColor = ConsoleColor.White;

        public Shape() : this(_defaultShapeColor)
        { }

        public Shape(ConsoleColor color)
        {
            Color = color;
        }

        public abstract double Area { get; }

        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Changes thecolor of the console to the color represented by the Color property.
        /// </summary>
        public virtual void Display()
        {
            Console.ForegroundColor = Color;
        }
    }
}
