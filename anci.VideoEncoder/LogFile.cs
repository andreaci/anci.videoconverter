using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anci.VideoEncoder
{
    public class LogFile
    {
        public StreamWriter writer;
        private String filename;

        public void Start()
        {
            Stop();

            filename = Path.Combine(Application.StartupPath, "log");
            Directory.CreateDirectory(filename);

            filename = Path.Combine(filename, DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            writer = new StreamWriter(filename, false);
        }

        public void Stop(bool popupFile = false)
        {
            if (writer != null)
            {
                if (popupFile)
                    Process.Start(filename);

                writer.Close();
                writer.Dispose();
                writer = null;
            }
        }

        public void Log(String stringToLog)
        {
            writer.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss") + " - " + stringToLog);
        }

    }
}