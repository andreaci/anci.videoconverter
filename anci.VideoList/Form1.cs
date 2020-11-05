using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anci.VideoList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String path = @"\\192.168.0.3\media\Kids movies";
            //String path = @"D:\Downloads";

            List<MediaFileInfo> list = new List<MediaFileInfo>(100);
            GetFilesList(path, list);

            foreach (MediaFileInfo finfo in list)
            finfo.ReadMetaData();


        }

        private void GetFilesList(string path, List<MediaFileInfo> list)
        {
            String[] dirs = null;
            try
            {
                dirs = Directory.GetDirectories(path);
            }
            catch { }

            if (dirs != null)
            {
                foreach (String dir in dirs)
                    GetFilesList(dir, list);
            }

            List<FileInfo> files = new DirectoryInfo(path).GetFilesByExtensions(".mkv",".avi",".mp4").ToList();
            foreach (FileInfo file in files)
                list.Add(new MediaFileInfo(file.FullName));
        }
    }
}
