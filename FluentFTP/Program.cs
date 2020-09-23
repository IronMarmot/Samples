using FluentFTP.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            FTPHelper helper = new FTPHelper();
            //helper.InitClient("123.57.233.60", "10003", "anonymous", "");
            helper.InitClient("116.62.120.174", "21", "anonymous", "");
            //helper.InitClient("116.62.120.174", "21", "ftpuser", "12345678");
            //helper.InitClient("123.57.233.60", "10003", "wm01", "wm01");
            //helper.Connect();
            if (helper.Connect())
            {
                Console.WriteLine(helper.Connected);
                try
                {
                    //helper.DownloadFiles("123.57.233.60", "10003", "anonymous", "","tmp", "/pub", "","*",true,false);
                    FtpListItem[] aa = helper.ftp.GetListing("/pub");
                    Console.WriteLine(aa.Length);

                    //helper.ftp.DownloadDirectory("tmp", "/pub", FtpFolderSyncMode.Update);

                    foreach (var item in aa)
                    {
                        if (item.Type == FtpFileSystemObjectType.File)
                        {

                            // get the file size
                            long size = helper.ftp.GetFileSize(item.FullName);

                            // calculate a hash for the file on the server side (default algorithm)
                            FtpHash hash = helper.ftp.GetChecksum(item.FullName);

                            //helper.ftp.DownloadFile("tmp", item.FullName, FtpLocalExists.Overwrite,FtpVerify.Retry);
                        }
                        else if (item.Type == FtpFileSystemObjectType.Directory)
                        {
                            Console.WriteLine(DateTime.Now);
                            string folder = item.FullName.Split('/').Reverse().ToArray()[0];
                            string localFolder = "tmp/" + folder;
                            if (!System.IO.Directory.Exists(localFolder))
                            {
                                System.IO.Directory.CreateDirectory(localFolder);
                            }
                            helper.ftp.DownloadDirectory(localFolder, item.FullName, FtpFolderSyncMode.Update);
                            Console.WriteLine(DateTime.Now);
                        }
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            
            Console.ReadLine();
        }
    }
}
