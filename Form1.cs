using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using Change_file_name.Properties;

namespace Change_file_name
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = txtPath.Text;

                DirectoryInfo fileDirectory = new DirectoryInfo(filePath + "\\");
                FileInfo[] Files = fileDirectory.GetFiles("*" + cmbExtension.Text);
                {
                    foreach (FileInfo file in Files)
                    {
                        string oldFileName = file.Name;
                        string newFileName = Regex.Replace(oldFileName, @"(FY\d+)?(V\d+)?[^\d]", "");

                        System.IO.File.Move(fileDirectory + oldFileName, fileDirectory + newFileName + cmbNewExtension.Text);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult ofd = folderBrowserDialog1.ShowDialog();

            if (ofd == DialogResult.OK)
            {
                string filePath = folderBrowserDialog1.SelectedPath;
                txtPath.Text = filePath;

                Settings.Default["directoryPath"] = txtPath.Text;
                Settings.Default.Save();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPath.Text = Settings.Default["directoryPath"].ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkStayOnTop.Checked;
        }
        }
    }


