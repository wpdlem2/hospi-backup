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
    public partial class UpdateReceptionist : Form
    {
        DBClass dbc = new DBClass();
        string selectedString; // 접수자 삭제, 복구시 선택한 접수자명 받아옴

        public UpdateReceptionist()
        {
            InitializeComponent();
        }

        // 리스트박스 새로고침
        public void RefreshListBox()
        {
            listBoxReceptionist.Items.Clear();
            for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;

                if (name.Substring(length - 1) != ")")
                {
                    listBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }

            listBoxDelete.Items.Clear();
            for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;

                if (name.Substring(length - 1) == ")")
                {
                    listBoxDelete.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }

            for(int i=0; i<listBoxReceptionist.Items.Count; i++)
            {
                if(listBoxReceptionist.Items[i].ToString() == selectedString)
                {
                    listBoxReceptionist.SelectedIndex = i;
                }
            }

            for (int i = 0; i < listBoxDelete.Items.Count; i++)
            {
                if (listBoxDelete.Items[i].ToString() == selectedString)
                {
                    listBoxDelete.SelectedIndex = i;
                }
            }
            textBoxName2.Clear();
            textBoxName.Clear();
        }

        // 폼 로드
        private void UpdateReceptionist_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                    int length = name.Length;

                    if(name.Substring(length - 1) != ")")
                    {
                        listBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                    }
                }
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 종료버튼
        private void buttonFinish_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 추가 버튼
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("접수자명이 입력되지 않았습니다.", "알림");
            }
            else
            {
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
                for (int i = 0; i < listBoxReceptionist.Items.Count; i++)
                {
                    if (listBoxReceptionist.Items[i].ToString() == textBoxName.Text)
                    {
                        MessageBox.Show(textBoxName.Text + "는 이미 존재합니다.", "알림");
                        return;
                    }
                }
                DialogResult ok = MessageBox.Show("접수자 '" + textBoxName.Text + "' 을/를 등록합니다.", "알림", MessageBoxButtons.YesNo);

                if(ok == DialogResult.Yes)
                {
                    try
                    {
                        DataRow newRow = dbc.ReceptionistTable.NewRow();
                        newRow["receptionistCode"] = dbc.ReceptionistTable.Rows.Count+  1;
                        newRow["receptionistName"] = textBoxName.Text;
                        dbc.ReceptionistTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "receptionist");
                        dbc.DS.AcceptChanges();

                        listBoxReceptionist.Items.Add(textBoxName.Text);
                        listBoxReceptionist.SelectedIndex = listBoxReceptionist.Items.Count - 1;

                        textBoxName.Clear();
                    }
                    catch (DataException DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                    catch (Exception DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                }
            }
        }

        // 리스트박스 아이템 클릭
        private void listBoxReceptionist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxReceptionist.SelectedIndex != -1)
            {
                textBoxName.Text = listBoxReceptionist.SelectedItem.ToString();
            }
        }

        // 삭제 버튼
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxReceptionist.SelectedIndex == -1)
            {
                MessageBox.Show("삭제할 정보가 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("접수자 '" + listBoxReceptionist.Items[listBoxReceptionist.SelectedIndex].ToString() +"' 을/를 삭제합니다." , "알림", MessageBoxButtons.YesNo);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc.Receptionist_Open();
                        dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
                        DataRow upRow = null;

                        for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                        {
                            if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == listBoxReceptionist.SelectedItem.ToString())
                            {
                                upRow = dbc.ReceptionistTable.Rows[i];
                            }
                        }

                        upRow.BeginEdit();
                        upRow["receptionistName"] = upRow["receptionistName"].ToString() + "(삭제)";
                        upRow.EndEdit();
                        dbc.DBAdapter.Update(dbc.DS, "receptionist");
                        dbc.DS.AcceptChanges();

                        listBoxReceptionist.Items.Clear();
                        for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                        {
                            string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                            int length = name.Length;

                            if (name.Substring(length - 1) != ")")
                            {
                                listBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                            }
                        }
                        RefreshListBox(); // 리스트박스 새로고침
                    }
                    catch (DataException DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                    catch (Exception DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                }
            }
        }

        // 엔터 이벤트
        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(sender, e);
            }
        }

        // 검색 버튼
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int search = 0;

            for(int i=0; i<listBoxReceptionist.Items.Count; i++)
            {
                if (textBoxName.Text == listBoxReceptionist.Items[i].ToString())
                {
                    listBoxReceptionist.SelectedIndex = i;
                    search = 1;
                }
            }
            if(search == 0)
            {
                MessageBox.Show("검색결과 없음,", "알림");
            }
        }

        // 삭제기록 버튼
        private void buttonHistory_Click(object sender, EventArgs e)
        {
            this.Width= 796;
            buttonHistory.Enabled = false;
            if(listBoxDelete.Items.Count == 0)
            {
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];

                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                    int length = name.Length;
                    if (name.Substring(length - 1) == ")")
                    {
                        listBoxDelete.Items.Add(name);
                    }
                }
            }
        }
        // 삭제내역 닫기
        private void button2_Click(object sender, EventArgs e)
        {
            this.Width = 409;
            buttonHistory.Enabled = true;
        }

        // 복구 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBoxDelete.SelectedIndex == -1)
            {
                MessageBox.Show("복구할 정보가 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("접수자 '" + listBoxDelete.Items[listBoxDelete.SelectedIndex].ToString() + "' 을/를 복구합니다.", "알림", MessageBoxButtons.YesNo);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc.Receptionist_Open();
                        dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
                        DataRow upRow = null;

                        for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                        {
                            if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == listBoxDelete.SelectedItem.ToString())
                            {
                                upRow = dbc.ReceptionistTable.Rows[i];
                            }
                        }
                        upRow.BeginEdit();
                        int dLength = Convert.ToInt32(upRow["receptionistName"].ToString().Length);
                        upRow["receptionistName"] = upRow["receptionistName"].ToString().Substring(0, dLength - 4);
                        upRow.EndEdit();
                        dbc.DBAdapter.Update(dbc.DS, "receptionist");
                        dbc.DS.AcceptChanges();

                        RefreshListBox(); // 리스트박스 새로고침

                    }
                    catch (DataException DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                    catch (Exception DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(selectedString);
        }

        // 삭제결과 검색 버튼
        private void buttonSearch2_Click(object sender, EventArgs e)
        {
            int search = 0;

            for (int i = 0; i < listBoxDelete.Items.Count; i++)
            {
                if (textBoxName2.Text + "(삭제)"== listBoxDelete.Items[i].ToString())
                {
                    listBoxDelete.SelectedIndex = i;
                    search = 1;
                }
            }
            if (search == 0)
            {
                MessageBox.Show("검색결과 없음,", "알림");
            }
        }
    }
}
