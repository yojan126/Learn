using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public class TabCtrlX : TabControl
    {
        public TabCtrlX()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);//用户自己绘制
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //让控件支持透明色
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        #region designer
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
        #endregion
        //[EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override Color BackColor
        {
            get
            {
                return Color.FromArgb(Alpha, BgColor);
            }
            set
            {
                Alpha = BackColor.A;
                BgColor = BackColor;
            }
        }

        [DisplayName("Color.Alpha")]
        [CategoryAttribute("Color"), DescriptionAttribute("Opacity不透明度0--255")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public byte Alpha { get; set; } = 16;

        [DisplayName("Color.BgColor")]
        [CategoryAttribute("Color"), DescriptionAttribute("TabControl工作区背景色")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public Color BgColor { get; set; } = Color.White;

        [DisplayName("Color.BordColor")]
        [CategoryAttribute("Color"), DescriptionAttribute("TabControl边线颜色")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public Color BordColor { get; set; } = Color.LightGray;

        [DisplayName("Color.TitleColor")]
        [CategoryAttribute("Color"), DescriptionAttribute("TabPage标头背景色")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public Color TitleColor { get; set; } = Color.WhiteSmoke;

        [DisplayName("Color.TitleSeleColor")]
        [CategoryAttribute("Color"), DescriptionAttribute("TabPage标头选中背景色")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public Color TitleSeleColor { get; set; } = Color.White;

        [DisplayName("Color.TextColor")]
        [CategoryAttribute("Color"), DescriptionAttribute("TabPage标题颜色")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public Color TextColor { get; set; } = Color.Gray;

        [DisplayName("Color.TextSeleColor")]
        [CategoryAttribute("Color"), DescriptionAttribute("TabPage选中标题颜色")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public Color TextSeleColor { get; set; } = Color.Black;

        protected override void OnPaint(PaintEventArgs e)
        {
            this.DrawTitle(e.Graphics);
            base.OnPaint(e);
            DrawBorder(e.Graphics);
        }

        protected void DrawBorder(Graphics g)
        {
            g.DrawRectangle(new Pen(BordColor, 1F), ClientRectangle);
        }

        protected void DrawTitle(Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            using (SolidBrush sb = new SolidBrush(SystemColors.Control))
            {
                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    Rectangle rect = this.GetTabRect(i);
                    if (this.SelectedIndex == i)
                    {
                        sb.Color = TitleSeleColor;
                        g.FillRectangle(sb, rect);
                        g.DrawString(this.TabPages[i].Text, this.Font, new SolidBrush(TextSeleColor), rect, sf);
                    }
                    else
                    {
                        sb.Color = TitleColor;
                        g.FillRectangle(sb, rect);
                        g.DrawString(this.TabPages[i].Text, this.Font, new SolidBrush(TextColor), rect, sf);
                    }
                }
            }
        }
    }
}
