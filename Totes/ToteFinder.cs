using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics.Totes
{
    class ToteFinder
    {
        private int edgeThreshold = 6; // inches
        private int toteWidthThreshold = 3; // min 'ranges' index width to register as a tote 
        private int toteWidth = 0;

        public Tote findTote()
        {
            Tote tote = new Tote();
            double[] ranges;

            if (MainWindow.ranges != null)
                ranges = MainWindow.ranges;
            else
            {
                tote.exists = false;
                return tote;
            }
            
            // Tote will only register if the width of the object is greater than 'toteWidthThreshold'
            for (int i = 0; i < ranges.Length - 1; i++)
            {
                if (ranges[i] - ranges[i+1] > edgeThreshold) // left edge
                {
                    if (IsTote(ranges, i))
                    {
                        tote.exists = true;
                        return FindCOG(ranges, i, tote);
                    }
                }
            }

            return tote;
        }

        private bool IsTote(double[] ranges, int index)
        {
            for (index++; index < ranges.Length; index++)
            {
                toteWidth++;

                if (index + 1 < ranges.Length)
                {
                    if (ranges[index] - ranges[index + 1] < -edgeThreshold) // right edge
                        break;
                }
                else
                {
                    break;
                }
            }

            if (toteWidth >= toteWidthThreshold)
                return true;
            else
                return false;
        }

        private Tote FindCOG(double[] ranges, int index, Tote tote)
        {
            int startIndex = index;
            double[] toteRanges = new double[toteWidth];
            int toteRangesIndex = 0;

            toteRanges[toteRangesIndex] = ranges[index];
            for (index++; index < ranges.Length; index++)
            {
                toteRangesIndex++;
                if (toteRangesIndex < toteRanges.Length)
                    toteRanges[toteRangesIndex] = ranges[index];

                //if (ranges[index] - ranges[index + 1] < -edgeThreshold) // right edge
                //    break;
            }

            tote.toteRanges = toteRanges;
            tote.leftEdge = startIndex;
            tote.rightEdge = index;
            //Console.WriteLine("Left: {0} Right: {1}", startIndex, index);
            return tote;
        }
    }
}
