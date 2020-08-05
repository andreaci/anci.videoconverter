using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anci.VideoEncoder
{

    public class ConversionList : IEnumerable
    {
        public delegate void dListChanged(object sender);
        public event dListChanged ListChanged;

        public ObservableCollection<ConversionItem> Items = new ObservableCollection<ConversionItem>();

        public IEnumerable<ConversionItem> PendingOrFailed
        {
            get
            {
                return Items.Where(x => x.Success != true);
            }
        }

        public ConversionList() {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ListChanged?.Invoke(this);
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        internal void Add(string source, string destination, string parameters)
        {
            Items.Add(new ConversionItem()
            {
                SourceFile = source,
                DestinationFile = destination,
                CommandLine = parameters
            });
        }

        internal void Add(ConversionItem conversionItem)
        {
            Items.Add(conversionItem);
        }

        internal void Clear()
        {
            Items.Clear();
        }

        internal void Updated()
        {
            ListChanged?.Invoke(this);
        }
    }

    public class ConversionItem
    {
        public ConversionItem()
        {
        }

        public ConversionItem(string sourceFile, Constants constants, dynamic generatedOptions)
        {
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

            String cmdline = constants.BasePattern.Replace("{SOURCE}", "\"" + sourceFile + "\"")
                                                  .Replace("{DEST}", "\"" + destfilename + "\"")
                                                  .Replace("{VIDEO}", generatedOptions.video)
                                                  .Replace("{AUDIO}", generatedOptions.audio)
                                                  .Replace("{RESOLUTION}", generatedOptions.resolution)
                                                  .Replace("{SUBREMOVE}", generatedOptions.subremove)
                                                  .Replace("{BITRATEV}", generatedOptions.bitratev)
                                                  .Replace("{BITRATEA}", generatedOptions.bitratea);

            this.SourceFile = sourceFile;
            this.DestinationFile = destfilename;
            this.CommandLine = cmdline;
        }

        public String SourceFile { get; set; }
        public String DestinationFile { get; set; }

        public String CommandLine { get; set; }



        public String OriginalMoveFolder { get; set; }
        public bool OriginalMove { get; set; }
        public bool OriginalDelete { get; set; }

        public bool? Success { get; set; } = null;


        public bool Process(String ffmpeg_exe)
        {
            //String cmdLine = CommandLine.Replace("{SOURCE}", "\"" + SourceFile + "\"")
            //                        .Replace("{DEST}", "\"" + DestinationFile + "\"");

            return ProcessFile(ffmpeg_exe, CommandLine);
        }

        public bool ProcessFile(string ffmpeg_exe, string cmdline)
        {
            RaiseStep(this, cmdline);
            RaiseStep(this, "Starting...");

            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = ffmpeg_exe,
                    Arguments = cmdline,
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

        public delegate void dLoggedStep(object sender, String eventString);
        public static event dLoggedStep LoggedStep;

        private static void RaiseStep(ConversionItem obj, string stringStep)
        {
            LoggedStep?.Invoke(obj, stringStep);
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