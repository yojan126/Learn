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

namespace WinFormProgressBar
{
    public partial class Form1 : Form
    {
        private delegate void SetPosEventHandler(int iPos, string strInfo);

        public Form1()
        {
            InitializeComponent();
        }

        private void SetTextMessage(int iPos, string strInfo)
        {
            if (this.InvokeRequired)
            {
                SetPosEventHandler setPosEventHandler = new SetPosEventHandler(SetTextMessage);
                this.Invoke(setPosEventHandler, new object[] { iPos, strInfo });
            }
            else
            {
                this.label1.Text = iPos.ToString() + "/100";
                this.progressBar1.Value = Convert.ToInt32(iPos);
                this.textBox1.AppendText(strInfo);
                if (iPos == 100)
                {
                    this.button1.Enabled = true;
                }
            }
        }

        private void SleepT()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                SetTextMessage(i, i.ToString() + "\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.button1.Enabled = false;
            Thread thread = new Thread(SleepT);
            thread.Start();
        }
    }
}
