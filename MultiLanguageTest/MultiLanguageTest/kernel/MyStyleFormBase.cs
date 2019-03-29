using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLanguageTest.kernel
{
    public class MyStyleFormBase : Form
    {
        public MyStyleFormBase()
        {
            if (!Thread.CurrentThread.CurrentUICulture.Name.Equals("zh-CN", StringComparison.OrdinalIgnoreCase)) //如果是简体，则无需转换
            {
                base.TextChanged += MyStyleFormBase_TextChanged;
                base.Shown += MyStyleFormBase_Shown;
            }
        }

        private void MyStyleFormBase_TextChanged(object sender, EventArgs e)
        {
            this.Text = LanguageHelper.GetLanguageText(this.Text);
        }

        private void MyStyleFormBase_Shown(object sender, EventArgs e)
        {
            LanguageHelper.SetControlLanguageText(this);
            base.ControlAdded += MyStyleFormBase_ControlAdded;
        }

        private void MyStyleFormBase_ControlAdded(object sender, ControlEventArgs e)
        {
            LanguageHelper.SetControlLanguageText(e.Control);
        }

        /// <summary>
        /// 强制通知子控件改变消息
        /// </summary>
        /// <param name="target"></param>
        protected virtual void PerformChildrenChange(Control target)
        {
            LanguageHelper.SetControlLanguageText(target);
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        protected DialogResult MessageBoxShow(string text, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(LanguageHelper.GetLanguageText(text), LanguageHelper.GetLanguageText(caption), buttons, icon, defaultButton);
        }
    }
}
