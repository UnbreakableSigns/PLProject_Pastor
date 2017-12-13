using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            new Analyzer();
        }

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
            Color originalColor = Color.Black;
            
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
    }
}
