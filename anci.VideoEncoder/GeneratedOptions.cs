using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anci.VideoEncoder
{
    public class GeneratedOptions
    {
        public string extension { get; internal set; }
        public string subremove { get; internal set; }
        public string video { get; internal set; }
        public string audio { get; internal set; }
        public string resolution { get; internal set; }
        public string bitratev { get; internal set; }
        public string bitratea { get; internal set; }

        public string destinationFolder { get; internal set; }

        public bool originalMove { get; internal set; }
        public bool originalDelete { get; internal set; }
        public string originalMoveFolder { get; internal set; }
    }
}
