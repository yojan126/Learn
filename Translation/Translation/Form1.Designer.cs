namespace Translation
{
    partial class Form1
    {
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_OpenExcel = new System.Windows.Forms.Button();
            this.txt_OpenExcel = new System.Windows.Forms.TextBox();
            this.btn_Trans = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_OpenExcel
            // 
            this.btn_OpenExcel.Location = new System.Drawing.Point(322, 27);
            this.btn_OpenExcel.Name = "btn_OpenExcel";
            this.btn_OpenExcel.Size = new System.Drawing.Size(88, 23);
            this.btn_OpenExcel.TabIndex = 1;
            this.btn_OpenExcel.Text = "Select Excel";
            this.btn_OpenExcel.UseVisualStyleBackColor = true;
            this.btn_OpenExcel.Click += new System.EventHandler(this.btn_OpenExcel_Click);
            // 
            // txt_OpenExcel
            // 
            this.txt_OpenExcel.Location = new System.Drawing.Point(12, 27);
            this.txt_OpenExcel.Name = "txt_OpenExcel";
            this.txt_OpenExcel.Size = new System.Drawing.Size(304, 21);
            this.txt_OpenExcel.TabIndex = 2;
            // 
            // btn_Trans
            // 
            this.btn_Trans.Location = new System.Drawing.Point(322, 56);
            this.btn_Trans.Name = "btn_Trans";
            this.btn_Trans.Size = new System.Drawing.Size(88, 23);
            this.btn_Trans.TabIndex = 3;
            this.btn_Trans.Text = "Translate";
            this.btn_Trans.UseVisualStyleBackColor = true;
            this.btn_Trans.Click += new System.EventHandler(this.btn_Trans_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 56);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(304, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 105);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_Trans);
            this.Controls.Add(this.txt_OpenExcel);
            this.Controls.Add(this.btn_OpenExcel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel Translation Tool ver 1.1 by JohnFeng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_OpenExcel;
        private System.Windows.Forms.TextBox txt_OpenExcel;
        private System.Windows.Forms.Button btn_Trans;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

