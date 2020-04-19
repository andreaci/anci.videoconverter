﻿namespace anci.VideoEncoder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listboxVideo = new System.Windows.Forms.ComboBox();
            this.listboxAudio = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listboxExtension = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.listboxDestination = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.listResolution = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.listboxBitrateV = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.listboxBitrateA = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "1. Select source files";
            // 
            // label2
            // 
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Location = new System.Drawing.Point(34, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(354, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Drag the files to convert or click HERE to browse for files";
            this.label2.Click += new System.EventHandler(this.FileSelection_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(37, 327);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1058, 214);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Source file";
            this.columnHeader1.Width = 501;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Destination file";
            this.columnHeader2.Width = 286;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            this.columnHeader3.Width = 235;
            // 
            // label3
            // 
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(34, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(471, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select options for conversion. You can also write your own codec identifier.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 32);
            this.label4.TabIndex = 5;
            this.label4.Text = "2. Check options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Video format";
            // 
            // listboxVideo
            // 
            this.listboxVideo.FormattingEnabled = true;
            this.listboxVideo.Location = new System.Drawing.Point(37, 213);
            this.listboxVideo.Name = "listboxVideo";
            this.listboxVideo.Size = new System.Drawing.Size(174, 25);
            this.listboxVideo.TabIndex = 8;
            // 
            // listboxAudio
            // 
            this.listboxAudio.FormattingEnabled = true;
            this.listboxAudio.Location = new System.Drawing.Point(255, 213);
            this.listboxAudio.Name = "listboxAudio";
            this.listboxAudio.Size = new System.Drawing.Size(174, 25);
            this.listboxAudio.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Audio format";
            // 
            // listboxExtension
            // 
            this.listboxExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listboxExtension.FormattingEnabled = true;
            this.listboxExtension.Location = new System.Drawing.Point(255, 270);
            this.listboxExtension.Name = "listboxExtension";
            this.listboxExtension.Size = new System.Drawing.Size(174, 25);
            this.listboxExtension.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(252, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Extension";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(966, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 17);
            this.label10.TabIndex = 16;
            this.label10.Text = "Destination directory";
            this.label10.Visible = false;
            // 
            // listboxDestination
            // 
            this.listboxDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listboxDestination.Enabled = false;
            this.listboxDestination.FormattingEnabled = true;
            this.listboxDestination.Location = new System.Drawing.Point(1101, 6);
            this.listboxDestination.Name = "listboxDestination";
            this.listboxDestination.Size = new System.Drawing.Size(20, 25);
            this.listboxDestination.TabIndex = 15;
            this.listboxDestination.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(980, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 42);
            this.button1.TabIndex = 17;
            this.button1.Text = "Prepare jobs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(923, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(172, 32);
            this.label11.TabIndex = 18;
            this.label11.Text = "3. Prepare jobs";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(892, 547);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 42);
            this.button2.TabIndex = 19;
            this.button2.Text = "Start conversion!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listResolution
            // 
            this.listResolution.FormattingEnabled = true;
            this.listResolution.Location = new System.Drawing.Point(37, 270);
            this.listResolution.Name = "listResolution";
            this.listResolution.Size = new System.Drawing.Size(174, 25);
            this.listResolution.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 250);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 20;
            this.label12.Text = "Resolution";
            // 
            // listboxBitrateV
            // 
            this.listboxBitrateV.FormattingEnabled = true;
            this.listboxBitrateV.Location = new System.Drawing.Point(471, 213);
            this.listboxBitrateV.Name = "listboxBitrateV";
            this.listboxBitrateV.Size = new System.Drawing.Size(174, 25);
            this.listboxBitrateV.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(468, 193);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 17);
            this.label13.TabIndex = 22;
            this.label13.Text = "Video bitrate limit";
            // 
            // listboxBitrateA
            // 
            this.listboxBitrateA.FormattingEnabled = true;
            this.listboxBitrateA.Location = new System.Drawing.Point(471, 270);
            this.listboxBitrateA.Name = "listboxBitrateA";
            this.listboxBitrateA.Size = new System.Drawing.Size(174, 25);
            this.listboxBitrateA.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(468, 250);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 17);
            this.label14.TabIndex = 24;
            this.label14.Text = "Audio bitrate limit";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1125, 601);
            this.Controls.Add(this.listboxBitrateA);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.listboxBitrateV);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.listResolution);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.listboxDestination);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.listboxExtension);
            this.Controls.Add(this.listboxAudio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listboxVideo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Video conversion";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox listboxVideo;
        private System.Windows.Forms.ComboBox listboxAudio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox listboxExtension;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox listboxDestination;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox listResolution;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox listboxBitrateV;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox listboxBitrateA;
        private System.Windows.Forms.Label label14;
    }
}
