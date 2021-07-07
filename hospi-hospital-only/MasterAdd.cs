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
    public partial class MasterAdd : Form
    {
        DBClass dbc = new DBClass();

        public MasterAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" && textBoxName.Text == " ")
            {
                for (int i = 0; i < dbc.MasterTable.Rows.Count; i++)
                {
                    if (dbc.MasterTable.Rows[i]["masterName"].ToString() == textBoxName.Text)
                    {
                        MessageBox.Show("관리자명이 중복됩니다. \r\n다른 이름을 입력해주세요.", "알림");
                        textBoxName.Focus();
                        return;
                    }
                }
                MessageBox.Show("사용 가능한 관리자명입니다.", "알림");
                buttonCheck.Enabled = false;
                textBoxPW1.Focus();
            }
            else
            {
                MessageBox.Show("관리자명을 입력해주세요", "알림");
            }
        }

        private void MasterAdd_Load(object sender, EventArgs e)
        {
            dbc.Master_Open();
            dbc.MasterTable = dbc.DS.Tables["master"];
        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            textBoxName.SelectAll();
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
            if (textBoxPW2.Text == textBoxPW1.Text)
            {
                pwLabel2.Visible = true;
            }
            else if (textBoxPW2.Text != textBoxPW1.Text)
            {
                pwLabel2.Visible = false;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonCheck.Enabled = true;
        }

        // 완료 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if (buttonCheck.Enabled == true)
            {
                MessageBox.Show("관리자명 중복확인을 먼저 진행해주세요.", "알림:");
            }
            else
            {
                if (pwLabel1.Visible == true && pwLabel2.Visible == true)
                {
                    DialogResult ok = MessageBox.Show("신규 관리자를 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ok == DialogResult.Yes)
                    {
                        DataRow newRow = dbc.MasterTable.NewRow();
                        newRow["MasterID"] = dbc.MasterTable.Rows.Count;
                        newRow["MasterName"] = textBoxName.Text;
                        newRow["MasterPassword"] = textBoxPW1.Text;

                        dbc.MasterTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "Master");
                        dbc.DS.AcceptChanges();

                        MessageBox.Show("관리자명 : " + textBoxName.Text + "\r\n등록이 완료되었습니다.", "알림");
                        Dispose();
                    }
                }
            }
        }
    }
}
