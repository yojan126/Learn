using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aiursoft.HSharp.Methods;
using SufeiUtil;

namespace B2CTest
{
    public partial class Form1 : Form
    {
        DataTable dtCache = new DataTable();
        DataTable dtShow = new DataTable();
        Thread t;
        int tmpRowIndex = 0;
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            // http://bbs.nga.cn/read.php?tid=17191970&_ff=-7
            //HttpHelper httpHelper = new HttpHelper();
            //HttpItem httpItem = new HttpItem();
            //httpItem.URL = "http://bbs.nga.cn/read.php?tid=17181370&_ff=-7";//"http://bbs.nga.cn/thread.php?fid=-7&rand=330";
            //httpItem.Encoding = Encoding.GetEncoding("GB18030");
            //httpItem.Cookie = "lastvisit = 1557365341; guestJs = 1557365340; UM_distinctid = 16a9a3540f0ac - 0d04f0131d3681 - 387e144f - 1fa400 - 16a9a3540f1cf3; CNZZDATA30043604 = cnzz_eid % 3D1904332819 - 1557360684 -% 26ntime % 3D1557360684; CNZZDATA30039253 = cnzz_eid % 3D928857818 - 1557361574 -% 26ntime % 3D1557361574; lastpath =/ thread.php ? fid = -7 & rand = 807; bbsmisccookies =% 7B % 22uisetting % 22 % 3A % 7B0 % 3A % 22a % 22 % 2C1 % 3A1557365644 % 7D % 7D; CNZZDATA1256638820 = 2004249330 - 1557362947 - http % 253A % 252F % 252Fbbs.nga.cn % 252F % 7C1557362947; ngaPassportUid = guest05cd3825c55516; taihe = 97269b18af08d5fd64e0275479a4c1ce; taihe_session = 76f606c15009834de6197d67d22b2fda; Hm_lvt_5adc78329e14807f050ce131992ae69b = 1557365346; Hm_lpvt_5adc78329e14807f050ce131992ae69b = 1557365346";
            //HttpResult httpResult =  httpHelper.GetHtml(httpItem);
            string url = ((ComboxItem)comboBox1.SelectedItem).Value;
            UpdateProcessBar(1);

            t = new Thread(HtmlRefresh);
            t.Start(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            DisposeHtml();
        }

