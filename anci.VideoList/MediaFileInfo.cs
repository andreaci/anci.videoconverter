using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anci.VideoList
{
    class MediaFileInfo
    {
        private string file;

        public MediaFileInfo(string file)
        {
            this.file = file;
        }

        internal void ReadMetaData()
        {
            var inputFile = new MediaFile { Filename = file };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }

            Console.Write(file + "\t" + inputFile.Metadata.Duration + "\t");
            Console.Write(inputFile.Metadata.VideoData.Format + "\t" + inputFile.Metadata.VideoData.FrameSize + "\t" + inputFile.Metadata.VideoData.Fps + " fps");
            
            foreach (StreamMetadata temp in inputFile.Metadata.AudioStreams)
            {
                Console.Write($"\t{temp.Language}"); //in {temp.Format}@{temp.BitRateKbs} kb/s");
            }

            Console.WriteLine("");
        }
    }
}
