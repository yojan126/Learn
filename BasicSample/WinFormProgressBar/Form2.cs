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
    public partial class Form2 : Form
    {
        private Form3 myProcessBar = null;
        private delegate bool IncreaseEventHandler(int iValue, string strInfo);
        private IncreaseEventHandler myincrease = null;
        private int iMax = 100;

        public Form2()
        {
            InitializeComponent();
        }

        private void ThreadFun()
        {
            MethodInvoker mi = new MethodInvoker(ShowProcessBar);
            this.BeginInvoke(mi);
            Thread.Sleep(100);
            object objReturn = null;
            for (int i = 1; i <= 100; i++)
            {
                objReturn = this.Invoke(this.myincrease, new object[] { 1, i.ToString() + "\r\n" });
                Thread.Sleep(50);
            }
        }

        private void ShowProcessBar()
        {
            myProcessBar = new Form3(iMax);
            myincrease = new IncreaseEventHandler(myProcessBar.Increase);
            myProcessBar.ShowDialog();
            myProcessBar = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(ThreadFun);
            thread.Start();
        }
    }
}
