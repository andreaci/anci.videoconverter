using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anci.VideoEncoder
{
    public partial class MainForm : Form
    {
        private readonly LogFile logFile = new LogFile();
        private readonly ConversionList conversionList = new ConversionList();

        private readonly ConsoleForm process_console = new ConsoleForm();
        private readonly Constants constants = null;
        public MainForm()
        {
            InitializeComponent();

            constants = Constants.Init();

            listboxAudio.Items.AddRange(constants.AudioFormats.Keys.ToArray());
            listboxVideo.Items.AddRange(constants.VideoFormats.Keys.ToArray());
            listboxDestination.Items.AddRange(constants.Destinations.Keys.ToArray());
            listboxExtension.Items.AddRange(constants.Extensions.Keys.ToArray());
            listResolution.Items.AddRange(constants.Resolutions.Keys.ToArray());
            listboxBitrateV.Items.AddRange(constants.BitRatesVideo.Keys.ToArray());
            listboxBitrateA.Items.AddRange(constants.BitRatesAudio.Keys.ToArray());

            listResolution.SelectedIndex = 0;
            listboxAudio.SelectedIndex = 0;
            listboxVideo.SelectedIndex = 0;
            listboxExtension.SelectedIndex = 0;
            listboxDestination.SelectedIndex = 0;
            listboxBitrateV.SelectedIndex = 0;
            listboxBitrateA.SelectedIndex = 0;

            ConversionItem.LoggedStep += ConversionItem_LoggedStep;
            conversionList.ListChanged += ConversionList_listChanged;
            conversionList.Clear();
        }

        private void ConversionList_listChanged(object sender)
        {
            ConversionList list = (ConversionList)sender;
            labelTotalJobs.Text =
                $"Total Jobs: {list.Items.Count()}\n" +
                $"Remaining jobs: {list.Items.Count(x => x.Success == null)}\n" +
                $"Success jobs: {list.Items.Count(x => x.Success == true)}\n" +
                $"Failed jobs: {list.Items.Count(x => x.Success == false)}";

            button2.Enabled = list.PendingOrFailed.Any();

            Application.DoEvents();
        }

        private String log_string = "";
        private void ConversionItem_LoggedStep(object sender, string eventString, bool toFile = true)
        {
            String sourceFile = sender != null ? ((ConversionItem)sender).SourceFile : "";
            log_string = sourceFile + "\n" + eventString;

            if(toFile)
                logFile.Log(log_string);

            labelCurrentStep.InvokeIfRequired((labelCurrentStep) =>
            {
                Console.WriteLine("update ui");
                labelCurrentStep.Text = log_string;
                //Application.DoEvents();
            });
        }

        private void FileSelection_Click(object sender, EventArgs e) => OpenFileSelection();

        private void OpenFileSelection()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                AddFileSelection(openFileDialog1.FileNames);
        }

        private void AddFileSelection(string[] fileNames)
        {
            foreach (String file in fileNames)
            {
                AddFileSelection(file);
            }
        }

        private void AddFileSelection(string source)
        {
            if (File.Exists(source))
            {
                listTemporary.Items.Add(new ListViewItem(source));
            }
            else if (Directory.Exists(source))
            {
                // path is a directory.
                Array.ForEach(Directory.GetDirectories(source), x => AddFileSelection(x));
                Array.ForEach(Directory.GetFiles(source), x => AddFileSelection(x));
            }
            else
            {
                // path doesn't exist.
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            AddFileSelection(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool same_path = constants.Destinations[listboxDestination.Text] == "SAME";
            string DestFolder = same_path ? "" : textDestinationDir.Text;

            if (!same_path && String.IsNullOrEmpty(DestFolder))
            {
                MessageBox.Show("Invalid destination folder");
                return;
            }

            foreach (ListViewItem itm in listTemporary.Items)
            {
#if EXE_CONVERSION
                conversionList.Add(new ConversionItemExternal(itm.SubItems[0].Text, constants, GetConversionOptions(DestFolder), constants.ffmpeg_exe));            
#else
                conversionList.Add(new ConversionItemInternal(itm.SubItems[0].Text, constants, GetConversionOptions(DestFolder)));
#endif
            }


            listTemporary.Items.Clear();

        }

        GeneratedOptions GetConversionOptions(string DestFolder)
        {
            return new GeneratedOptions
            {
                subremove = checkRemoveSubs.Checked ? " -sn " : " -c:s copy ",
                video = GetComboValue(constants.VideoFormats, listboxVideo.Text, "-c:v "),
                audio = GetComboValue(constants.AudioFormats, listboxAudio.Text, "-c:a "),
                resolution = GetComboValue(constants.Resolutions, listResolution.Text, "-s "),
                bitratev = Constants.BitRateVString(GetComboValue(constants.BitRatesVideo, listboxBitrateV.Text)),
                bitratea = Constants.BitRateAString(GetComboValue(constants.BitRatesAudio, listboxBitrateA.Text)),
                extension = GetComboValue(constants.Extensions, listboxExtension.Text),
                destinationFolder = DestFolder,

                originalMove = radioButton1.Checked,
                originalDelete = radioButton3.Checked,
                originalMoveFolder = textMoveFolder.Text
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            logFile.Start();

            foreach (ConversionItem conversionItem in conversionList.PendingOrFailed)
            {
                if (conversionItem.Process())
                    conversionItem.ArchiveFile();

                conversionList.Updated();
            }

            logFile.Stop(checkOpenLog.Checked);

            if (checkShutdown.Checked)
            {
                Process.Start("shutdown", "/s /t 0");
                Application.Exit();
            }
        }

        private string GetComboValue(Dictionary<string, string> videoFormats, string text, String prefix = "")
        {
            if (videoFormats.TryGetValue(text, out string value))
                return value;
            else
                return prefix + text;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listTemporary.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textMoveFolder.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textMoveFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void concatenateButton_click(object sender, EventArgs e)
        {
            if (listTemporary.Items.Count == 0)
                return;

            String BetterExtension = GetComboValue(constants.Extensions, listboxExtension.Text);
            String temp = Path.GetTempFileName();
            using (StreamWriter tempFile = new StreamWriter(temp, false))
            {
                foreach (ListViewItem itm in listTemporary.Items)
                {
                    String source = "file '" + itm.SubItems[0].Text + "'";
                    tempFile.WriteLine(source);

                    FileInfo info = new FileInfo(itm.SubItems[0].Text);
                    BetterExtension = GetBetterExtension(BetterExtension, info.Extension);
                }
            }

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = BetterExtension + "|*" + BetterExtension;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String destFile = saveFileDialog1.FileName;

                ConversionItem itm = new ConversionItemExternal(constants.ffmpeg_exe) { DestinationFile = destFile, SourceFile = temp, CommandLine = constants.ConcatPattern };
                itm.Process();
            }
        }

        private string GetBetterExtension(string betterExtension, string extension)
        {
            // TODO do something better here

            if (extension == ".mkv")
                return extension;

            return betterExtension;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textDestinationDir.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textDestinationDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void checkOpenLog_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            conversionList.Clear();
        }
    }
}