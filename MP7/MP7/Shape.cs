using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    abstract class Shape
    {
        public abstract double GetArea();

        public override string ToString()
        {
            return string.Format("{0:F}",this.GetArea());
        }
    }
}
