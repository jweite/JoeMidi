using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;

namespace JoeMidi1
{
    class MappedMidiControl
    {
        public String sourceDeviceName;
        public Channel sourceChannel;
        public Control sourceControl;
        public int sourceValue;

        public OutputDevice mappedDevice;
        public Channel mappedChannel;
        public Control mappedControl;
        public int mappedValue;
    }
}
