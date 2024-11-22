using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoeMidi1
{
    internal class ConfigurationException : Exception
    {
        public ConfigurationException(String message) : base(message) {
        }
    }
}
