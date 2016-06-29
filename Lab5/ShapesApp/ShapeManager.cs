using ShapeLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApp
{
    class ShapeManager
    {
        private ArrayList _shapes = new ArrayList();

        public void Add(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void DisplayAll()
        {
            Shape currentShape = null;

            foreach (var shape in _shapes)
            {
                currentShape = shape as Shape;
                
                if(currentShape != null)
                {
                    currentShape.Display();
                    Console.WriteLine("Area: {0}", currentShape.Area);
                }
            }
        }

        public Shape this[int index]
        {
            get
            {
                return _shapes[index] as Shape;
            }
            private set
            {
                /* set the specified index to value here */
                _shapes[index] = value;
            }
        }

        public int Count { get { return _shapes.Count; } }

        public void Save(StringBuilder builder)
        {
            IPersist currentPersistant = null;

            if(builder != null)
            {
                foreach (Shape shape in _shapes)
                {
                    currentPersistant = shape as IPersist;

                    if(currentPersistant != null)
                    {
                        currentPersistant.Write(builder);
                    }
                }
            }
        }
    }
}
