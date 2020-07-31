using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BreakpointResumeDemo
{
    class Client
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int TotalSize { get; set; }
        public int DownloadSize { get; set; }

        private int blockLength = 1024*1024;//1M
        private int blockCount = 1;
        public int StartPosition { get; set; } = 0;

        List<byte[]> datas = new List<byte[]>();

        /// <summary>
        /// 读取文件特定长度的数据，用来模拟rpc通信时客户端收到的服务端返回的数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetFile(int start,int length)
        {
            StreamReader reader = new StreamReader(FilePath);
            char[] ch = new char[8*length];
            reader.Read(ch, 0, ch.Length);
            return new byte[length];         

            byte[] b = new byte[blockLength];
            
        }

        public void DownloadFile()
        {
            while (true)
            {
                byte[] b = GetFile(StartPosition, blockLength);
                datas.Add(b);
                if (datas.Count == blockCount)
                {
                    byte[] bt = new byte[blockLength * blockCount];
                    datas.ForEach(o => bt.CopyTo(o, 0));
                    File.WriteAllBytes("", bt);
                }
            }
        }
    }
}
