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
    public partial class ReceptionUpdate : Form
    {
        DBClass dbc = new DBClass();
        int receptionID; //  접수번호
        string selectedSybjectName;

        public int ReceptionID
        {
            get { return receptionID; }
            set { receptionID = value; }
        }
        public string SelectedSubjectName
        {
            get { return selectedSybjectName; }
            set { selectedSybjectName = value; }
        }

        public ReceptionUpdate()
        {
            InitializeComponent();
        }

        // 폼 로드
        private void ReceptionUpdate_Load(object sender, EventArgs e)
        {
            // 폼 로드시 포커스
            this.ActiveControl = button1;

            // 접수DB
            dbc.Reception_Open();
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            DataRow row = dbc.ReceptionTable.Rows[receptionID-1];
            dateTimePicker1.Text = "20" + row["receptionDate"].ToString();
            comboBoxTime1.Text = row["receptionTime"].ToString().Substring(0, 2);
            comboBoxTime2.Text = row["receptionTime"].ToString().Substring(2, 2);
            textBoxChartNum.Text = row["patientID"].ToString();
            comboBoxSubjcet.Text = DBClass.hospidepartment[0];
            comboBoxReceptionist.Text = row["receptionistCode"].ToString();
            // comboBoxReceptionist에 접수자명 추가
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            for(int i =0; i<dbc.ReceptionistTable.Rows.Count; i++)
            {
                comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
            }
            if(row["receptionType"].ToString() == "1")
            {
                textBoxReceptionType.Text = "진료 대기중";
            }else if(row["receptionType"].ToString() == "4")
            {
                textBoxReceptionType.Text = "진료 보류중";
            }

            // 환자DB
            dbc.Visitor_Open();
            dbc.VisitorTable = dbc.DS.Tables["visitor"];
            row = dbc.VisitorTable.Rows[Convert.ToInt32(textBoxChartNum.Text) - 1];
            patientName.Text = row["patientName"].ToString();

            // 과목DB
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            comboBoxSubjcet.Text = selectedSybjectName;
            
            for (int i = 0; i < DBClass.hospidepartment.Length; i++)  
            {
                comboBoxSubjcet.Items.Add(DBClass.hospidepartment[i]);
            }

            // 접수자DB
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            row = dbc.ReceptionistTable.Rows[Convert.ToInt32(comboBoxReceptionist.Text) - 1];
            comboBoxReceptionist.Text = row["receptionistName"].ToString();
            // comboBoxReceptionist에 접수자명 추가
            for (int i = 0; i < dbc.ReceptionistTable.Columns.Count; i++)  
            {
                comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i][1]);
            }
        }

        // 취소버튼
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 수정완료 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                DataRow upRow = dbc.ReceptionTable.Rows[receptionID - 1];
                upRow.BeginEdit();
                upRow["receptionDate"] = dateTimePicker1.Value.ToString("yy-MM-dd");
                upRow["receptionTime"] = comboBoxTime1.Text + comboBoxTime2.Text;
                for (int i = 0; i < dbc.SubjectTable.Rows.Count; i++)
                {
                    if (dbc.SubjectTable.Rows[i]["subjectName"].ToString() == comboBoxSubjcet.Text)
                    {
                        upRow["subjectName"] = comboBoxSubjcet.Text;
                    }
                }
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == comboBoxReceptionist.Text)
                    {
                        upRow["receptionistCode"] = dbc.ReceptionistTable.Rows[i]["receptionistCode"];
                    }
                }
                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "reception");
                dbc.DS.AcceptChanges();

                MessageBox.Show("수정이 완료되었습니다.", "알림");
                Dispose();
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

        private void comboBoxSubjcet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxReceptionist_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxTime2_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxTime2.Text != "")
            {
                if (Convert.ToInt32(comboBoxTime2.Text) > 59)
                {
                    comboBoxTime2.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBoxTime1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxTime1.Text != "")
            {
                if (Convert.ToInt32(comboBoxTime1.Text) > 23)
                {
                    comboBoxTime1.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }
    }
}
