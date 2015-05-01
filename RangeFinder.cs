using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class RangeFinder
    {
        private RangeConverter rangeConverter;

        public RangeFinder()
        {
            rangeConverter = new RangeConverter();
        }
        /// <summary>
        /// Returns an array of depths in feet.
        /// There are 'width' depth points, all on one line area
        /// </summary>
        /// <param name="depthPixels">Array of raw depths in 640x480 format</param>
        /// <param name="print">Print to console?</param>
        /// <returns>array of ranges</returns>
        public double[] RangeArray(DepthImagePixel[] depthPixels, bool print)
        {
            int width = 20; // MUST BE A EVEN NUMBER
            int pixelWidth = 640;

            double[] ranges = new double[width];
            int increment = (int)Math.Round((double)pixelWidth / width);
            int pixelIndex = 107520; // starting line in 'pixelWidth' increments (currently .35 from top)

            for (int i = 0; i < width; i++ )
            {
                int nextPixelIndex = pixelIndex + increment;
                int rawRange = FindMinInRange(depthPixels, pixelIndex, nextPixelIndex);
                ranges[i] = rangeConverter.ConvertDepthToFeet(rawRange);
                pixelIndex = (int)nextPixelIndex;
            }
            //WriteRangeToConsole(ranges);

            return ranges;
        }

        private int FindMinInRange(DepthImagePixel[] depthPixels, int start, int stop)
        {
            int min = int.MaxValue;
            int domain = stop - start;
            double total = 0;
            double count = 0;

            for (int i = 0; i < domain; i++)
            {
                /*int index = start + i;
                int depth = depthPixels[index].Depth;
                if (depth != 0 && depth < min)
                    min = (int)depth;*/

                int index = start + i;
                int depth = depthPixels[index].Depth;
                if (depth != 0 && depth < min)
                {
                    total += depth;
                    count++;
                }

            }

            double average;

            if (count > 0)
            {
                average = total / count;
            }
            else
            {
                return 0;
            }

            /*if (min == int.MaxValue)
                return 0; */

            //return min;
            return (int) Math.Round(average);
        }

        private void WriteRangeToConsole(double[] ranges)
        {
            String output = "";
            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i] < 10)
                    output += "[" + ranges[i] + "]";
                else 
                    output += "[" + ranges[i] + "]";
            }
            Console.WriteLine(output);
        }
    }
}

