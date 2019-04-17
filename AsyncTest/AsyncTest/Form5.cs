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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        Thread t1;
        Thread t2;

        private void button1_Click(object sender, EventArgs e)
        {
            t1 = new Thread(T1Data);
            t2 = new Thread(T2Data);

            t1.Start();
            //t2.Start();
        }

        private delegate void t1EventHandler(int i);
        private delegate void t2EventHandler(int i);

        private void T1Data()
        {
            for (int i = 0; i < 10; i++)
            {
                T1Show(i);
                Thread.Sleep(100);
            }
        }

        private void T1Show(int i)
        {
            if (textBox1.InvokeRequired)
            {
                t1EventHandler t1EventHandler=new t1EventHandler(T2Show);
                t1EventHandler += T1Show;
                textBox1.Invoke(t1EventHandler, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                textBox1.Text += i.ToString() + "  " + Thread.CurrentThread.ManagedThreadId.ToString() + "\r\n";
            }
        }

        private void T2Data()
        {
            for (int i = 0; i < 10; i++)
            {
                T2Show(i);
                Thread.Sleep(100);
            }
        }

        private void T2Show(int i)
        {
            if (textBox2.InvokeRequired)
            {
                textBox2.BeginInvoke(new t2EventHandler(T2Show), Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                textBox2.Text += i.ToString() + "  " + Thread.CurrentThread.ManagedThreadId.ToString() + "\r\n";
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            t1.Abort();
            t2.Abort();
        }
    }
}
