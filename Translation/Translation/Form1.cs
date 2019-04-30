using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TranCore;
using ExcelEditCore;
using System.Threading;

namespace Translation
{
    public partial class Form1 : Form
    {
        Common com = new Common();
        ExcelEdit ee = new ExcelEdit();
        Dictionary<string, string> dicAllWords = new Dictionary<string, string>();
        Thread t;
        Task task;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_OpenExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = "E:\\ProgramData\\Document\\WeChat Files\\fzh_126\\Files";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "Excel(xls,xlsx)|*.xlsx;*.xls";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                txt_OpenExcel.Text = fName;

                ee.mFilename = fName;
            }
        }

        private void btn_Trans_Click(object sender, EventArgs e)
        {
            try
            {
                btn_OpenExcel.Enabled = false;
                btn_Trans.Enabled = false;
                task = new Task(DoTranslate);
                task.Start();
                //t = new Thread(DoTranslate);
                //t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error");
            }
        }

        private void DoTranslate()
        {
            UpdateProgress(10);
            Dictionary<string, string> dicWaitTrans = new Dictionary<string, string>();
            bool bolFlag = true;
            string strNewFile = "";
            do
            {
                dicWaitTrans = ee.GetExcelWords(ref bolFlag, dicAllWords);
                // Thread.Sleep(1000);
                UpdateProgress(30);
                if (com.GetMes(ref dicWaitTrans))
                {
                    foreach (string key in dicWaitTrans.Keys)
                    {
                        if (!dicAllWords.ContainsKey(key))
                        {
                            dicAllWords.Add(key, dicWaitTrans[key]);
                        }
                    }
                }
            } while (bolFlag);
            // Thread.Sleep(1000);
            UpdateProgress(70);

            if (dicAllWords != null && dicAllWords.Count > 0 && dicAllWords.Keys.ToArray()[0] != "ERROR")
            {
                ee.UpdateAndCreateExcel(dicAllWords, ref strNewFile);
                UpdateProgress(100);
                MessageBox.Show("保存路径：" + strNewFile, "翻译成功");
            }
        }

        private delegate void UPEventHandler(int i);

        private void UpdateProgress(int index)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new UPEventHandler(UpdateProgress), index);
            }
            else
            {
                progressBar1.Value = index;
                if (index == 100)
                {
                    btn_OpenExcel.Enabled = true;
                    btn_Trans.Enabled = true;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            task.Dispose();
            //t.Abort();
        }
    }
}
