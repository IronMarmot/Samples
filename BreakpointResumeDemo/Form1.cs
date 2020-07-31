using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * 断点下载示例
 *      本示例通过一个本地文件来演示断点下载的原理。
 *      1：客户端每次请求固定长度的数据，数据达到一定值后写一个小文件，最终合并所有小文件。
 */
namespace BreakpointResumeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                Stream rs=openFileDialog1.OpenFile();
                tb_FileName.Text = filePath;
                lb_FileSize.Text = rs.Length.ToString()+" kb";
            }
        }
    }
}
