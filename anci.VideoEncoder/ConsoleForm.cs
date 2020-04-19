using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anci.VideoEncoder
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
        }

        internal void SetProcess(Process temp)
        {
            //while (!temp.StandardOutput.EndOfStream)
            //{
            //    string line = temp.StandardOutput.ReadLine();
            //    listBox1.Items.Add(line);
            //
            //    Application.DoEvents();
            //}

        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
