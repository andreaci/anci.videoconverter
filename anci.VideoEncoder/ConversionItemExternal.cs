using System;
using System.Diagnostics;
using System.IO;

namespace anci.VideoEncoder
{
    public class ConversionItemExternal : ConversionItem
    {
        private string ffmpeg_exe { get; set; }

        public ConversionItemExternal(String ffmpeg_exe) :base() {
            this.ffmpeg_exe = ffmpeg_exe;
        }

        public ConversionItemExternal(string sourceFile, Constants constants, GeneratedOptions generatedOptions, String ffmpeg_exe)
            : base(sourceFile, constants, generatedOptions)
        {
            this.ffmpeg_exe = ffmpeg_exe;
                  }


        public override bool Process()
        {
            RaiseStep(this, CommandLine);
            RaiseStep(this, "Starting...");

            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = ffmpeg_exe,
                    Arguments = CommandLine,
                    //UseShellExecute = false,
                    //RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    CreateNoWindow = true
                };

                Stopwatch watch = new Stopwatch();
                watch.Start();
                Process temp = System.Diagnostics.Process.Start(info);

                temp.WaitForExit();
                watch.Stop();
                RaiseStep(this, "Done...");

                return (bool)(Success = IsCompletedSuccessfully(watch));
            }
            catch (Exception ex)
            {
                RaiseStep(this, $"Error: {ex.Message}");
                Success = false;
                return false;
            }

        }

        private bool IsCompletedSuccessfully(Stopwatch watch)
        {
            //TODO analyze output

            if (watch.ElapsedMilliseconds < 10 * 1000)
                return false;

            FileInfo info = new FileInfo(DestinationFile);

            if (!info.Exists)
                return false;

            if (info.Length < 5 * 1024 * 1024)
                return false;

            return true;
        }


    }
}