using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BreakpointResumeDemo
{
    class Server
    {
        string filePath;
        StreamReader reader;
        FileStream fileStream;
        BinaryReader binaryReader;

        public Server(string filePath)
        {
            this.filePath = filePath;
            reader = new StreamReader(filePath);
            fileStream = new FileStream(filePath, FileMode.Open,FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);
        }

        public byte[] ReturnFile(int start,int length)
        {
            int size = fileStream.Length-1 - long.Parse(start.ToString()) >= length ? length : Convert.ToInt32(fileStream.Length-1 - long.Parse(start.ToString()));
           
            byte[] b = new byte[size];
            //FileStream fileStream = new FileStream(filePath, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fileStream);
            binaryReader.Read(b, 0, size);
            return b;
        }
    }
}
