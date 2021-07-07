using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Spire.Xls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Xml;

namespace hospi_hospital_only
{
    public partial class Prescription : Form
    {
        
        DBClass dbc = new DBClass();
        Security security = new Security();

        DataRow locationRow;
        // 환자정보
        string patientID;
        string receptionTime;
        string receptionDate;
        string patient;
        string patientAge;
        string subjectName;
        string item_ID;
        string period;
        string dosage;
        string count;
        int prescriptionType;

        public int PrescriptionType
        {
            get { return prescriptionType; }
            set { prescriptionType = value; }
        }
        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
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
        public string Patient
        {
            get { return patient; }
            set { patient = value; }
        }
        public string PatientAge
        {
            get { return patientAge; }
            set { patientAge = value; }
        }
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }

        public Prescription()
        {
            InitializeComponent();
        }

        [STAThread]
        // 환자정보 오픈
        public void PatientInfo(int patientNum)
        {
            dbc.PatientInfo(patientNum);
            dbc.VisitorTable = dbc.DS.Tables["visitor"];
        }

        private void Prescription_Load(object sender, EventArgs e)
        {
            // 베이스파일, 디렉토리 없으면 생성해줌
            DirectoryInfo baseFile = new DirectoryInfo(@Properties.Resources.baseFileLocation);
            if (baseFile.Exists == false)
            {
                Directory.CreateDirectory(@Properties.Resources.baseFileLocation);
                byte[] baseFileResource = Properties.Resources.BaseFileResource;
                string savePath = @Properties.Resources.baseFile;
                System.IO.File.WriteAllBytes(savePath, baseFileResource);
            }

            dbc.Select_Prescription(patientID, receptionDate, receptionTime);
            dbc.PrescriptionTable = dbc.DS.Tables["prescription"];
            DBGrid.DataSource = dbc.PrescriptionTable.DefaultView;

            // 환자DB 열기
            PatientInfo(Convert.ToInt32(patientID));

            // 환자정보, 진료정보 그룹박스
            patientName.Text = patient;
            textBoxChartNum.Text = patientID;
            textBoxAge.Text = patientAge + "세";
            textBoxSubject.Text = subjectName;
            textBoxReceptionDate.Text = "20" + receptionDate;
            textBoxHour.Text = receptionTime.Substring(0, 2);
            textBoxMinute.Text = receptionTime.Substring(2, 2);


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

            DBGrid.Columns[0].HeaderText = "약품명";
            DBGrid.Columns[1].HeaderText = "투약일 수";
            DBGrid.Columns[2].HeaderText = "1일 투약 량";
            DBGrid.Columns[3].HeaderText = "1회 투약 량";
            DBGrid.Columns[0].Width = 226;
            DBGrid.Columns[1].Width = 80;
            DBGrid.Columns[2].Width = 80;
            DBGrid.Columns[3].Width = 80;

            if (DBGrid.Rows.Count == 0)
            {
                button1.Text = "수 납";
            }

            if (prescriptionType == 2)
            {
                if(button1.Text == "수 납")
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                    checkBox1.Checked = false;
                    checkBox1.Enabled = false;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(DBGrid.Rows.Count == 0)
            {
                MessageBox.Show("처방정보가 존재하지 않습니다. 수납 화면으로 진행됩니다.","알림");
                Payment payment = new Payment();
                payment.PatientID = textBoxChartNum.Text;
                payment.PatientName = patientName.Text;
                payment.SubjectName = textBoxSubject.Text;
                payment.ReceptionDate = textBoxReceptionDate.Text.Substring(2, textBoxReceptionDate.Text.Length - 2);
                payment.ReceptionTime = textBoxHour.Text + textBoxMinute.Text;
                payment.ShowDialog();

                Dispose();
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(@Properties.Resources.saveLocation);
                if (di.Exists == true)
                {
                    Directory.Delete(@Properties.Resources.saveLocation, true);
                }
                DirectoryInfo dir = new DirectoryInfo(@Properties.Resources.saveLocation);
                if (dir.Exists == false)
                {
                    Directory.CreateDirectory(@Properties.Resources.saveLocation);
                }
                string date = textBoxReceptionDate.Text.Substring(0, 4) + textBoxReceptionDate.Text.Substring(5, 2) + textBoxReceptionDate.Text.Substring(8, 2);
                int patientN = Convert.ToInt32(dbc.VisitorTable.Rows[0]["patientID"]);
                string patientID = patientN.ToString("000");
                // 베이스파일 저장경로
                string path1 = @Properties.Resources.baseFile;
                // excel, pdf 저장경로
                string path2 = @Properties.Resources.saveLocation + date + patientID + ".xls";
                string path3 = @Properties.Resources.saveLocation + date + patientID + " " + patientName.Text + ".pdf";
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook wb = null;
                Excel.Worksheet ws = null;

                try
                {
                    wb = excelApp.Workbooks.Open(path1);
                    ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                    ws.Cells[5, 6] = "  " + date.Substring(0, 4) + "년 " + date.Substring(4, 2) + "월 " + date.Substring(6, 2) + "일   제 " + patientID + " 호";
                    ws.Cells[7, 8] = "  " + dbc.VisitorTable.Rows[0]["patientName"].ToString();
                    ws.Cells[8, 8] = "  " + dbc.VisitorTable.Rows[0]["PatientBirthCode"].ToString().Substring(0, 8) + security.AESDecrypt128(dbc.VisitorTable.Rows[0]["PatientBirthCode"].ToString().Substring(8), DBClass.hospiPW);
                    ws.Cells[5, 25] = "  " + dbc.Hospiname;
                    ws.Cells[6, 25] = "  " + dbc.HospiTell;
                    ws.Cells[7, 25] = "  " + dbc.HospiTell;
                    ws.Cells[22, 5] = "교부일로부터 (    7    )일간";

                    for (int i = 0; i < DBGrid.Rows.Count; i++)
                    {
                        string mediName = "  " + DBGrid.Rows[i].Cells[0].FormattedValue.ToString();
                        for (int k = 0; k < mediName.Length; k++)
                        {
                            if (mediName.Substring(k, 1) == "(")
                            {
                                mediName = mediName.Substring(0, k);
                            }
                        }
                        ws.Cells[10 + i, 1] = mediName;
                        ws.Cells[10 + i, 17] = "  " + DBGrid.Rows[i].Cells[1].FormattedValue.ToString();
                        ws.Cells[10 + i, 20] = "  " + DBGrid.Rows[i].Cells[2].FormattedValue.ToString();
                        ws.Cells[10 + i, 23] = "  " + DBGrid.Rows[i].Cells[3].FormattedValue.ToString();
                    }

                    ws.SaveAs(path2);
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(path2, ExcelVersion.Version2010);
                    workbook.SaveToFile(path3, Spire.Xls.FileFormat.PDF);

                    File.Exists(path1);
                    File.Exists(path2);
                    wb.Close(false);
                    excelApp.Quit();
                    File.Delete(path2);
                    System.Diagnostics.Process.Start(path3);

                    if(prescriptionType == 1)
                    {
                        if (checkBox1.Checked == true)
                        {
                            Payment payment = new Payment();
                            payment.PatientID = textBoxChartNum.Text;
                            payment.PatientName = patientName.Text;
                            payment.SubjectName = textBoxSubject.Text;
                            payment.ReceptionDate = textBoxReceptionDate.Text.Substring(2, textBoxReceptionDate.Text.Length - 2);
                            payment.ReceptionTime = textBoxHour.Text + textBoxMinute.Text;
                            payment.ShowDialog();
                        }
                    }
                    Dispose();

                }
                catch
                {
                    MessageBox.Show("오류");
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

    }
}
