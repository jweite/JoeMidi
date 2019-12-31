using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;

namespace JoeMidi1
{
    class MappedNote
    {
        // Used by Mapper to cature a note that's sounding, so it can be subsequently silenced, even after the mapping that sounded it is
        //  deactivated (replaced by another one).

        public String sourceDeviceName;
        public Channel sourceChannel;
        public Pitch origNote;

        public OutputDevice mappedDevice;
        public Channel mappedChannel;
        public Pitch mappedNote;
    }
}
