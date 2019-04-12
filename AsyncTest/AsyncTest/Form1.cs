using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;

            long length = AccessWeb();
            button1.Enabled = true;

            this.textBox1.Text += string.Format("\n 回复的字节长度为： {0}.\r\n", length);
            this.textBox2.Text = Thread.CurrentThread.ManagedThreadId.ToString();

        }

        private long AccessWeb()
        {
            MemoryStream contect = new MemoryStream();

            // 对MSDN发起一个web请求
            HttpWebRequest webRequest = WebRequest.Create("http://msdn.microsoft.com/zh-cn/") as HttpWebRequest;

            if (webRequest != null)
            {
                // 返回回复结果
                using (WebResponse response = webRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        responseStream.CopyTo(contect);
                    }
                }
            }

            textBox3.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            return contect.Length;
        }
    }
}
