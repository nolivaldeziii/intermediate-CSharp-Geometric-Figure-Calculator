using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class RightCircularCone : Shape , ICircle
    {

        public RightCircularCone(double radius, double height)
        {
            this.Radius = radius;
            this.Height = height;
        }

        public override double GetArea()
        {
            return (Math.PI * Math.Pow(this.Radius,2)) + (Math.PI* ( this.GetCircumference() ));
        }

        public double Radius
        {
            get;
            set;
        }

        public double Height { get; set; }

        public double GetCircumference()
        {
            return Math.Sqrt((Math.Pow(this.Radius,2)+(Math.Pow(this.Height,2))));
        }
    }
}
