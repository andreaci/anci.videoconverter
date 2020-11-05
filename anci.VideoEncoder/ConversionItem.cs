using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anci.VideoEncoder
{

    public abstract class ConversionItem
    {
        public ConversionItem() { }

        public ConversionItem(String sourceFile, Constants constants, GeneratedOptions generatedOptions ) {

            String destfilename = sourceFile;

            FileInfo info = new FileInfo(destfilename);
            String newFolderName = String.IsNullOrEmpty(generatedOptions.destinationFolder) ? info.DirectoryName : generatedOptions.destinationFolder;
            destfilename = Path.Combine(info.DirectoryName, info.Name.Substring(0, info.Name.Length - info.Extension.Length) + generatedOptions.extension);

            if (sourceFile == destfilename || File.Exists(destfilename))
            {
                info = new FileInfo(destfilename);
                destfilename = Path.Combine(newFolderName, info.Name.Replace(info.Extension, "") + " " + DateTime.Now.ToString("yyyyMMddHHmmss") + info.Extension);
            }

            OriginalDelete = generatedOptions.originalDelete;
            OriginalMove = generatedOptions.originalMove;
            OriginalMoveFolder = generatedOptions.originalMoveFolder;

            this.SourceFile = sourceFile;
            this.DestinationFile = destfilename;


            this.CommandLine = constants.BasePattern.Replace("{SOURCE}", "\"" + SourceFile + "\"")
                                        .Replace("{DEST}", "\"" + DestinationFile + "\"")
                                        .Replace("{VIDEO}", generatedOptions.video)
                                        .Replace("{AUDIO}", generatedOptions.audio)
                                        .Replace("{RESOLUTION}", generatedOptions.resolution)
                                        .Replace("{SUBREMOVE}", generatedOptions.subremove)
                                        .Replace("{BITRATEV}", generatedOptions.bitratev)
                                        .Replace("{BITRATEA}", generatedOptions.bitratea);

        }


        public String SourceFile { get; set; }
        public String DestinationFile { get; set; }

        public String CommandLine { get; set; }



        public String OriginalMoveFolder { get; set; }
        public bool OriginalMove { get; set; }
        public bool OriginalDelete { get; set; }

        public bool? Success { get; set; } = null;


        public abstract bool Process();

        public delegate void dLoggedStep(object sender, String eventString, bool toFile);
        public static event dLoggedStep LoggedStep;

        protected static void RaiseStep(ConversionItem obj, string stringStep, bool toFile = true)
        {
            LoggedStep?.Invoke(obj, stringStep, toFile);
        }

        public void ArchiveFile()
        {
            RaiseStep(this, $"Moving file...");

            try
            {
                if (OriginalMove)
                {
                    if (!Directory.Exists(OriginalMoveFolder))
                        Directory.CreateDirectory(OriginalMoveFolder);

                    FileInfo info = new FileInfo(SourceFile);
                    File.Move(SourceFile, Path.Combine(OriginalMoveFolder, info.Name));

                    RaiseStep(this, $"Done...");
                }

                if (OriginalDelete)
                {
                    File.Delete(SourceFile);
                }
            }
            catch (Exception ex)
            {
                RaiseStep(this, $"Error moving/deleting file: {ex.Message}");
            }
        }

    }
}