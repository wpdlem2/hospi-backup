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
    public partial class MasterPassword : Form
    {
        DBClass dbc = new DBClass();
        string masterName;
        int masterID;

        public string MasterName
        {
            get { return masterName; }
            set { masterName = value; }
        }

        public MasterPassword()
        {
            InitializeComponent();
        }

        private void MasterPassword_Load(object sender, EventArgs e)
        {
            textBoxName.Text = masterName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 비밀번호 변경 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if(pwLabel1.Visible == true && pwLabel2.Visible == true)
            {
                DialogResult ok = MessageBox.Show("비밀변호 변경을 완료 하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ok == DialogResult.Yes)
                {
                    dbc.Master_Open();
                    dbc.MasterTable = dbc.DS.Tables["master"];

                    for(int i=0; i<dbc.MasterTable.Rows.Count; i++)
                    {
                        if(dbc.MasterTable.Rows[i]["masterName"].ToString() == textBoxName.Text)
                        {
                            masterID = Convert.ToInt32(dbc.MasterTable.Rows[i]["masterID"]);
                        }
                    }
                    DataRow upRow = dbc.MasterTable.Rows[masterID];
                    upRow.BeginEdit();
                    upRow["masterPassword"] = textBoxPW1.Text;
                    upRow.EndEdit();
                    dbc.DBAdapter.Update(dbc.DS, "master");
                    dbc.DS.AcceptChanges();

                    MessageBox.Show("변경이 완료 되었습니다.", "알림");
                    Dispose();
                }
            }
            else if(pwLabel1.Visible == false || pwLabel2.Visible == false)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.", "알림");
                textBoxPW1.Focus();
            }
        }

        private void textBoxPW1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPW1.Text.Length >= 4)
            {
                pwLabel1.Visible = true;
            }
            else if (textBoxPW1.Text.Length < 4)
            {
                pwLabel1.Visible = false;
            }
            if (textBoxPW2.Text == textBoxPW1.Text && textBoxPW2.Text.Length >= 4)
            {
                pwLabel2.Visible = true;
            }
            else if (textBoxPW2.Text != textBoxPW1.Text || textBoxPW2.Text.Length < 4)
            {
                pwLabel2.Visible = false;
            }
        }

        private void textBoxPW2_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPW2.Text == textBoxPW1.Text)
            {
                pwLabel2.Visible = true;
            }
            else if(textBoxPW2.Text != textBoxPW1.Text)
            {
                pwLabel2.Visible = false;
            }
        }

        private void textBoxPW1_Enter(object sender, EventArgs e)
        {
            textBoxPW1.SelectAll();
        }

        private void textBoxPW2_Enter(object sender, EventArgs e)
        {
            textBoxPW2.SelectAll();
        }
    }
}
