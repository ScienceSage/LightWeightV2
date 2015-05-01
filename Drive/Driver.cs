using Microsoft.Samples.Kinect.DepthBasics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.DepthBasics.Drive
{
    class Driver
    {
        ArduinoCommunictor communicator;

        public Driver ()
        {
           communicator = new ArduinoCommunictor();
        }

        public void AdjustAccordingTo(double xCOG)
        {
            int rangeSize = MainWindow.ranges.Length;
            int accuracyThreshold = 0;
            ArduinoCommunictor.Command command = ArduinoCommunictor.Command.CENTER;
            String direction = "";

            // find the two center points
            int middle2 = rangeSize / 2 - 2;
            int middle1 = rangeSize / 2 - 3;

            // determine whether the chassis needs to move and how (trust me it works)
            if (xCOG > middle2 + accuracyThreshold || xCOG < middle1 - accuracyThreshold)
            {
                if (xCOG > middle1)
                {
                    command = ArduinoCommunictor.Command.LEFT;
                    direction = "Left";
                }
                else if (xCOG < middle2)
                {
                    command = ArduinoCommunictor.Command.RIGHT;
                    direction = "Right";
                }
            }
            else
            {
                command = ArduinoCommunictor.Command.CENTER;
                direction = "Center";
            }
            //communicator.SendRotation(direction);
            communicator.SendCommand(command);
            //Console.WriteLine("Direction: " + direction);
        }
    }
}
