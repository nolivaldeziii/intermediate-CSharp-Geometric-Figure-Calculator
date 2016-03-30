using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Sphere : Circle
    {

        public Sphere(double radius) : base(radius) { }

        public override double GetArea()
        {
            return 4.0 * Math.PI * Math.Pow(this.Radius, 2);
        }
    }
}
