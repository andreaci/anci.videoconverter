using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;

namespace anci.VideoEncoder
{
    internal class ConversionItemInternal : ConversionItem
    {

        public ConversionItemInternal(string sourceFile, Constants constants, GeneratedOptions generatedOptions)
                     : base(sourceFile, constants, generatedOptions)
        {

        }


        public override bool Process()
        {
            RaiseStep(this, "Starting...");

            var inputFile = new MediaFile { Filename = SourceFile};
            var outputFile = new MediaFile { Filename = DestinationFile };

            using (var engine = new Engine())
            {
                engine.ConvertProgressEvent += Engine_ConvertProgressEvent; 
                engine.ConversionCompleteEvent += Engine_ConversionCompleteEvent ;
                engine.CustomCommand(this.CommandLine);
                engine.Convert(inputFile, outputFile);
            }

            return true;
        }

        private void Engine_ConversionCompleteEvent(object sender, ConversionCompleteEventArgs e)
        {
            //Console.WriteLine("\n------------\nConversion complete!\n------------");
            //Console.WriteLine("Bitrate: {0}", e.Bitrate);
            //Console.WriteLine("Fps: {0}", e.Fps);
            //Console.WriteLine("Frame: {0}", e.Frame);
            //Console.WriteLine("ProcessedDuration: {0}", e.ProcessedDuration);
            //Console.WriteLine("SizeKb: {0}", e.SizeKb);
            //Console.WriteLine("TotalDuration: {0}\n", e.TotalDuration);
        }

        private void Engine_ConvertProgressEvent(object sender, ConvertProgressEventArgs e)
        {
            Console.WriteLine($"Bitrate: {e.Bitrate}, Fps: {e.Fps}, ProcessedDuration: {e.ProcessedDuration}, SizeKb: {e.SizeKb}");
            RaiseStep(this, $"Bitrate: {e.Bitrate}, Fps: {e.Fps}, ProcessedDuration: {e.ProcessedDuration}, SizeKb: {e.SizeKb}", false);
        }

        /*
         var inputFile = new MediaFile {Filename = @"C:\Path\To_Video.flv"};

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }

            Console.WriteLine(inputFile.Metadata.Duration);
         */
    }
}