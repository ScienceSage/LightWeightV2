using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics.COG
{
    class COGFinder
    {
        public int FindCOG() // only finds x position of COG
        {
            int COG = int.MaxValue;
            double[] ranges = MainWindow.ranges;

            if (ranges == null)
                return int.MaxValue;

            COG = EdgeDetection(ranges, COG);
            
            return COG;
        }

        private int EdgeDetection (double[] ranges, int COG)
        {
            int edgeThreshold = 6; // inches difference of two adjacent ranges to mark a tote edge
            int maxDetectionRange = 50; // inches that it will start recognizing
            int minRangeIndex = 0;
            int rightEdgeIndex = ranges.Length - 1;
            int leftEdgeIndex = 0;

            //  find the min range to estimate the center of the tote
            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i] < ranges[minRangeIndex] && ranges[i] != 0)
                    minRangeIndex = i;
            }
            if (minRangeIndex > maxDetectionRange)
            {
                return ranges.Length / 2;
                Console.WriteLine("Object is out of range");
                //return int.MaxValue;
            }
                

            // Find the 'ranges' OR edges essentially indexes that contain data on the tote/container

            // Find left edge
            for (int i = minRangeIndex; i > 0; i--)
            {
                if (Math.Abs(ranges[i] - ranges[i - 1]) > edgeThreshold)
                {
                    leftEdgeIndex = i;
                    break;
                }
            }
            // Find right edge
            for (int i = minRangeIndex; i < ranges.Length - 1; i++)
            {
                if (Math.Abs(ranges[i] - ranges[i + 1]) > edgeThreshold)
                {
                    rightEdgeIndex = i;
                    break;
                }
            }

            double domain = (double)rightEdgeIndex + (double)leftEdgeIndex;

            COG = (int)Math.Round(domain / 2);
            Console.WriteLine("Center of Gravity: " + COG + " Min Range: " + minRangeIndex + " Right: " + rightEdgeIndex + " Left: " + leftEdgeIndex );
            return COG;
        }
    }
}
