using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Triangle : Shape, ITriangle
    {
        public Triangle(double side1, double side2, double side3)
        {
            this.Side1 = side1;
            this.Side2 = side2;
            this.Side3 = side3;
        }

        public Triangle(double side1, double side2)
        {
            this.Side1 = side1;
            this.Side2 = side2;
        }
        public override double GetArea()
        {
            return (this.Side1 * this.Side2)/2.0;
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

        public double Side3
        {
            get;
            set;
        }

        public double GetPerimeter()
        {
            return this.Side1 + this.Side2 + this.Side3;
        }
    }
}
