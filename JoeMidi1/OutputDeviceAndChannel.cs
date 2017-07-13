using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;
using Newtonsoft.Json;

namespace JoeMidi1
{
    class OutputDeviceAndChannel
    {
        public String deviceName;
        public Channel channel;

        [JsonIgnore]
        public OutputDevice device;
    }
}
