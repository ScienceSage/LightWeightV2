# LightWeightV2
This is for FRC robots with mechanum drives and will use a kinect and ardiuno to srafe to align with an object

LightWeight is a project for FRC robots with mechanum drive. It will make the robot strafe to align with the nearest object that it detects.

The Application is built off of a kinect project provided by Microsoft and the camera view reflects that, I would never make something that nice for a personal project.

Before you start

    Put the kinect on the front of the robot and make it low, and pointing directly ahead.
    Get an arduio that can read the signals
    Change the com port number that LightWeight uses to communicate with the arduino
    Wire the arduino to the analog input on the roborio (dont wire the power wire, only signal and ground)
    Run the program and cross you fingers

Note: You must write code for the roborio to react to changing analog voltages and the arduino must read Serial chars and then send them as analog voltages
