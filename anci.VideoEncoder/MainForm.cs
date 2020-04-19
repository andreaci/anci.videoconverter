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
        }

        private void FileSelection_Click(object sender, EventArgs e) => OpenFileSelection();

        private void OpenFileSelection()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
                AddFileSelection(openFileDialog1.FileNames);
        }

        private void AddFileSelection(string[] fileNames)
        {
            foreach (String file in fileNames) {
                AddFileSelection(file);
            }
        }

        private void AddFileSelection(string source)
        {
            listView1.Items.Add(new ListViewItem(new String[] { source, "", "" }));
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
            foreach (ListViewItem itm in listView1.Items)
            {
                PrepareSingleFileName(itm);
            }
        }

        private string PrepareSingleFileName(ListViewItem itm)
        {
            bool same_path = constants.Destinations[listboxDestination.Text] == "SAME";
            String newFolderName = "TODO";
            String filename = itm.SubItems[0].Text;

            FileInfo info = new FileInfo(filename);
            filename = filename.Substring(0, filename.Length - info.Extension.Length) + GetComboValue(constants.Extensions, listboxExtension.Text);

            if (!same_path)
                filename = filename.Replace(info.DirectoryName, newFolderName);

            if (filename == itm.SubItems[0].Text)
                itm.SubItems[2].Text = "WARNING! SAME FILE NAME!";

            itm.SubItems[1].Text = filename;

            return filename;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //process_console.Show();

            String pattern = constants.BasePattern;

            foreach (ListViewItem itm in listView1.Items)
            {
                String source = "\"" + itm.SubItems[0].Text + "\"";
                String dest = "\"" + (itm.SubItems[1].Text == "" ? PrepareSingleFileName(itm) : itm.SubItems[1].Text) + "\"";

                String video = GetComboValue(constants.VideoFormats, listboxVideo.Text, "-c:v ");
                String audio = GetComboValue(constants.AudioFormats, listboxAudio.Text, "-c:a ");
                String resolution = GetComboValue(constants.Resolutions, listResolution.Text, "-s ");
                String bitratev = Constants.BitRateVString(GetComboValue(constants.BitRatesVideo, listboxBitrateV.Text));
                String bitratea = Constants.BitRateAString(GetComboValue(constants.BitRatesAudio, listboxBitrateA.Text));

                String cmdline = pattern.Replace("{SOURCE}", source)
                                        .Replace("{DEST}", dest)
                                        .Replace("{VIDEO}", video)
                                        .Replace("{AUDIO}", audio)
                                        .Replace("{RESOLUTION}", resolution)
                                        .Replace("{BITRATEV}", bitratev)
                                        .Replace("{BITRATEA}", bitratea);

                ProcessFile(itm, cmdline);
            }
        }

        private string GetComboValue(Dictionary<string, string> videoFormats, string text, String prefix = "")
        {
            videoFormats.TryGetValue(text, out string value);
            return String.IsNullOrEmpty(value) ? prefix + text : value;
        }

        private void ProcessFile(ListViewItem itm, string cmdline)
        {
            itm.SubItems[2].Text = "Starting...";
            Application.DoEvents();

            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = constants.ffmpeg_exe,
                    Arguments = cmdline,
                    //UseShellExecute = false,
                    //RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    CreateNoWindow = true
                };

                Process temp = Process.Start(info);

                process_console.SetProcess(temp);

                temp.WaitForExit();
                itm.SubItems[2].Text = "Done...";
            }
            catch (Exception ex)
            {
                itm.SubItems[2].Text = $"Error: {ex.Message}";
            }

            Application.DoEvents();
        }
    }
}
