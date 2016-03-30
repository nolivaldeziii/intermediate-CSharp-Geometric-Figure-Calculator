using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP2_ValdezIII
{
    interface ISquare
    {
        double Side1 { get; set; }
        double Side2 { get; set; }

        double GetPerimeter();
    }
}
