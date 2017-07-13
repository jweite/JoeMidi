using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;

namespace JoeMidi1
{
    class MappedNote
    {
        public String sourceDeviceName;
        public Channel sourceChannel;
        public Pitch origNote;

        public OutputDevice mappedDevice;
        public Channel mappedChannel;
        public Pitch mappedNote;
    }
}
