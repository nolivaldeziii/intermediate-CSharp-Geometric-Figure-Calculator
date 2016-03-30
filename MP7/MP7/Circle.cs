using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Circle : Shape, ICircle
    {

        public Circle(double radius)
        {
            this.Radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * Math.Pow(this.Radius, 2);
        }

        public double Radius { get; set; }


        public double GetCircumference()
        {
            return 2.0 * Math.PI * this.Radius;
        }
    }
}
