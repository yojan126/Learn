using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        delegate void DoEventHandler(object obj);

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(TestMethod));
            thread.Start("A");
            textBox2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        private void TestMethod(object threadid)
        {
            if (textBox3.InvokeRequired)
            {
                Thread.Sleep(2000);
                threadid = Thread.CurrentThread.ManagedThreadId.ToString();
                this.textBox3.Invoke(new DoEventHandler(TestMethod), threadid);
            }
            else
            {
                textBox3.Text = threadid.ToString();
            }
        }
    }
}
