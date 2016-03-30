using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    interface ICircle
    {
        double Radius { get; set; }

        double GetCircumference();
    }
}
