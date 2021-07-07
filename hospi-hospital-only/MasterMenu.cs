using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class MasterMenu : Form
    {
        DBClass dbc = new DBClass();
        string masterName;  // 관리자명

        public string MasterName
        {
            get { return masterName; }
            set { masterName = value; }
        }

        public MasterMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void MasterMenu_Load(object sender, EventArgs e)
        {
            textBoxName.Text = masterName;

            dbc.Master_Open();
            dbc.MasterTable = dbc.DS.Tables["master"];

            for (int i = 0; i < dbc.MasterTable.Rows.Count; i++)
            {
                if(textBoxName.Text == dbc.MasterTable.Rows[0]["masterName"].ToString())
                {
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonInfomation.Enabled = true;
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MasterPassword masterPassword = new MasterPassword();
            masterPassword.MasterName = masterName;
            masterPassword.ShowDialog();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            MasterAdd masterAdd = new MasterAdd();
            masterAdd.ShowDialog();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            MasterDelete masterDelete = new MasterDelete();
            masterDelete.ShowDialog();
        }

        private void buttonInfomation_Click(object sender, EventArgs e)
        {
            MasterInfomation masterInfomation = new MasterInfomation();
            masterInfomation.ShowDialog();
        }
    }
}
