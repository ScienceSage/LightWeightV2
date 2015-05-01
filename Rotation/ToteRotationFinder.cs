using Microsoft.Samples.Kinect.DepthBasics.Totes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics.Rotation
{
    class ToteRotationFinder
    {
        /**
         * NOTE:
         * 
         * Currently this does not give an exact rotation in degrees. It is only an approximate because the width of the view angle is unkown
         * 
         * I am assuming that the tote is exactly 17in wide in all situations
         * 
         */
        public int FindToteRotation(Tote tote)
        {
            int widthOfTote = 17;
            int changeInDepth = Math.Abs(tote.rightEdge - tote.leftEdge);
            int rotation = (int)Math.Round(Math.Asin(changeInDepth / widthOfTote));

            return rotation;
        }
    }
}
