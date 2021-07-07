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
    public partial class MasterDelete : Form
    {
        DBClass dbc = new DBClass();
        DataRow delRow;
        int comboBoxIndex;

        public MasterDelete()
        {
            InitializeComponent();
        }

        private void MasterDelete_Load(object sender, EventArgs e)
        {
            dbc.Master_Open();
            dbc.MasterTable = dbc.DS.Tables["master"];

            for (int i = 1; i < dbc.MasterTable.Rows.Count; i++)
            {
                comboBox1.Items.Add(dbc.MasterTable.Rows[i]["masterName"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 관리자 삭제 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != textBoxName.Text)
            {
                MessageBox.Show("관리자명 확인값이 일치하지 않습니다.", "알림");
            }
            else if (comboBox1.Text == textBoxName.Text)
            {
                DialogResult ok = MessageBox.Show("삭제한 정보는 되돌릴 수 없습니다.\r\n관리자  '" + comboBox1.Text + "' 의 정보를 삭제하시겠습니까?", "알림", MessageBoxButtons.YesNo);

                if (ok == DialogResult.Yes)
                {
                    DataColumn[] PrimaryKey = new DataColumn[1];
                    PrimaryKey[0] = dbc.MasterTable.Columns["masterID"];
                    dbc.MasterTable.PrimaryKey = PrimaryKey;
                    delRow = dbc.MasterTable.Rows.Find(comboBoxIndex);
                    int rowCount = dbc.MasterTable.Rows.Count;  // 삭제전 전체 행의 개수
                    delRow.Delete();
                    int select = comboBoxIndex + 1;     // rowCount를 아래 for문에서 증감시킬경우 정상적으로 반복문이 실행되지 않기 때문에 별도 변수 지정

                    for (int i = 1; i < (rowCount - comboBoxIndex); i++)
                    {
                        delRow = dbc.MasterTable.Rows[rowCount - (rowCount - select)];
                        delRow.BeginEdit();
                        delRow["masterID"] = Convert.ToInt32(delRow["masterID"]) - 1;
                        delRow.EndEdit();
                        select += 1;
                    }

                    dbc.DBAdapter.Update(dbc.DS, "master");
                    dbc.DS.AcceptChanges();

                    MessageBox.Show("삭제가 완료되었습니다.", "알림");
                    Dispose();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxIndex = comboBox1.SelectedIndex + 1;
        }

        // 콤보박스 문자 입력 막기
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'º')
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }
    }
}
