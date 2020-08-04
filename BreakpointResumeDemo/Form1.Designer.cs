namespace BreakpointResumeDemo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_FileSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_FileName = new System.Windows.Forms.TextBox();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_ClientSize = new System.Windows.Forms.Label();
            this.lb_process = new System.Windows.Forms.Label();
            this.lb_download = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_FileSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_FileName);
            this.groupBox1.Controls.Add(this.btn_OpenFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 177);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务端";
            // 
            // lb_FileSize
            // 
            this.lb_FileSize.AutoSize = true;
            this.lb_FileSize.Location = new System.Drawing.Point(75, 88);
            this.lb_FileSize.Name = "lb_FileSize";
            this.lb_FileSize.Size = new System.Drawing.Size(23, 12);
            this.lb_FileSize.TabIndex = 2;
            this.lb_FileSize.Text = "0 b";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件大小：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件路径：";
            // 
            // tb_FileName
            // 
            this.tb_FileName.Location = new System.Drawing.Point(77, 52);
            this.tb_FileName.Name = "tb_FileName";
            this.tb_FileName.ReadOnly = true;
            this.tb_FileName.Size = new System.Drawing.Size(162, 21);
            this.tb_FileName.TabIndex = 1;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Location = new System.Drawing.Point(19, 20);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(98, 23);
            this.btn_Pause.TabIndex = 0;
            this.btn_Pause.Text = "开   始";
            this.btn_Pause.UseVisualStyleBackColor = true;
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Location = new System.Drawing.Point(13, 20);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(226, 23);
            this.btn_OpenFile.TabIndex = 0;
            this.btn_OpenFile.Text = "选择文件";
            this.btn_OpenFile.UseVisualStyleBackColor = true;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_ClientSize);
            this.groupBox2.Controls.Add(this.lb_process);
            this.groupBox2.Controls.Add(this.lb_download);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn_Pause);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(278, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 177);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客户端";
            // 
            // lb_ClientSize
            // 
            this.lb_ClientSize.AutoSize = true;
            this.lb_ClientSize.Location = new System.Drawing.Point(89, 55);
            this.lb_ClientSize.Name = "lb_ClientSize";
            this.lb_ClientSize.Size = new System.Drawing.Size(23, 12);
            this.lb_ClientSize.TabIndex = 3;
            this.lb_ClientSize.Text = "0 b";
            // 
            // lb_process
            // 
            this.lb_process.AutoSize = true;
            this.lb_process.Location = new System.Drawing.Point(89, 118);
            this.lb_process.Name = "lb_process";
            this.lb_process.Size = new System.Drawing.Size(23, 12);
            this.lb_process.TabIndex = 2;
            this.lb_process.Text = "0 %";
            // 
            // lb_download
            // 
            this.lb_download.AutoSize = true;
            this.lb_download.Location = new System.Drawing.Point(89, 86);
            this.lb_download.Name = "lb_download";
            this.lb_download.Size = new System.Drawing.Size(23, 12);
            this.lb_download.TabIndex = 4;
            this.lb_download.Text = "0 b";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "下载进度：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "已下载：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "文件大小：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(547, 197);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "断点下载";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lb_FileSize;
        private System.Windows.Forms.Label lb_process;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_FileName;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.Label lb_ClientSize;
        private System.Windows.Forms.Label lb_download;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

