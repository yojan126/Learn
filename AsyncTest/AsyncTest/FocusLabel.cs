using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTest
{
    public class FocusLabel : Label
    {
        private bool m_ShowBorder = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_ShowBorder)
            {
                Rectangle rect = this.ClientRectangle;
                rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                e.Graphics.DrawRectangle(new Pen(Color.Orange), rect);
            }
            base.OnPaint(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (this.Parent != null)
            {
                foreach (Control ctrl in this.Parent.Controls)
                {
                    if (ctrl is FocusLabel && ctrl != this)
                    {
                        ((FocusLabel)ctrl).m_ShowBorder = false;
                        ctrl.Refresh();
                    }
                }
            }

            this.m_ShowBorder = true;
            this.Refresh();

        }
    }
}
