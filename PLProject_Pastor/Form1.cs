using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLProject_Pastor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Interpreter();
        }


        //set color of certain keywords and tokens in languages
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string keywords = @"\b(write|read|sum|len|true|false)\b";//hello
            string variabs = @"(\$.+?\s|#.+?\s|@.+?\s|~.+?\s)";     
            string comments = @"(\--\--.+?$|\--*.+?\--)";
            string str = @"(''|'.+?')";
            
            MatchCollection keymatch = Regex.Matches(richTextBox1.Text, keywords);
            MatchCollection commsmatch = Regex.Matches(richTextBox1.Text, comments);
            MatchCollection strmatch = Regex.Matches(richTextBox1.Text, str);
            MatchCollection varmatch = Regex.Matches(richTextBox1.Text, variabs);

            int originalIndex = richTextBox1.SelectionStart;
            int originalLength = richTextBox1.SelectionLength;
            Color originalColor = Color.White;
            
            // removes any previous highlighting 
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length;
            richTextBox1.SelectionColor = originalColor;

            foreach (Match m in keymatch)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Blue;
            }

            foreach (Match m in commsmatch)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Green;
            }
            foreach (Match m in strmatch)
            {

                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Red;
            }
            foreach (Match m in varmatch)
            {

                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.SeaGreen;
            }

            richTextBox1.SelectionStart = originalIndex;
            richTextBox1.SelectionLength = originalLength;
            richTextBox1.SelectionColor = originalColor;

            // giving back the focus
            richTextBox1.Focus();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = Directory.GetParent(Directory.GetParent((Directory.GetCurrentDirectory())).FullName).FullName + "\\sample";
            
            dialog.Title = "Select a text file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
                this.Text = string.Format("{0}", System.IO.Path.GetFileName(dialog.FileName));
                richTextBox1.Text = File.ReadAllText(dialog.FileName);
            }

        }
        
        //create new project; clears the richtextbox
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != String.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to create a new project?", "Confirm Create New Project", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    this.Text = "New Project";
                    richTextBox1.Text = "";
                }
            }
            else {
                this.Text = "New Project";
                richTextBox1.Text = "";
            }
        }

        //save to file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = Directory.GetParent(Directory.GetParent((Directory.GetCurrentDirectory())).FullName).FullName + "\\sample";

            dialog.Title = "Save as";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(dialog.OpenFile());
                writer.Write(richTextBox1.Text);
                writer.Dispose();
                writer.Close();
                this.Text = string.Format("{0}", System.IO.Path.GetFileName(dialog.FileName));
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Mendoza, Pastor");
        }


        //exit program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
