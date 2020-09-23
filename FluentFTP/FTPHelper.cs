using FluentFTP;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace FluentFTP
{
    public class FTPHelper
    {
        public FtpClient ftp;
        public bool Connected { get { return ftp.IsConnected; } }

        public void InitClient(string server, string port, string user, string pwd)
        {
            try
            {
                ftp = new FtpClient(server, Int32.Parse(port), new NetworkCredential(user, pwd));
                //ftp.DataConnectionType = FtpDataConnectionType.PASV;
                //ftp.DataConnectionType = FtpDataConnectionType.PORT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Connect()
        {
            try
            {
                if (!Connected)
                {
                    ftp.Connect();
                }
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (ftp != null && Connected)
                {
                    ftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ArchiveFile(string oldPath, string newPath, string fileName)
        {
            try
            {
                if (Connect())
                {

                    if (!ftp.DirectoryExists(newPath))
                        ftp.CreateDirectory(newPath);
                    ftp.Rename(oldPath + "/" + fileName, newPath + "/" +
                        Path.GetFileNameWithoutExtension(fileName) + "_" +
                        DateTime.Now.ToString("yyyyMMddHHmmssffff") + Path.GetExtension(fileName));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DownloadFiles(string server, string port, string user, string password,
            string localPath, string remotePath, string archiveFilePath, string fileType,
            bool recursive, bool archive)
        {
            try
            {
                InitClient(server, port, user, password);
                if (Connect())
                {
                    foreach (var ftpListItem in ftp.GetListing(remotePath))
                    {
                        RecursiveFiles(ftpListItem, fileType, localPath, remotePath, archiveFilePath, recursive, archive);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }

        public void RecursiveFiles(FtpListItem file, string fileType, string localPath,
            string remotePath, string archiveFilePath, bool recursive, bool archive)
        {
            try
            {
                if (file.Type.ToString().Equals("Directory"))
                {
                    if (recursive)
                    {
                        foreach (var recursiveFile in ftp.GetListing(file.FullName))
                        {
                            localPath += (file.FullName.Split('/').Reverse().ToArray()[0]);
                            RecursiveFiles(recursiveFile, fileType, localPath, file.FullName, archiveFilePath, recursive, archive);
                        }
                    }
                }
                else
                {
                    ProcessFiles(file, fileType, localPath, remotePath, archiveFilePath, archive);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessFiles(FtpListItem file, string fileType, string localPath,
            string remotePath, string archiveFilePath, bool archive)
        {
            string fileName = file.Name;
            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);
            try
            {
                if (fileType.Equals("*"))
                {
                    WriteFileToLocalStorage(file, fileName, localPath, remotePath, archiveFilePath, archive);
                }
                else
                {
                    string[] fileTypes = fileType.Split(',');
                    foreach (var type in fileTypes)
                    {
                        if (type.ToLower().Equals(Path.GetExtension(fileName).ToLower()))
                        {
                            WriteFileToLocalStorage(file, fileName, localPath, remotePath, archiveFilePath, archive);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteFileToLocalStorage(FtpListItem file, string fileName, string localPath, string remotePath,
            string archiveFilePath, bool archive)
        {
            Stream ftpStream = null;
            FileStream fileStream = null;
            try
            {
                string destinationPath = string.Format(@"{0}\{1}", localPath, file.Name);
                ftpStream = ftp.OpenRead(file.FullName);
                if (ftpStream.Length > 0)
                {
                    fileStream = File.Create(destinationPath, (int)ftpStream.Length);
                    byte[] buffer = new byte[200 * 1024];
                    int count;
                    while ((count = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, count);
                    }
                }
                if (archive && !string.IsNullOrEmpty(archiveFilePath))
                {
                    ArchiveFile(remotePath, archiveFilePath, fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ftpStream != null)
                {
                    ftpStream.Flush();
                    ftpStream.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
        }
    }
}

