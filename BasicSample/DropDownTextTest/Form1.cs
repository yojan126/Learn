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
using DAL;

namespace DropDownTextTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            string input = comboBox1.Text.ToString();
            Thread t = new Thread(GetData);
            t.Start(input);
        }

        private List<string> lstDataCache = new List<string>();
        private readonly object locker = new object();

        private void GetData(object inputText)
        {
            List<string> lst = new List<string>();
            lock (locker)
            {
                if (lstDataCache != null && lstDataCache.Count > 0)
                {
                    lst = lstDataCache.Where(t => t.StartsWith(inputText.ToString())).ToList();
                }
                else
                {
                    DataTable dt = DBHelper.GetTable("select value from data where value like '" + inputText + "%'");
                    lstDataCache.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstDataCache.Add(dr[0].ToString());
                    }
                    lst = lstDataCache.Where(t => t.StartsWith(inputText.ToString())).ToList();
                }
                if (lst == null || lst.Count == 0)
                {
                    DataTable dt = DBHelper.GetTable("select value from data where value like '" + inputText + "%'");
                    lstDataCache.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstDataCache.Add(dr[0].ToString());
                    }
                    lst = lstDataCache.Where(t => t.StartsWith(inputText.ToString())).ToList();
                }

                SetData(lst);
            }
        }

        delegate void SetDataEventHandler(List<string> lstData);

        private void SetData(List<string> lstData)
        {
            if (this.comboBox1.InvokeRequired)
            {
                comboBox1.BeginInvoke(new SetDataEventHandler(SetData), lstData);
            }
            else
            {
                if (comboBox1.Text.ToString().Length > 0)
                {
                    if (lstData.Count > 0 && comboBox1.Text.ToString() != lstData[0])
                    {
                        lstData.Insert(0, comboBox1.Text.ToString());
                    }
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(lstData.ToArray());
                    // comboBox1.SelectedIndex = 0;
                    // 设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
                    comboBox1.SelectionStart = this.comboBox1.Text.Length;
                    // 保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置
                    Cursor = Cursors.Default;
                    // 自动弹出下拉框
                    comboBox1.DroppedDown = true;
                }
                else
                {
                    comboBox1.Items.Clear();
                }
            }
        }
    }
}
