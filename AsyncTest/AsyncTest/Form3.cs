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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            long length = await AccessWebAsync();

            // 这里可以做一些不依赖回复的操作      此处描述错误   await之后的所有操作都需等待前一步完成后才能进行
            OtherWork();

            this.textBox1.Text += string.Format("\n 回复的字节长度为： {0}.\r\n", length);
            textBox2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        // 使用C# 5.0中提供的async 和await关键字来定义异步方法
        // 从代码中可以看出C#5.0 中定义异步方法就像定义同步方法一样简单。
        // 使用async 和await定义异步方法不会创建新线程,
        // 它运行在现有线程上执行多个任务.
        // 此时不知道大家有没有一个疑问的？在现有线程上(即UI线程上)运行一个耗时的操作时，
        // 为什么不会堵塞UI线程的呢？
        // 这个问题的答案就是 当编译器看到await关键字时，线程会  等待
        private async Task<long> AccessWebAsync()
        {
            MemoryStream content = new MemoryStream();

            // 对MSDN发起一个web请求
            HttpWebRequest webRequest = WebRequest.Create("http://msdn.microsoft.com/zh-cn/") as HttpWebRequest;

            if (webRequest != null)
            {
                using (WebResponse response = await webRequest.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }

            textBox3.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            return content.Length;
        }

        private void OtherWork()
        {
            this.textBox1.Text += "\r\n等待服务器回复中...........";
        }
    }
}
