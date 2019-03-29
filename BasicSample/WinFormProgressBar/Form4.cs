using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormProgressBar
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public Dictionary<int, string> Dic = new Dictionary<int, string>();

        public void CreateData()
        {
            for (int i = 0; i < 99999; i++)
            {
                Thread.Sleep(10);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
        }
    }
}
