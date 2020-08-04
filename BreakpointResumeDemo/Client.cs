using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BreakpointResumeDemo
{
    /// <summary>
    /// 客户端向服务端请求数据时，进行请求数据的控制
    /// </summary>
    class Client
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int TotalSize { get; set; }
        public int DownloadSize { get; set; }
        public int StartPosition { get; set; } = 0;

        private int blockLength = 1024*1024;//1M
        private int blockCount = 1;

        List<byte[]> datas = new List<byte[]>();

        bool isComplete = false;
        public bool isPause = true;
        int fileNum = 0;

        Server server;
        FileStream fileStream;

        public Client(string path,string name,int size)
        {
            FilePath = path;
            FileName = name;
            TotalSize = size;

            server = new Server(FilePath);
            fileStream = new FileStream(name, FileMode.Append, FileAccess.Write);
        }

        /// <summary>
        /// 读取文件特定长度的数据，用来模拟rpc通信时客户端收到的服务端返回的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetBlock()
        {
            //if (TotalSize-StartPosition<blockLength)
            //{
            //    blockLength = TotalSize - StartPosition;
            //}
            return server.ReturnFile(StartPosition, blockLength);
        }

        public void DownloadFile()
        {
            isPause = !isPause;

            while (!isComplete&&!isPause)
            {
                byte[] b = GetBlock();
                datas.Add(b);
                DownloadSize += b.Length;
                if (DownloadSize== TotalSize)
                {
                    isComplete = true;
                }
                if (datas.Count == blockCount|| isComplete)
                {
                    w2f();
                }
                StartPosition= DownloadSize-1;
                if (isComplete)
                {
                    //isComplete = true;
                    //w2f();
                    //StartPosition = 0;
                    break;
                }
            }
            if (isComplete)
            {
                StartPosition = 0;
            }
        }

        private void w2f()
        {
            fileNum++;
            //byte[] bt = new byte[blockLength * datas.Count];
            byte[] bt = new byte[datas.Sum(o => o.Length)] ;
            datas.ForEach(o => bt.CopyTo(o, 0));
            //string name = FileName + fileNum;
            //File.WriteAllBytes(name, bt);
            //FileStream fileStream = new FileStream(name, FileMode.Append, FileAccess.Write);
            fileStream.Write(bt, 0, bt.Length);
            fileStream.Flush();
            
            datas.Clear();
        }
    }
}
