using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Midi;

namespace JoeMidi1
{
    public class SoundGeneratorChannel
    {
        // A base class for the XXXXXXMapping classes, which all have a SoundGenerator and a relative channel that they use.

        public String soundGeneratorName;
        public int soundGeneratorRelativeChannel;

        public bool Equals(SoundGeneratorChannel other)
        {
            return soundGeneratorName.Equals(other.soundGeneratorName) && soundGeneratorRelativeChannel.Equals(other.soundGeneratorRelativeChannel);
        }


        [JsonIgnore]
        public SoundGenerator soundGenerator;

        [JsonIgnore]
        public int soundGeneratorPhysicalChannel;

        public virtual void bind(Dictionary<String, SoundGenerator> soundGenerators)
        {
            if (soundGenerators.ContainsKey(soundGeneratorName))
            {
                soundGenerator = soundGenerators[soundGeneratorName];

                if (soundGeneratorRelativeChannel >= 0 && soundGeneratorRelativeChannel < soundGenerator.nChannels)
                {
                    soundGeneratorPhysicalChannel = soundGeneratorRelativeChannel + soundGenerator.channelBase;
                }
                else {
                    throw new ConfigurationException("Exception binding SoundGeneratorChannel: Illegal soundGeneratorRelativeChannel: " + soundGeneratorRelativeChannel);
                }
            }
            else
            {
                throw new ConfigurationException("Exception binding SoundGeneratorChannel: unknown SoundGeneratorName " + soundGeneratorName);
            }
        }
    }
}
