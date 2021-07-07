using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetDefaultConfigEncryption(true);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        private static void SetDefaultConfigEncryption(bool encrypt)
        {
            SetConfigSectionEncryption(encrypt, "appSettings");
        }

        private static void SetConfigSectionEncryption(bool encrypt, string sectionKey)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection(sectionKey);
            if (section != null)
            {
                if (encrypt && !section.SectionInformation.IsProtected)
                {
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                }
                if (!encrypt && section.SectionInformation.IsProtected)
                {
                    section.SectionInformation.UnprotectSection();
                }
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);
            }
        }
    }
}
