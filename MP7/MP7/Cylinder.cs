using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Cylinder : Shape , ICircle
    {
        public Cylinder(double radius, double height)
        {
            this.Radius = radius;
            this.Height = height;
        }

        public override double GetArea()
        {
            return (2.0 * Math.PI * this.Radius) * (this.Radius + this.Height);
        }

        public double Height { get; set; }

        public double Radius
        {
            get;
            set;
        }

        public double GetCircumference()
        {
            return 2.0 * Math.PI * this.Radius;
        }
    }
}
