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
    public partial class MasterInfomation : Form
    {
        DBClass dbc = new DBClass();

        public MasterInfomation()
        {
            InitializeComponent();
        }

        private void MasterInfomation_Load(object sender, EventArgs e)
        {
            dbc.Master_Open();
            dbc.MasterTable = dbc.DS.Tables["master"];

            textBoxCount.Text = dbc.MasterTable.Rows.Count.ToString();

            for(int i=0; i<dbc.MasterTable.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = dbc.MasterTable.Rows[i]["masterID"].ToString();
                item.SubItems.Add(dbc.MasterTable.Rows[i]["masterName"].ToString());

                listView2.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 비밀번호 확인 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.FormNum = 5;
            checkMasterPW.ShowDialog();
            int passwordOK = checkMasterPW.PasswordOK;

            if(passwordOK == 1)
            {
                listView2.Items.Clear();

                for (int i = 0; i < dbc.MasterTable.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = dbc.MasterTable.Rows[i]["masterID"].ToString();
                    item.SubItems.Add(dbc.MasterTable.Rows[i]["masterName"].ToString());
                    item.SubItems.Add(dbc.MasterTable.Rows[i]["masterPassword"].ToString());

                    listView2.Items.Add(item);
                }
                button1.Enabled = false;
            }
        }
    }
}
