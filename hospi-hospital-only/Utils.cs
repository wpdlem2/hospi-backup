using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    class Utils
    {
        public static void FtpDownload(string fileName, string outputPath)
        {
            string ftpPath = ConfigurationManager.AppSettings["FtpPath"] + fileName;
            string user = ConfigurationManager.AppSettings["FtpId"];
            string pwd = ConfigurationManager.AppSettings["FtpPwd"];
            string outputFile = fileName;

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath);
            req.Credentials = new NetworkCredential(user, pwd);
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.EnableSsl = true;

            // 파일 다운로드
            using (var localfile = File.Open(outputPath + outputFile, FileMode.Create))
            using (var ftpStream = req.GetResponse().GetResponseStream())
            {
                byte[] buffer = new byte[1024];
                int n;
                while ((n = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    localfile.Write(buffer, 0, n);
                }
            }
        }
    }
}
