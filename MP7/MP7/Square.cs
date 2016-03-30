using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    class Square : Shape, ISquare
    {

        public Square(double side1)
        {
            this.Side1 = side1;
  
        }

        public override double GetArea()
        {
            return this.Side1 * this.Side1;
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
            return (4 * this.Side1);
        }
    }
}
