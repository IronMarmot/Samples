using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        string filePath;
        string fileName;
        int fileSize = 0;
        Client client;
        bool btnStatu = false;//初始时默认暂停状态，为false
        System.Windows.Forms.Timer timer;


        public Form1()
        {
            InitializeComponent();
            btn_Pause.Enabled = false;

            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 2000;
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            bool isOK = false;
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                fileName=openFileDialog1.SafeFileName;
                Stream rs=openFileDialog1.OpenFile();
                tb_FileName.Text = filePath;
                fileSize = Convert.ToInt32(rs.Length);
                lb_FileSize.Text=lb_ClientSize.Text = fileSize + " kb";

                btn_Pause.Enabled = true;
                isOK = true;
                //Thread t = new Thread(RefreshClient);
                //t.Start();
            }
            if (isOK)
            {
                client = new Client(filePath, fileName, fileSize);
                timer.Start();
            }
        }

        /// <summary>
        /// 
        /// 开始后，客户端向服务端请求数据，接收到数据后写入，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pause_Click(object sender, EventArgs e)
        {
            ChangeStatus();

            Task.Factory.StartNew(new Action(()=> client.DownloadFile()));
            
        }

        private void ChangeStatus()
        {
            btn_Pause.Text = btnStatu ? "开始" : "暂停";
            client.isPause = btnStatu = !btnStatu;
        }

        private void RefreshClient()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lb_download.Invoke(new Action(() => { lb_download.Text = client.DownloadSize.ToString()+" kb"; }));
            double process = client.DownloadSize / fileSize * 100;
            lb_download.Invoke(new Action(() => { lb_process.Text = process.ToString() + " %"; }));
        }
    }
}