        private void DisposeHtml()
        {
            UpdateProcessBar(80);
            StreamReader reader = GetWebBrowser();
            string html = reader.ReadToEnd();
            html = html.Replace("<br/>", "");

            var parsedDocument = HtmlConvert.DeserializeHtml(html);
            dtShow.Clear();
            if (parsedDocument["html"]["body"]["div", 1]["div"]["div", 3].Properties["id"] == "m_threads")
            {
                foreach (var element in parsedDocument["html"]["body"]["div", 1]["div"]["div", 3]["div"]["table"])
                {
                    if (element.TagName == "tbody")
                    {
                        try
                        {
                            DataRow dr = dtShow.NewRow();
                            dr[0] = element["tr"]["td", 1]["a"].Son;
                            string drurl = "http://bbs.nga.cn/read.php?" + element["tr"]["td", 1]["a"].Properties["href"].Substring(element["tr"]["td", 1]["a"].Properties["href"].IndexOf("tid=")) + "&_ff=-7";
                            dr[1] = drurl;
                            //Console.WriteLine(element["tr"]["td", 1]["a"].Son);
                            //Console.WriteLine(element["tr"]["td", 1]["a"].Properties["href"]);
                            dtShow.Rows.Add(dr);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            else
            {
                foreach (var element in parsedDocument["html"]["body"]["div", 1]["div"]["div", 3]["div"])
                {
                    if (element.TagName == "table")
                    {
                        DataRow dr = dtShow.NewRow();
                        try
                        {
                            dr[0] = element["tr"]["td", 1]["span"]["p"].Son;
                            // Console.WriteLine(element["tr"]["td", 1]["span"]["p"].Son);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                dr[0] = element["tr"]["td", 1]["span"]["span"].Son;
                                // Console.WriteLine(element["tr"]["td", 1]["span"]["span"].Son);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        dtShow.Rows.Add(dr);
                    }
                }
            }
            if (parsedDocument["html"]["body"]["div", 1]["div"]["div", 4]["div"]["div", 1].Children != null)
            {
                DataRow dr = dtShow.NewRow();
                dr[0] = "Next";
                string drurl = "";
                if (parsedDocument["html"]["body"]["div", 1]["div"]["div", 3].Properties["id"] == "m_threads")
                {
                    drurl = "http://bbs.nga.cn/thread.php?";
                }
                else
                {
                    drurl = "http://bbs.nga.cn/read.php?";
                }
                foreach (var element in parsedDocument["html"]["body"]["div", 1]["div"]["div", 4]["div"]["div", 1])
                {
                    if (element.Properties.ContainsKey("title") && element.Properties["title"].ToString() == "下一页")
                    {
                        drurl = drurl + element.Properties["href"].Substring(element.Properties["href"].IndexOf("id=") - 1);
                        dr[1] = drurl;
                        dtShow.Rows.Add(dr);
                        break;
                    }
                }
            }

            UpdateDGV();
        }

        delegate StreamReader GetWebBrowserEventHandler();
        private StreamReader GetWebBrowser()
        {
            if (webBrowser1.InvokeRequired)
            {
                object o = webBrowser1.Invoke(new GetWebBrowserEventHandler(GetWebBrowser));
                return (StreamReader)o;
            }
            else
            {
                return new StreamReader(webBrowser1.DocumentStream, Encoding.GetEncoding(webBrowser1.Document.Encoding));
            }
        }

        delegate void UpdateDGVEventHandler();
        private void UpdateDGV()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new UpdateDGVEventHandler(UpdateDGV));
            }
            else
            {
                dataGridView1.DataSource = dtShow;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
                dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
                UpdateProcessBar(100);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Win32.AnimateWindow(this.Handle, 200, Win32.AW_BLEND);

            dtCache.Columns.Add("Value");
            dtCache.Columns.Add("Url");

            dtShow.Columns.Add("Value");
            dtShow.Columns.Add("Url");

            ComboxItem[] items = {
                new ComboxItem("DYW", "http://bbs.nga.cn/thread.php?fid=-7"),
                new ComboxItem("LD", "http://bbs.nga.cn/thread.php?fid=623"),
                new ComboxItem("DTBY", "http://bbs.nga.cn/thread.php?fid=659"),
                new ComboxItem("DDZZQ", "http://bbs.nga.cn/thread.php?fid=641")
            };

            comboBox1.Items.AddRange(items);
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            comboBox1.SelectedIndex = 0;

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.AllowNavigation = true;


            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(dtShow.Rows[e.RowIndex]["Url"].ToString()))
            {
                if (dtShow.Rows[e.RowIndex]["Value"].ToString() != "Next")
                {
                    tmpRowIndex = e.RowIndex;
                    dtCache = dtShow.Copy();
                }
                UpdateProcessBar(1);

                t = new Thread(HtmlRefresh);
                t.Start(dtShow.Rows[e.RowIndex]["Url"].ToString());
            }
            else
            {
                MessageBox.Show(dtShow.Rows[e.RowIndex]["Value"].ToString());
            }
        }

        private void HtmlRefresh(object url)
        {
            int index = 10;
            while (WebBIsBusy())
            {
                UpdateProcessBar(index);
                Thread.Sleep(100);

                if (index >= 100)
                {
                    break;
                }
                index++;
            }
            if (index < 100)
            {
                webBrowser1.Navigate(url.ToString());
            }
        }

        delegate bool WebBIsBusyEventHandler();
        private bool WebBIsBusy()
        {
            if (webBrowser1.InvokeRequired)
            {
                object o = webBrowser1.Invoke(new WebBIsBusyEventHandler(WebBIsBusy));
                return (bool)o;
            }
            else
            {
                return webBrowser1.IsBusy;
            }
        }

        delegate void UpdateProcessBarEventHandler(int index);
        private void UpdateProcessBar(int index)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new UpdateProcessBarEventHandler(UpdateProcessBar), index);
            }
            else
            {
                progressBar1.Value = index;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dtShow.Clear();
            dtShow = dtCache.Copy();
            dataGridView1.DataSource = dtShow;
            dataGridView1.FirstDisplayedScrollingRowIndex = tmpRowIndex;
        }

        public class ComboxItem
        {
            public string Text = "";
            public string Value = "";
            public ComboxItem(string _Text, string _Value)
            {
                Text = _Text;
                Value = _Value;
            }
            public override string ToString()
            {
                return Text;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            Win32.AnimateWindow(this.Handle, 200, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
        }

        private Point mousePoint = new Point();
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
        }
    }

    public class Win32
    {
        public const Int32 AW_HOR_POSITIVE = 0x00000001;    // 从左到右打开窗口
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;    // 从右到左打开窗口
        public const Int32 AW_VER_POSITIVE = 0x00000004;    // 从上到下打开窗口
        public const Int32 AW_VER_NEGATIVE = 0x00000008;    // 从下到上打开窗口
        public const Int32 AW_CENTER = 0x00000010;
        public const Int32 AW_HIDE = 0x00010000;        // 在窗体卸载时若想使用本函数就得加上此常量
        public const Int32 AW_ACTIVATE = 0x00020000;    //在窗体通过本函数打开后，默认情况下会失去焦点，除非加上本常量
        public const Int32 AW_SLIDE = 0x00040000;
        public const Int32 AW_BLEND = 0x00080000;       // 淡入淡出效果
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool AnimateWindow(
        IntPtr hwnd, // handle to window  
        int dwTime, // duration of animation  
        int dwFlags // animation type  
        );
    }

}
