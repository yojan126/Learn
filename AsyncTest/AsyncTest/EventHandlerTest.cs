using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTest
{
    public partial class EventHandlerTest : Form
    {
        private string _text;

        public string GetText()
        {
            return _text;
        }

        public void SetText(string value)
        {
            _text = value;
        }

        public EventHandlerTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetText("AAA");
        }

        public void Show(object sender, EventArgs e)
        {
            MessageBox.Show(GetText());
        }
    }
}
