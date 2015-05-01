using Microsoft.Samples.Kinect.DepthBasics.Totes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics.COG
{
    class ToteCOGFinder
    {
        private int edgeThreshold = 6; // inches
        private int toteWidthThreshold = 2; // min 'ranges' index width to register as a tote 

        public int FindCOG(Tote tote)
        {
            double domain = (double)tote.leftEdge + (double)tote.rightEdge;

            return (int)Math.Round(domain / 2);
        }

        public int FindToteAndCOG() // only finds x position of COG
        {
            double[] ranges = MainWindow.ranges;

            if (ranges == null)
                return int.MaxValue;

            double closestRangeValue = double.MaxValue;
            int closestRangeIndex = ranges.Length / 2;
            for (int i = 0; i < ranges.Length; i++ )
            {
                if (closestRangeValue > ranges[i] && ranges[i] != 0)
                {
                    closestRangeValue = ranges[i];
                    closestRangeIndex = i;
                }
            }

            int leftEdgeIndex = ranges.Length - 1;
            int rightEdgeIndex = 0;

            // Find left edge
            for (int i = closestRangeIndex; i < ranges.Length - 1; i++)
            {
                if (Math.Abs(ranges[i] - ranges[i + 1]) > edgeThreshold)
                {
                    leftEdgeIndex = i;
                    i = ranges.Length;
                }
            }
            // Find right edge
            for (int i = closestRangeIndex; i > 1; i--)
            {
                if (Math.Abs(ranges[i] - ranges[i - 1]) > edgeThreshold)
                {
                    rightEdgeIndex = i;
                    i = 0;
                }
            }
            // Find COG
            int domain = rightEdgeIndex + leftEdgeIndex;
            
            Console.WriteLine(closestRangeIndex);
            Console.WriteLine(rightEdgeIndex);
            Console.WriteLine(leftEdgeIndex);

            return domain / 2;

            //// Tote will only register if the width of the object is greater than 'toteWidthThreshold'
            //for (int i = 0; i < ranges.Length - 1; i++)
            //{
            //    if (ranges[i] - ranges[i + 1] > edgeThreshold) // left edge
            //    {
            //        if (IsTote(ranges, i))
            //        {
            //            return FindCOG(ranges, i);
            //        }
            //    }
            //}

            //return int.MaxValue;
        }

        private bool IsTote(double[] ranges, int index)
        {
            int width = 0;

            for (index++; index < ranges.Length - 1; index++)
            {
                width++;

                if (ranges[index] - ranges[index + 1] < -edgeThreshold) // right edge
                    break;
            }

            if (width >= toteWidthThreshold)
                return true;
            else
                return false;
        }

        private int FindCOG(double[] ranges, int index)
        {
            int startIndex = index;

            for (index++; index < ranges.Length - 1; index++)
            {
                if (ranges[index] - ranges[index + 1] < -edgeThreshold) // right edge
                    break;
            }
            double domain = (double)startIndex + (double)index;

            return (int)Math.Round(domain / 2);
        }
    }
}
