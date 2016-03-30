using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Rectangle : Shape , ISquare
    {

        public Rectangle(double side1, double side2)
        {
            this.Side1 = side1;
            this.Side2 = side2;
        }

        public override double GetArea()
        {
            return this.Side1 * this.Side2;
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

        public double GetPerimeter()
        {
            return (2.0 * this.Side1) + (2.0 * this.Side2);
        }
    }
}
