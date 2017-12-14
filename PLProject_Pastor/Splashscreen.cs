using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLProject_Pastor
{
    public partial class Splashscreen : Form
    {
        Timer t1 = new Timer();


        public Splashscreen()
        {
            InitializeComponent();
        }

        private void Splashscreen_Load(object sender, EventArgs e)
        {
            t1.Tick += t1_Tick;
            t1.Interval = 5000; //5000 ms = 5 seconds
            t1.Start();
        }

        void t1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            t1.Stop(); //it should be stopped after hiding the form
            new Form1().Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
