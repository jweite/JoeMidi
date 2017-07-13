using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Midi;
using Newtonsoft.Json;

namespace JoeMidi1
{

    public class LogicalOutputDevice
    {
        public int logicalOutputDeviceNumber;
        public String logicalDeviceName;
        public List<String> physicalDevicePreferenceList = new List<String>();

        [JsonIgnore]
        public OutputDevice device;

        public bool bind()
        {

            foreach (String preferredDeviceName in physicalDevicePreferenceList)
            {
                foreach (OutputDevice device in OutputDevice.InstalledDevices)
                {
                    if (device.Name.Equals(preferredDeviceName))
                    {
                        this.device = device;
                        try
                        {
                            device.Open();
                            return true;
                        }
                        catch (DeviceException)
                        {
                            MessageBox.Show("Cannot open output device " + device.Name);
                            return false;
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("Device " + device.Name + " already open.  Leaving it open.");
                            return true;
                        }
                    }
                }
            }
            MessageBox.Show("Could not find a preferred physical device for logical output device " + this.logicalDeviceName);
            this.device = null;
            return false;
        }

        public static void createTrialConfiguration(Dictionary<String, LogicalOutputDevice> logicalOutputDeviceDict)
        {
            logicalOutputDeviceDict.Clear();

            LogicalOutputDevice logicalOutputDevice = new LogicalOutputDevice();
            logicalOutputDevice.logicalOutputDeviceNumber = 1;
            logicalOutputDevice.logicalDeviceName = "Output Device 1";
            logicalOutputDevice.physicalDevicePreferenceList.Add("Out To MIDI Yoke:  2");
            logicalOutputDeviceDict.Add(logicalOutputDevice.logicalDeviceName, logicalOutputDevice);

            logicalOutputDevice = new LogicalOutputDevice();
            logicalOutputDevice.logicalOutputDeviceNumber = 2;
            logicalOutputDevice.logicalDeviceName = "Output Device 2";
            logicalOutputDevice.physicalDevicePreferenceList.Add("Out To MIDI Yoke:  3");
            logicalOutputDeviceDict.Add(logicalOutputDevice.logicalDeviceName, logicalOutputDevice);
        }

    }

    public class LogicalInputDevice
    {
        public int logicalInputDeviceNumber;
        public String logicalDeviceName;
        public List<String> physicalDevicePreferenceList = new List<String>();

        [JsonIgnore]
        public InputDevice device;

        public bool bind()
        {

            foreach (String preferredDeviceName in physicalDevicePreferenceList)
            {
                foreach (InputDevice device in InputDevice.InstalledDevices)
                {
                    if (device.Name.Equals(preferredDeviceName))
                    {
                        this.device = device;
                        return true;
                    }
                }
            }
            this.device = null;
            MessageBox.Show("Cannot open any physical input devices configured for logical input device " + logicalDeviceName);
            return false;
        }

        public static void createTrialConfiguration(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict)
        {
            logicalInputDeviceDict.Clear();
            
            LogicalInputDevice logicalInputDevice = new LogicalInputDevice();
            logicalInputDevice.logicalInputDeviceNumber = 1;
            logicalInputDevice.logicalDeviceName = "Input Device 1";
            logicalInputDevice.physicalDevicePreferenceList.Add("Oxygen 61");
            logicalInputDevice.physicalDevicePreferenceList.Add("In From MIDI Yoke:  1");
            logicalInputDeviceDict.Add(logicalInputDevice.logicalDeviceName, logicalInputDevice);
        }

    }
}
