using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics.Totes
{
    class Tote
    {
        public bool exists { get; set; }
        public double[] toteRanges { get; set; }
        public int leftEdge { get; set; }
        public int rightEdge { get; set; }
        public int COG { get; set; }
        public int rotation { get; set; }
    }
}
