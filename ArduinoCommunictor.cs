using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Microsoft.Samples.Kinect.DepthBasics.Properties
{
    class ArduinoCommunictor
    {
        private SerialPort port;
        public enum Command { RIGHT, LEFT, CENTER };

        public ArduinoCommunictor()
        {
            port = new SerialPort("COM4", 9600);
            port.Open();
        }

        public void SendCommand(Command command)
        {
            String output = "N";

            switch (command)
            {
                case Command.CENTER:
                    output = "C";
                    break;
                case Command.LEFT:
                    output = "L";
                    break;
                case Command.RIGHT:
                    output = "R";
                    break;
            }

            port.Write(output);
            Console.WriteLine("Ouput: " + output);
        }

        /// <summary>
        /// You can send a rotation value from 25.38 to 154.62 to an arduino with code similar to
        /// pos = Serial.read();
        /// pos = map(pos, 65, 90, 20, 160);
        /// </summary>
        /// <param name="rotation">value in degrees centered at 90</param>
        public void SendRotation(int rotation)
        {
            rotation += 90;

            if (rotation == 90) {
                return;
            }

            String output = "";

            if (rotation <= 25.38)
            {
                output = "A";
            } 
            else if (rotation <= 30.77)
            {
                output = "B";
            }
            else if (rotation <= 36.15)
            {
                output = "C";
            }
            else if (rotation <= 41.54)
            {
                output = "D";
            }
            else if (rotation <= 46.92)
            {
                output = "E";
            }
            else if (rotation <= 52.31)
            {
                output = "F";
            }
            else if (rotation <= 57.69)
            {
                output = "G";
            }
            else if (rotation <= 63.08)
            {
                output = "H";
            }
            else if (rotation <= 68.46)
            {
                output = "I";
            }
            else if (rotation <= 73.85)
            {
                output = "J";
            }
            else if (rotation <= 79.23)
            {
                output = "K";
            }
            else if (rotation <= 84.62)
            {
                output = "L";
            }
            else if (rotation <= 90)
            {
                output = "M";
            }
            else if (rotation <= 95.38)
            {
                output = "N";
            }
            else if (rotation <= 100.77)
            {
                output = "O";
            }
            else if (rotation <= 106.15)
            {
                output = "P";
            }
            else if (rotation <= 111.54)
            {
                output = "Q";
            }
            else if (rotation <= 116.92)
            {
                output = "R";
            }
            else if (rotation <= 122.31)
            {
                output = "S";
            }
            else if (rotation <= 127.67)
            {
                output = "T";
            }
            else if (rotation <= 133.08)
            {
                output = "U";
            }
            else if (rotation <= 138.46)
            {
                output = "V";
            }
            else if (rotation <= 143.85)
            {
                output = "W";
            }
            else if (rotation <= 149.23)
            {
                output = "X";
            }
            else if (rotation <= 154.62)
            {
                output = "Y";
            }
            else if (rotation > 154.62)
            {
                output = "Z";
            }

            //output = "Z";

            port.Write(output);
        }
    }
}
