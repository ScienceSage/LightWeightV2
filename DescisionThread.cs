using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Samples.Kinect.DepthBasics.Properties;

namespace Microsoft.Samples.Kinect.DepthBasics
{
    class DescisionThread
    {
        private ArduinoCommunictor arduinoCom;
        private VectorAngleEngine vectorEngine;

        private readonly int threshold = 5;

        public DescisionThread()
        {
            //arduinoCom = new ArduinoCommunictor();
            vectorEngine = new VectorAngleEngine();
        }

        public void Decide()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1); // must be less than the arduino delay() value

                double[] ranges = MainWindow.ranges;

                if (IsEmergency(ranges, threshold))
                {
                    int rotation = vectorEngine.VectorAngle(ranges);
                    //arduinoCom.SendRotation(rotation); // value -70 to 70
                }
            }
        }

        // Will change according to velocity ideally
        private bool IsEmergency(double[] ranges, int threshold)
        {
            if (ranges == null)
                return false;

            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i] <= threshold && ranges[i] != 0)
                    return true;
            }
            return false;
        }
    }
}
