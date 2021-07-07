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
    public partial class PatientSetting : Form
    {
        DBClass dbc = new DBClass();
        Security security = new Security();


        private int old;

        public PatientSetting()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (patientName.Text == "")
            {
                MessageBox.Show("수진자명을 입력하세요.", "알림");
                patientName.Focus();
            }
            else
            {
                try
                {
                    DBGrid.Rows.Clear();
                    DBGrid.Columns.Clear();
                    // 재진조회 그룹박스 정보 넣기
                    TextBoxClear();

                    dbc.Visitor_Name(patientName.Text);
                    dbc.VisitorTable = dbc.DS.Tables["visitor"];

                    if (dbc.VisitorTable.Rows.Count == 0)
                    {
                        MessageBox.Show("등록된 정보가 존재하지 않습니다.", "알림");
                        TextBoxClear();
                        patientName.Clear();
                    }

                    DBGrid.Columns.Add("PatientID", "차트번호");
                    DBGrid.Columns.Add("PatientName", "이름");
                    DBGrid.Columns.Add("SecurityNumber", "주민번호");

                    // GirdView 띄우기
                    for (int i = 0; i < dbc.VisitorTable.Rows.Count; i++)
                    {
                        try
                        {
                            DBGrid.Rows.Add(dbc.VisitorTable.Rows[i][0], dbc.VisitorTable.Rows[i][1], dbc.VisitorTable.Rows[i][2].ToString().Substring(0, 8) + security.AESDecrypt128(dbc.VisitorTable.Rows[i][2].ToString().Substring(8), DBClass.hospiPW));
                        }
                        catch
                        {
                            DBGrid.Rows.Add(dbc.VisitorTable.Rows[i][0], dbc.VisitorTable.Rows[i][1], dbc.VisitorTable.Rows[i][2].ToString().Substring(0, 8));
                        }
                    }

                    // GirdView 속성 ▼
                    DBGrid.CurrentCell = null; // 로딩시 첫번째열 자동선택 없애기 
                                               // 색상변경
                    for (int i = 1; i < DBGrid.Rows.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            DBGrid.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240);
                        }
                        else
                        {
                            DBGrid.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    // 정렬 막기
                    foreach (DataGridViewColumn item in DBGrid.Columns)
                    {
                        item.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    // 생년월일 텍스트박스 포커스
                    if (dbc.VisitorTable.Rows.Count != 0 || dbc.VisitorTable.Rows.Count == 1)
                    {
                        patientBirth.Focus();
                    }
                }
                catch (DataException DE)
                {
                    MessageBox.Show(DE.Message);
                }
            }
        }
        public void TextBoxClear()
        {
            // 재진조회
            textBoxChartNum.Clear();
            textBoxB1.Clear();
            textBoxB2.Clear();
            phone1.Clear();
            phone2.Clear();
            phone3.Clear();
            textBoxADDR.Clear();
            labelGenderAge.Text = "성별/나이";
            // 출생년도
            patientBirth.Clear();
        }

        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                TextBoxClear();
                patientName.Text = DBGrid.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                // 재진조회 그룹박스, 특이사항 정보 넣기
                //VisitorText(Convert.ToInt32(DBGrid.Rows[e.RowIndex].Cells[1].FormattedValue));
                VisitorText(e.RowIndex);

                // 성별/나이 라벨 수정

                GenderAgeLabel();
            }
        }


        // 성별/나이 라벨 수정
        public void GenderAgeLabel()
        {
            String year = DateTime.Now.ToString("yyyy");
            if (textBoxB2.Text.Substring(0, 1) == "1" || textBoxB2.Text.Substring(0, 1) == "2")
            {
                old = Convert.ToInt32(year) - Convert.ToInt32(textBoxB1.Text.Substring(0, 2)) - 1899;
            }
            else if (textBoxB2.Text.Substring(0, 1) == "3" || textBoxB2.Text.Substring(0, 1) == "4")
            {
                old = Convert.ToInt32(year) - Convert.ToInt32(textBoxB1.Text.Substring(0, 2)) - 1999;
            }

            if (textBoxB2.Text.Substring(0, 1) == "1" || textBoxB2.Text.Substring(0, 1) == "3")
            {
                labelGenderAge.Text = "남/" + old.ToString() + "세";
            }
            else if (textBoxB2.Text.Substring(0, 1) == "2" || textBoxB2.Text.Substring(0, 1) == "4")
            {
                labelGenderAge.Text = "여/" + old.ToString() + "세";
            }
        }

        // 수진자 정보 넣기
        public void VisitorText(int rows)
        {
            try
            {
                DataRow vRow = dbc.VisitorTable.Rows[rows];
                textBoxChartNum.Text = vRow["patientID"].ToString();
                textBoxB1.Text = vRow["patientBirthCode"].ToString().Substring(0, 6);
                if (vRow["patientBirthCode"].ToString().Length > 9)
                {
                    textBoxB2.Text = vRow["patientBirthCode"].ToString().Substring(7, 1) + security.AESDecrypt128(vRow["patientBirthCode"].ToString().Substring(8), DBClass.hospiPW);
                }
                else
                {
                    textBoxB2.Text = vRow["patientBirthCode"].ToString().Substring(7, 1);
                }
                phone1.Text = vRow["patientPhone"].ToString().Substring(0, 3);
                phone2.Text = vRow["patientPhone"].ToString().Substring(3, 4);
                phone3.Text = vRow["patientPhone"].ToString().Substring(7, 4);
                textBoxADDR.Text = vRow["patientAddress"].ToString();
                
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (patientBirth.Text == "" || patientBirth.Text == " ")
            {
                MessageBox.Show("생년월일을 입력해주세요", "알림");
            }
            else
            {
                try
                {
                        String searchValue = patientBirth.Text;
                        int rowIndex = -1;
                        foreach (DataGridViewRow row in DBGrid.Rows)
                        {
                            if (row.Cells[2].Value.ToString().Substring(0, 6).Equals(searchValue))
                            {
                                rowIndex = row.Index;
                                break;
                            }
                            else if (row.Cells[2].Value.ToString().Substring(0, 2).Equals(searchValue))
                            {
                                rowIndex = row.Index;
                                break;
                            }
                        }
                        DBGrid.Rows[rowIndex].Selected = true;

                }
                catch
                {

                } 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxB1.Text.Length == 6 && textBoxB2.Text.Length == 7)
            {
                dbc.Visitor_Select(textBoxChartNum.Text);
                dbc.VisitorTable = dbc.DS.Tables["visitor"];
                DataRow upRow = dbc.VisitorTable.Rows[0];
                upRow.BeginEdit();
                upRow["PatientName"] = patientName.Text;
                upRow["PatientBirthCode"] = textBoxB1.Text + "-" + textBoxB2.Text.Substring(0, 1) + security.AESEncrypt128(textBoxB2.Text.Substring(1, 6), DBClass.hospiPW);

                upRow["PatientPhone"] = phone1.Text + phone2.Text + phone3.Text;
                upRow["PatientAddress"] = textBoxADDR.Text;

                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "visitor");
                dbc.DS.AcceptChanges();


                MessageBox.Show("변경이 완료되었습니다.", "알림");
                Dispose();
            }
            else if (textBoxB1.Text.Length != 6 && textBoxB2.Text.Length != 7)
            {
                MessageBox.Show("주민등록번호를 확인해주세요.", "알림");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button9_Click(sender, e);
            }
            
        }

        private void PatientSetting_Load(object sender, EventArgs e)
        {
            patientName.Focus();
        }
    }
}
