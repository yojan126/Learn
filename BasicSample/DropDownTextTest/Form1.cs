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
        bool dataFlag = true;
        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            string input = comboBox1.Text.ToString();
            Thread t = new Thread(GetData);
            t.Start(input);
        }

        private List<string> lstDataCache = new List<string>();

        private void GetData(object inputText)
        {
            List<string> lst = new List<string>();
            if (lstDataCache != null && lstDataCache.Count > 0)
            {
                lst = lstDataCache.Where(t => t[0].ToString().IndexOf(inputText.ToString()) > -1).ToList();
            }
            else
            {
                DataTable dt = DBHelper.GetTable("select value from data where value like '" + inputText + "%'");
                foreach (DataRow dr in dt.Rows)
                {
                    lstDataCache.Add(dr[0].ToString());
                }
                GetData(inputText);
            }
            if (lst == null || lst.Count == 0)
            {
                DataTable dt = DBHelper.GetTable("select value from data where value like '" + inputText + "%'");
                foreach (DataRow dr in dt.Rows)
                {
                    lstDataCache.Add(dr[0].ToString());
                }
                lst = lstDataCache.Where(t => t[0].ToString().IndexOf(inputText.ToString()) > -1).ToList();
            }

            SetData(lst);
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
                if (lstData == null || lstData.Count == 0)
                {

                }
                else
                {
                    string tmp = comboBox1.Text;
                    comboBox1.ValueMember = "value";
                    DataRow dr = dt.NewRow();
                    dr[0] = tmp;
                    dt.Rows.InsertAt(dr, 0);
                    comboBox1.DataSource = dt;
                    comboBox1.SelectedIndex = 0;
                    comboBox1.SelectionStart = this.comboBox1.Text.Length;
                    Cursor = Cursors.Default;
                    comboBox1.DroppedDown = true;
                    //comboBox1.SelectedIndex = -1;
                }
            }
        }
    }
}
