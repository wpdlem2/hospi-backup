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
    public partial class Payment : Form
    {
        DBClass dbc = new DBClass();
        PrescriptionList prescription = new PrescriptionList();
        ReceptionList reception = new ReceptionList();
        Reserve reserve = new Reserve();

        string patientID;
        string patientName;
        string subjectName;
        string hospitalID;
        string receptionDate;
        string receptionTime;

        string mobileID;
        bool mobileUse;

        List<string> Medicine = new List<string>();

        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }
        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public string ReceptionTime
        {
            get { return receptionTime; }
            set { receptionTime = value; }
        }
        public string ReceptionDate
        {
            get { return receptionDate; }
            set { receptionDate = value; }
        }
        
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            prescription.FireConnect();
            reception.FireConnect();
            dbc.Delay(200);
            textBoxChartNum.Text = patientID;
            textBoxPatientName.Text = patientName;
            textBoxSubject.Text = subjectName;

            dbc.FirstReception(Convert.ToInt32(patientID));
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            if(dbc.ReceptionTable.Rows.Count == 1)
            {
                textBoxType.Text = "초진";
            }
            else if (dbc.ReceptionTable.Rows.Count != 1)
            {
                textBoxType.Text = "재진";
            }
            // 병의원타입 추가
            dbc.FireConnect();
            dbc.Hospital_Open(hospitalID);
            if(DBClass.hospikind == "대학")
            {
                textBoxHospiKind.Text = "대학 병원";
                textBox3.Text = "15000";
            }
            else if(DBClass.hospikind == "종합")
            {
                textBoxHospiKind.Text = "종합 병원";
                textBox3.Text = "10000";
            }
            else if(DBClass.hospikind == "의원")
            {
                textBoxHospiKind.Text = "의원";
                textBox3.Text = "5000";
            }
            textBox4.Text = (Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text)).ToString();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void te(object sender, EventArgs e)
        {

        }

        // 카드
        private void button2_Click(object sender, EventArgs e)
        {
            

            DialogResult ok = MessageBox.Show("카드 결제는 별도의 단말에서 진행해주세요. \r\n결제완료 처리 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (ok == DialogResult.Yes)
            {
                dbc.Reception_Date(receptionDate, receptionTime, patientID);
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                DataRow upRow = dbc.ReceptionTable.Rows[0];

                upRow.BeginEdit();
                upRow["payment"] = "카드";
                upRow["price"] = textBox4.Text;
                upRow["receptionType"] = 3;
                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "reception");
                dbc.DS.AcceptChanges();

                PayEnd();
                Dispose();
            }
        }

        // 현금
        private void button1_Click(object sender, EventArgs e)
        {
            

            DialogResult ok = MessageBox.Show("현금으로 결제완료 처리 하시겠습니까?.", "알림", MessageBoxButtons.YesNo);
            if (ok == DialogResult.Yes)
            {

                dbc.Reception_Date(receptionDate, receptionTime, patientID);
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                DataRow upRow = dbc.ReceptionTable.Rows[0];

                upRow.BeginEdit();
                upRow["payment"] = "현금";
                upRow["price"] = textBox4.Text;
                upRow["receptionType"] = 3;
                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "reception");
                dbc.DS.AcceptChanges();

                PayEnd();
                Dispose();
            }
        }

        //수납 완료 시 파이어스토어 처방 등록
        private void PayEnd()
        {
            Medicine.Clear();
            dbc.Mobile_Use(Convert.ToInt32(patientID));
            dbc.MobileTable = dbc.DS.Tables["Visitor"];
            if (dbc.MobileTable.Rows[0][0].ToString() != "")
            {
                mobileUse = true;
                mobileID = dbc.MobileTable.Rows[0][0].ToString();
            }
            else if (dbc.MobileTable.Rows[0][0].ToString() == "")
            {
                mobileUse = false;
            }
            if (mobileUse == true)
            {
                
                dbc.FindOpinion(Convert.ToInt32(patientID), receptionDate, receptionTime);
                dbc.PrescriptionTable = dbc.DS.Tables["Prescription"];
                for (int i = 0; i < dbc.PrescriptionTable.Rows.Count; i++)
                {
                    Medicine.Add(dbc.PrescriptionTable.Rows[i][1].ToString());
                }
                if (dbc.PrescriptionTable.Rows.Count == 0)
                {
                    prescription.PrescriptionAdd(subjectName, dbc.MobileTable.Rows[0][0].ToString(), "등록된 의사 소견이 없습니다.", null);
                }
                else if (dbc.PrescriptionTable.Rows.Count != 0)
                {
                    prescription.PrescriptionAdd(subjectName, dbc.MobileTable.Rows[0][0].ToString(), dbc.PrescriptionTable.Rows[0][0].ToString(), Medicine);
                }
                dbc.Delay(200);
                

            }
        }
    }
}
