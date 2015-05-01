using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class RangeConverter
    {
        public double ConvertDepthToFeet(int depth)
        {
            double inches = depth * 0.03937008;

            return Math.Round(inches); // that was easy, thought it would be harder
        }
    }
}
