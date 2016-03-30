using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Parallelogram : Shape , ISquare
    {
        public Parallelogram(double side1, double side2, double height)
        {
            this.Side1 = side1;
            this.Side2 = side2;
            this.Height = height;
        }

        public override double GetArea()
        {
            return this.Side2 * this.Height;
        }

        public double Side1
        {
            get;
            set;
        }

        public double Side2
        {
            get;
            set;
        }

        public double Height { get; set; }

        public double GetPerimeter()
        {
            return (2.0 * this.Side1) + (2.0 * this.Side2);
        }
    }
}
