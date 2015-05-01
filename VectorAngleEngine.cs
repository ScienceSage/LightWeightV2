using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class VectorAngleEngine
    {
        public int VectorAngle(double[] ranges)
        {
            double angle = 0;
            int primeIndex = HighestPriority(ranges);

            if (primeIndex != int.MaxValue)
                angle = TrajectoryChange(primeIndex, ranges);

            //Console.WriteLine(angle);

            return (int)Math.Round(angle);
        }

        /// <summary>
        /// Finds the highest danger index
        /// Currently the smallest number
        /// </summary>
        private int HighestPriority(double[] ranges)
        {
            int halfLength = ranges.Length / 2;
            // Left side
            double leftValue = double.MaxValue;
            int leftIndex = int.MaxValue;
            for (int i = 0; i < halfLength; i++)
            {
                double value = ranges[i];

                if (value <= leftValue && value != 0)
                {
                    leftValue = value;
                    leftIndex = i;
                }
            }

            // Right side
            double rightValue = double.MaxValue;
            int rightIndex = int.MaxValue;
            for (int i = halfLength; i < ranges.Length; i++)
            {
                double value = ranges[i];

                if (value < rightValue && value != 0)
                {
                    rightValue = value;
                    rightIndex = i;
                }
            }
            //Console.WriteLine(leftValue + ", " + rightValue);
            if (rightValue < leftValue)
                return rightIndex;
            else if (leftValue < rightValue)
                return leftIndex;
            else
                return int.MaxValue;
        }

        /// <summary>
        /// There is a field of view of 57 degrees total according to wikipedia
        /// </summary>
        /// <returns>Change of trajectory in degrees</returns>
        private double TrajectoryChange(int primeIndex, double[] ranges)
        {
            /*
             * Here we use a slope field equation using 3 variables
             * NOTE: This is where machine learning could be applied
             *          Or you could just do guess and check
             * 
             * r(x, y) = 18 (x) - 5 (y)
             * 
             * Where
             * 
             * r = rotation
             * x = range prime index
             * y = depth of prime index range
             * T(x) = length of range array
             * T(y) = max value of depth
             * 
             * When
             * 
             * x = [0, T(x)/2)
             * ie if the prime index is on the left side
             * 
             */

            double rotation = 0; // degrees
            int X = primeIndex;
            double Y = ranges[X];
            int Tx = ranges.Length;
            int Ty = 20; // theoretically

            
            // These should be open for change
            int C = 0;
            int Cx = 30;
            int Cy = 3;

            if (X < Tx / 2)
            {
                rotation =  -1 * Cx * X + Cy * Y;
            }
            else
            {
                rotation = 1 * Cx * (9 - X) - Cy * Y;
            }

            Console.WriteLine(rotation);

            return rotation;
        }

    }
}
