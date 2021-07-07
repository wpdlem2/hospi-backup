using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    class FBKey
    {
        private string password = ConfigurationManager.AppSettings["FBKeyPwd"]; // 비밀번호
        private string encyptKeyPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Hospi\service-account.hos";
        private string tempFolderPath;
        private string tempKeyFilePath;
        private string tempFileName;
        private DirectoryInfo di;

        public string TempFolderPath
        {
            get { return tempFolderPath; }
        }

        public string TempKeyFilePath
        {
            get { return tempKeyFilePath; }
        }

        public string TempFileName
        {
            get { return tempFileName; }
        }

        public FBKey()
        {
            try
            {
                tempFolderPath = Path.GetTempPath() + Path.GetRandomFileName();
                di = new DirectoryInfo(tempFolderPath);
                if (di.Exists == false) // 폴더가 존재하지 않으면
                {
                    di.Create(); // 폴더 생성
                }
                tempFileName = Path.GetRandomFileName();
                tempKeyFilePath = tempFolderPath + "\\" + tempFileName + ".json";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DecryptFile()
        {
            try
            {
                FileStream fsCrypt = new FileStream(encyptKeyPath, FileMode.Open);

                Aes aes = Aes.Create();

                byte[] iv = new byte[aes.IV.Length];
                int numBytesToRead = aes.IV.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fsCrypt.Read(iv, numBytesRead, numBytesToRead);
                    if (n == 0) break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }

                //byte[] key = Encoding.UTF8.GetBytes(password);
                SHA256Managed sha256Managed = new SHA256Managed();
                byte[] encryptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(password));

                CryptoStream cs = new CryptoStream(fsCrypt,
                aes.CreateDecryptor(encryptBytes, iv),
                CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(tempKeyFilePath, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("DecryptFile: " + e.Message);
            }
        }

        public void DeleteTemp()
        {
            di.Delete(true);
        }
    }
}
