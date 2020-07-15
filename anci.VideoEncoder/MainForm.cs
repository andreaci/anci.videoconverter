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
            foreach (String file in fileNames)
            {
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
            String filename = itm.SubItems[0].Text;

            FileInfo info = new FileInfo(filename);
            String newFolderName = same_path ? info.DirectoryName : "TODO_TEXT_BOX_FOLDERNAME";
            filename = Path.Combine(info.DirectoryName, info.Name.Substring(0, info.Name.Length - info.Extension.Length) + GetComboValue(constants.Extensions, listboxExtension.Text));
            
            if (filename == itm.SubItems[0].Text || File.Exists(filename))
            {
                info = new FileInfo(filename);
                filename = Path.Combine(newFolderName, info.Name.Replace(info.Extension, "") + " " + DateTime.Now.ToString("yyyyMMddHHmmss") + info.Extension);

                itm.SubItems[2].Text = "WARNING! SAME FILE NAME!";
            }

            itm.SubItems[1].Text = filename;

            return filename;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //process_console.Show();

            String pattern = constants.BasePattern;

            foreach (ListViewItem itm in listView1.Items)
            {
                String source = itm.SubItems[0].Text;
                String dest = itm.SubItems[1].Text == "" ? PrepareSingleFileName(itm) : itm.SubItems[1].Text;

                String subremove = checkRemoveSubs.Checked ? " -sn " : "";
                String video = GetComboValue(constants.VideoFormats, listboxVideo.Text, "-c:v ");
                String audio = GetComboValue(constants.AudioFormats, listboxAudio.Text, "-c:a ");
                String resolution = GetComboValue(constants.Resolutions, listResolution.Text, "-s ");
                String bitratev = Constants.BitRateVString(GetComboValue(constants.BitRatesVideo, listboxBitrateV.Text));
                String bitratea = Constants.BitRateAString(GetComboValue(constants.BitRatesAudio, listboxBitrateA.Text));

                String cmdline = pattern.Replace("{SOURCE}", "\"" + source + "\"")
                                        .Replace("{DEST}", "\"" + dest + "\"")
                                        .Replace("{VIDEO}", video)
                                        .Replace("{AUDIO}", audio)
                                        .Replace("{RESOLUTION}", resolution)
                                        .Replace("{SUBREMOVE}", subremove)
                                        .Replace("{BITRATEV}", bitratev)
                                        .Replace("{BITRATEA}", bitratea);

                if (ProcessFile(itm, cmdline))
                {
                    if (radioButton1.Checked)
                        ArchiveFile(itm);

                    try
                    {
                        if (radioButton3.Checked)
                            File.Delete(itm.SubItems[0].Text);
                    }
                    catch (Exception ex)
                    {
                        itm.SubItems[2].Text = $"Error deleting file: {ex.Message}";
                    }
                }
            }
        }

        private void ArchiveFile(ListViewItem itm)
        {
            itm.SubItems[2].Text = $"Moving file...";
            Application.DoEvents();
            try
            {
                if (!Directory.Exists(textMoveFolder.Text))
                    Directory.CreateDirectory(textMoveFolder.Text);

                FileInfo info = new FileInfo(itm.SubItems[0].Text);
                File.Move(itm.SubItems[0].Text, Path.Combine(textMoveFolder.Text, info.Name));

                itm.SubItems[2].Text = $"Done...";
            }
            catch (Exception ex)
            {
                itm.SubItems[2].Text = $"Error moving file: {ex.Message}";
            }
        }

        private string GetComboValue(Dictionary<string, string> videoFormats, string text, String prefix = "")
        {
            if (videoFormats.TryGetValue(text, out string value))
                return value;
            else
                return prefix + text;
        }

        private bool ProcessFile(ListViewItem itm, string cmdline)
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

                return true;
            }
            catch (Exception ex)
            {
                itm.SubItems[2].Text = $"Error: {ex.Message}";

                return false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textMoveFolder.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textMoveFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
                return;

            String BetterExtension = GetComboValue(constants.Extensions, listboxExtension.Text);
            String temp = Path.GetTempFileName();
            using (StreamWriter tempFile = new StreamWriter(temp, false))
            {
                foreach (ListViewItem itm in listView1.Items)
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
                
                ProcessFile(listView1.Items[0], constants.ConcatPattern.Replace("{TEMPFILE}", temp).Replace("{DEST}", destFile));
            }
        }

        private string GetBetterExtension(string betterExtension, string extension)
        {
            // TODO do something better here

            if (extension == ".mkv") 
                return extension;

            return betterExtension;
        }
    }
}