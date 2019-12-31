using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;

namespace JoeMidi1
{
    class MappedMidiControl
    {
        // Used by Mapper to capture current CC settings made by a mapping so they can be undone even after the mapping is deactivated/replaced.
        //  Only used for Sustain Pedal at this point.
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
