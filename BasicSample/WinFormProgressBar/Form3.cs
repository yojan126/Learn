using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormProgressBar
{
    public partial class Form3 : Form
    {
        public Form3(int iMax)
        {
            InitializeComponent();
            this.progressBar1.Maximum = iMax;
        }

        public bool Increase(int iValue, string strInfo)
        {
            if (iValue > 0)
            {
                if (progressBar1.Value + iValue < progressBar1.Maximum)
                {
                    progressBar1.Value += iValue;
                    this.textBox1.AppendText(strInfo);
                    Application.DoEvents();
                    progressBar1.Update();
                    progressBar1.Refresh();
                    textBox1.Update();
                    textBox1.Refresh();
                    return true;
                }
            }
            else
            {
                progressBar1.Value = progressBar1.Maximum;
                this.textBox1.AppendText(strInfo);
                return false;
            }
            return false;
        }
    }
}
