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

        delegate void TestMethodEventHandler(object obj);
        delegate void RefreshTBEventHandler(object obj);

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(TestMethod));
            thread.Start(null);
            textBox2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            thread.Abort();
        }

        private void TestMethod(object threadid)
        {
            if (textBox3.InvokeRequired)
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    RefreshTb(i);
                }

                threadid = Thread.CurrentThread.ManagedThreadId.ToString();
                this.textBox3.Invoke(new TestMethodEventHandler(TestMethod), threadid);
            }
            else
            {
                textBox3.Text = threadid.ToString();
            }
        }

        private void RefreshTb(object obj)
        {
            if (textBox1.InvokeRequired)
            {
                this.textBox1.Invoke(new RefreshTBEventHandler(RefreshTb), obj);
            }
            else
            {
                this.textBox1.Text += obj.ToString() + "\r\n";
            }
        }
    }
}
