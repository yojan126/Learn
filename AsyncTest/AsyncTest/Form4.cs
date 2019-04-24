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

namespace AsyncTest
{
    public partial class Form4 : Form
    {
        public DataTable dtBack = new DataTable();
        public DataTable dtShow = new DataTable();
        Thread t1;
        Thread t2;
        private static readonly object lockobj = new object();
        public Form4()
        {
            InitializeComponent();
            dtBack.Columns.Add("ID", typeof(int));
            dtBack.Columns.Add("Value", typeof(string));
            Random r = new Random();
            for (int j = 0; j < 10; j++)
            {
                DataRow dr = dtBack.NewRow();
                dr[0] = j;
                dr[1] = r.Next(999).ToString();
                dtBack.Rows.Add(dr);
            }
            dataGridView1.DataSource = dtBack;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t1 = new Thread(CreateData);
            t1.Start();

            t2 = new Thread(dgvRefresh);
            t2.Start();
        }

        public void dgvRefresh()
        {
            while (true)
            {
                Thread.Sleep(200);
                ShowData();
            }
        }

        delegate void ShowDataEventHandler();

        public void ShowData()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke(new ShowDataEventHandler(ShowData));
            }
            else
            {
                lock (lockobj)
                {
                    //dtShow = dtBack.Copy();
                    //dataGridView1.DataSource = dtShow;
                    dataGridView1.Refresh();         // 相较于refresh方法，目前的方法datagridview不会出现闪烁现象
                }
                // dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
            }
        }

        public void CreateData()
        {
            Random r = new Random();
            for (int i = 0; i < 100000; i++)
            {
                Thread.Sleep(200);
                //DataRow dr = dtBack.NewRow();
                //dr[0] = i;
                //dr[1] = r.Next(999).ToString();
                //SetData(dr);
                for (int j = 0; j < 10; j++)
                {
                    dtBack.Rows[j][1]= r.Next(999).ToString();
                }
            }
        }

        public void SetData(DataRow dr)
        {
            lock (lockobj)
            {
                dtBack.Rows.Add(dr);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t1.Abort();
            t2.Abort();
        }
    }
}
