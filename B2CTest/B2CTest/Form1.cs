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
                            dr[0] = element["tr"]["td", 1]["span"]["span"].Son;
                            // Console.WriteLine(element["tr"]["td", 1]["span"]["span"].Son);
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
                    if (element.Properties["title"].ToString() == "下一页")
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
            dtCache.Columns.Add("Value");
            dtCache.Columns.Add("Url");

            dtShow.Columns.Add("Value");
            dtShow.Columns.Add("Url");

            ComboxItem[] items = {
                new ComboxItem("DYW", "http://bbs.nga.cn/thread.php?fid=-7&rand=330"),
                new ComboxItem("LD", "http://bbs.nga.cn/thread.php?fid=623&rand=788")
            };

            comboBox1.Items.AddRange(items);
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            comboBox1.SelectedIndex = 0;

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.AllowNavigation = true;
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

                t = new Thread(HtmlRefresh);
                t.Start(dtShow.Rows[e.RowIndex]["Url"].ToString());
            }
        }

        private void HtmlRefresh(object url)
        {
            int index = 10;
            while (WebBIsBusy())
            {
                UpdateProcessBar(index);
                Thread.Sleep(100);
                index++;
            }
            webBrowser1.Navigate(url.ToString());
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
            t.Abort();
        }
    }
}
