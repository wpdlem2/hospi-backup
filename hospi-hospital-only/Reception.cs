using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.ViewManagement;

namespace hospi_hospital_only
{
    public partial class Reception : Form
    {
        DBClass dbc = new DBClass(); // hospital, visitor
        DBClass dbc2 = new DBClass(); // reception, receptionist
        DBClass dbc3 = new DBClass(); // reception 조회 삭제
        DBClass dbc4 = new DBClass();
        DBClass dbc5 = new DBClass();   // timer 에서 사용
        
        Inquiry inquiry = new Inquiry();
        Reserve reserve = new Reserve();
        ReceptionList receptionlist = new ReceptionList();
        PrescriptionList prescriptionlist = new PrescriptionList();
        Security security = new Security();
        
        string listViewIndexID1; // 접수 현황 리스트뷰 아이템 클릭시 해당정보의 receptionID를 저장하는 변수
        string listViewIndexID2; // 수납 현황 리스트뷰 아이템 클릭시 해당정보의 receptionID를 저장하는 변수
        string messageTypeL, messageTypeR; // 진료보류, 접수복구    수납대기, 수납취소 메시지를 담기 위한 변수
        string listViewIndexPatientNameL, listViewIndexPatientNameR; // 리스트뷰 아이템 클릭시 해당정보의 PatientName을 저장하는 변수 // L(진료현황) R(수납현황)
        int listViewModeL, listViewModeR; // 리스트뷰의 현재상태를 저장한 변수     // L ( 진료대기 : 1 , 진료보류 : 2 )      // R ( 수납대기 : 1 , 수납완료 : 2 ) 
        string hospitalID;
        int old;
        string date; // 날짜 변수
        string[] prescriptionArr = new string[5]; // 처방전 조회에 필요한 (patientID, receptionTime, receptionDate 저장)
        DataTable hisTable; // 수진자 정보 조회시 이전 진료기록을 담은 테이블 ( 이전 진료기록 띄울때 사용하고, 이전 진료기록중 내원목적 확인시에 재사용 )
        int selectedListViewItemIndex; // 이전 진료기록 리스트뷰의 선택 인덱스 저장
        string selectedSubjectName; // 접수 수정시 과목명 저장
        string receptionistName; // MainMenu에서 접수자명 받아옴
        public static bool pushAlim;
        int incount;
        int listView3SelectedRow;
        int SelectRow=0; //접수현황 리스트뷰 선택인덱스
        string[] prescription;
        int waitingIsNull = 0;
        int acceptReserve = 0;
        int prescriptionType = 0; // 1:수납대기 상태에서 2:수납완료 상태에서
        int updatewait = 0;
        bool patientSearch = false;

        public Reception()
        {
            InitializeComponent();
        }

        // 프로퍼티 
        public string HospitalID // Main폼에서 입력된 병원코드를 받아옴
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public string ReceptionistName
        {
            get { return receptionistName; }
            set { receptionistName = value; }
        }

        // 진료정보의 접수일, 접수시간 초기화 & 현재 체크박스 체크
        public void TimeNow()
        {
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            comboBoxTime1.Text = DateTime.Now.ToString("HH");
            comboBoxTime2.Text = DateTime.Now.ToString("mm");
            checkBox2.Checked = true;
        }

        // 접수 현황 버튼 ▶◀ 삭제
        public void ButtonClearL()
        {
            button2.Text = "진료대기";
            button5.Text = "진료보류";
        }

        // 수납 현황 버튼 ▶◀ 삭제
        public void ButtonClearR()
        {
            button8.Text = "수납대기";
            button13.Text = "수납완료";
        }

        // 재진조회, 진료정보 텍스트박스 비우기
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
            textBox16.Clear();
            // 이전진료
            listView2.Items.Clear();
            // 진료정보
            textBoxPurpose.Clear();
            // 내원정보
            textBoxFirst.Clear();
            textBoxLast.Clear();
            // 출생년도
            patientBirth.Clear();
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

        // 접수 리스트뷰 ( listView1,3 )
        public void ReceptionUpdate(int receptionType)
        {
            try
            {
                date = dateTimePicker2.Value.ToString("yy-MM-dd");
                dbc3.Reception_Update(date, receptionType); // 타입 4,5 함수 추가
                dbc3.ReceptionTable = dbc3.DS.Tables["Reception"];

                if (receptionType == 1 || receptionType == 4)
                {
                    listView1.Items.Clear();
                }
                else if (receptionType == 2 || receptionType == 3)
                {
                    listView3.Items.Clear();
                }

                if(receptionType == 3)
                {
                    for(int i = dbc3.ReceptionTable.Rows.Count-1; i>=0; i--)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = (listView3.Items.Count + 1).ToString();
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][0].ToString().Substring(0, 2) + " : " + dbc3.ReceptionTable.Rows[i][0].ToString().Substring(2, 2));
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][1].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][2].ToString());
                        // AGE
                        int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));



                        if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "1" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "2" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "0")
                        {
                            item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1899).ToString());
                        }
                        else if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "3" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "4" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "5")
                        {
                            item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1999).ToString());
                        }

                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][4].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][5].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][6].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][7].ToString());

                        listView3.Items.Add(item);
                        dbc2.Reception_Update(date, 2);
                        dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                        receptionCount2.Text = "수납대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                        dbc2.Reception_Update(date, 3);
                        dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                        receptionCount2.Text = receptionCount2.Text + "\r\n수납완료 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                    }
                }
                else
                {
                    for (int i = 0; i < dbc3.ReceptionTable.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem();
                        if (receptionType == 1 || receptionType == 4)
                        {
                            item.Text = (listView1.Items.Count + 1).ToString();
                        }
                        else if (receptionType == 2)
                        {
                            item.Text = (listView3.Items.Count + 1).ToString();
                        }
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][0].ToString().Substring(0, 2) + " : " + dbc3.ReceptionTable.Rows[i][0].ToString().Substring(2, 2));
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][1].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][2].ToString());
                        // Age
                        int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));


                        if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "1" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "2" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "0")
                        {
                            item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1899).ToString());
                        }
                        else if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "3" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "4" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "5")
                        {
                            item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1999).ToString());
                        }


                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][4].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][5].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][6].ToString());
                        item.SubItems.Add(dbc3.ReceptionTable.Rows[i][7].ToString());
                        if (receptionType == 1 || receptionType == 4)
                        {
                            listView1.Items.Add(item);
                            dbc2.Reception_Update(date, 1);
                            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                            receptionCount1.Text = "진료대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                            dbc2.Reception_Update(date, 4);
                            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                            receptionCount1.Text = receptionCount1.Text + "\r\n진료보류 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                        }
                        else if (receptionType == 2 || receptionType == 3)
                        {
                            listView3.Items.Add(item);
                            dbc2.Reception_Update(date, 2);
                            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                            receptionCount2.Text = "수납대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                            dbc2.Reception_Update(date, 3);
                            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                            receptionCount2.Text = receptionCount2.Text + "\r\n수납완료 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                        }
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

        // 수진자 정보 넣기
        public void VisitorText(int rows)
        {
            try
            {
                DataRow vRow = dbc.VisitorTable.Rows[rows];
                textBoxChartNum.Text = vRow["patientID"].ToString();
                textBoxB1.Text = vRow["patientBirthCode"].ToString().Substring(0,6);
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
                textBox16.Text = vRow["patientMemo"].ToString();

                // 이전 진료 기록
                dbc.Visitor_Chart(Convert.ToInt32(vRow["patientID"]));
                hisTable = dbc.DS.Tables["visitor"];
                if(hisTable.Rows.Count != 0)
                {
                    for (int i = 0; i < hisTable.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = hisTable.Rows[i]["receptionDate"].ToString();
                        item.SubItems.Add(hisTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + hisTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                        item.SubItems.Add(hisTable.Rows[i]["subjectName"].ToString());
                        item.SubItems.Add(hisTable.Rows[i]["receptionInfo"].ToString());
                        listView2.Items.Add(item);
                    }
                    textBoxFirst.Text = "20" + hisTable.Rows[hisTable.Rows.Count - 1]["receptionDate"].ToString();
                    textBoxLast.Text = "20" + hisTable.Rows[0]["receptionDate"].ToString();
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

        // 종료
        private void button1_Click(object sender, EventArgs e)
        {
            if (updatewait == 1)
            {
                MessageBox.Show("접수 현황을 업데이트하는 중입니다.\n잠시만 기다려 주세요.", "알림");
            }
            else if (updatewait == 0)
            {
                Dispose();
            }
        }

        // 폼 로드
        private void Receipt_Load(object sender, EventArgs e)
        {
            dbc.FireConnect();
            dbc2.FireConnect();
            dbc3.FireConnect();
            inquiry.FireConnect();
            reserve.FireConnect();
            incount = Inquiry.count;
            receptionlist.FireConnect();


            // 폼 로드시 버튼 클릭
            button2_Click(sender, e); // 진료대기버튼
            button8_Click(sender, e); // 진료보류버튼

            // 폼 로드시 수신자명 포커스
            this.ActiveControl = patientName;

            // 접수시간 (현재)
            TimeNow();

            try
            {
                // DB오픈 ( Hospital, SubjectName, Reception ) 
                // 병원정보 가져오고 과목명 comboBox에 추가
                dbc.Reception_Open();
                dbc.Hospital_Open(hospitalID);
                reserve.TodayReserveOpen(hospitalID);
                dbc.Delay(400);
                inquiry.UpdateWait(hospitalID);
                reserve.ReserveCancelWait(hospitalID);
                reserve.ReserveUpdateWait(hospitalID);
                //dbc.HospitalTable = dbc.DS.Tables["hospital"];
                //DataRow subjectRow = dbc.HospitalTable.Rows[0];
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"]; // 접수자 테이블
                dbc.Visitor_Open();
                dbc.VisitorTable = dbc.DS.Tables["Visitor"];
                textBoxReceptionist.Text = receptionistName;
                dbc.Subject_Open();
                dbc.SubjectTable = dbc.DS.Tables["subjectName"]; // 과목 테이블
                for (int i = 0; i < DBClass.hospidepartment.Length; i++)     // comboBoxSubject에 과목명 추가
                {
                    comboBoxSubjcet.Items.Add(DBClass.hospidepartment[i]);
                }
                comboBoxSubjcet.Text = DBClass.hospidepartment[0];    // 최상위 과목명을 기본 텍스트로 지정
                button5_Click(sender, e);
                button2_Click(sender, e);
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }

            timer1.Start();
        }

        // 초진등록
        private void button7_Click(object sender, EventArgs e)
        {
            Reception_First receipt_First = new Reception_First();
            receipt_First.ShowDialog();
            patientName.Text = receipt_First.VisitorName; // 수진자명 받아와서 텍스트 대입
            if (patientName.Text != "")
            {
                button9_Click(sender, e);                                   // 조회 클릭
                VisitorText(0);                                                     // 수진자 정보 넣기
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // (현재) 접수 체크변경
            if (checkBox2.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                comboBoxTime1.Enabled = false;
                comboBoxTime2.Enabled = false;
                timer1.Start();
                TimeNow();
            }
            else if (checkBox2.Checked == false)
            {
                dateTimePicker1.Enabled = true;
                comboBoxTime1.Enabled = true;
                comboBoxTime2.Enabled = true;
                timer1.Stop();
            }
        }

        // 진료대기 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClearL();
            button2.Text = "▶ " + button2.Text + " ◀";
            button12.Enabled = true;
            label7.Visible = false;
            listViewModeL = 1;

            button22.Text = "진료 보류";

            // 접수로드 (1 = 진료대기)
            ReceptionUpdate(1);

            label2.Visible = false;
        }

        // 진료보류 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            ButtonClearL();
            button5.Text = "▶ " + button5.Text + " ◀";
            button12.Enabled = false;
            label7.Visible = true;
            listViewModeL = 2;

            button22.Text = "접수 복구";

            // 접수로드 (4 = 진료보류환자) + 5번 보류환자
            ReceptionUpdate(4);

            for(int i=0; i<listView1.Items.Count; i++)
            {
                if(listView1.Items[i].SubItems[8].Text == "5")
                {
                    listView1.Items[i].BackColor = Color.Yellow;
                }
            }

            label2.Visible = true;
        }

        // 수납대기 버튼
        private void button8_Click(object sender, EventArgs e)
        {
            button15.Text = "수납";
            ButtonClearR();
            button8.Text = "▶ " + button8.Text + " ◀";
            listViewModeR = 1;

            // 접수로드 (2 = 수납대기)
            ReceptionUpdate(2);
            prescriptionType = 1;
        }

        // 수납완료 버튼
        private void button13_Click(object sender, EventArgs e)
        {
            button15.Text = "처방 확인";
            ButtonClearR();
            button13.Text = "▶ " + button13.Text + " ◀";
            listViewModeR = 2;

            // 접수로드 (3 = 수납완료)
            ReceptionUpdate(3);
            prescriptionType = 2;
        }

        // 병원정보설정 버튼
        private void button16_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.HospitalID = hospitalID;
            checkMasterPW.FormNum = 1;
            checkMasterPW.ShowDialog();
        }

        // 접수된 예약 버튼
        private void button10_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();
            reservation.HospitalID = hospitalID;
            reservation.Receptionist = receptionistName;
            reservation.ShowDialog();
        }

        // 수진자 조회 버튼
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
                    DBGrid.Columns[0].Width = 75;
                    DBGrid.Columns[2].Width = 120;

                    // GirdView 띄우기
                    for (int i=0; i<dbc.VisitorTable.Rows.Count; i++)
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
                catch (Exception DE)
                {
                    MessageBox.Show(DE.Message);
                }
            }
        }

        // 수진자 조회 셀 더블클릭    ( 조회된 수진자의 수가 1보다 많을경우 )      
        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                TextBoxClear();
                patientName.Text = DBGrid.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                // 재진조회 그룹박스, 특이사항 정보 넣기
                //VisitorText(Convert.ToInt32(DBGrid.Rows[e.RowIndex].Cells[1].FormattedValue));
                VisitorText(e.RowIndex);

                // 성별/나이 라벨 수정

                GenderAgeLabel();
                textBoxPurpose.Focus();
            }
        }

        // 접수 (접수등록) 버튼
        private void button11_Click(object sender, EventArgs e)
        {
            if (textBoxChartNum.Text == "")
            {
                MessageBox.Show("수진자 정보를 확인하세요.", "알림");
            }
            else if (textBoxPurpose.Text == "")
            {
                MessageBox.Show("내원 목적이 작성되지 않았습니다.", "알림");
            }
            else
            {
                try
                {
                    dbc.Mobile_Visitor(textBoxChartNum.Text);
                    dbc.VisitorTable = dbc.DS.Tables["Visitor"];
                    if (dbc.VisitorTable.Rows[0][0].ToString() == "")
                    {
                        try
                        {
                            dbc.Reception_Open();
                            dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                            DataRow newRow = dbc.ReceptionTable.NewRow();
                            newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                            newRow["PatientID"] = textBoxChartNum.Text;
                            newRow["ReceptionTime"] = comboBoxTime1.Text + comboBoxTime2.Text;
                            newRow["ReceptionDate"] = dateTimePicker1.Value.ToString("yy/MM/dd");
                            newRow["SubjectName"] = comboBoxSubjcet.Text;
                            for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                            {
                                if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == textBoxReceptionist.Text)
                                {
                                    newRow["ReceptionistCode"] = i + 1;
                                }
                            }
                            newRow["ReceptionInfo"] = textBoxPurpose.Text;
                            newRow["ReceptionType"] = 1;

                            dbc.ReceptionTable.Rows.Add(newRow);
                            dbc.DBAdapter.Update(dbc.DS, "Reception");
                            dbc.DS.AcceptChanges();

                            MessageBox.Show("접수 완료.", "알림");
                            TextBoxClear();
                            patientName.Clear();
                            DBGrid.DataSource = null;
                            DBGrid.Rows.Clear();
                            DBGrid.Columns.Clear();
                            patientName.Focus();

                            if (checkBox2.Checked == false)
                            {
                                checkBox2.Checked = true;
                            }

                            // 접수현황 업데이트
                            ReceptionUpdate(1);

                            ReceptionListUpdate(0);


                            comboBoxSubjcet.Text = comboBoxSubjcet.Items[0].ToString();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else if(dbc.VisitorTable.Rows[0][0].ToString() != "")
                    {
                        dbc.FindDoctor(comboBoxSubjcet.Text);
                        dbc.SubjectTable = dbc.DS.Tables["Subject"];
                        dbc.countWaiting(comboBoxSubjcet.Text, comboBoxTime1.Text + comboBoxTime2.Text, DateTime.Now.ToString("yy-MM-dd"));
                        dbc.WaitingTable = dbc.DS.Tables["Reception"];
                        receptionlist.ReceptionAccept(comboBoxSubjcet.Text, dbc.SubjectTable.Rows[0][0].ToString(), dbc.VisitorTable.Rows[0][0].ToString(), patientName.Text, DateTime.Now.ToString("yyyy-MM-dd"), comboBoxTime1.Text + ":" + comboBoxTime2.Text , Convert.ToInt32(dbc.WaitingTable.Rows[0][0]));
                        dbc.Delay(200);
                        dbc.Reception_Open();
                        dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                        DataRow newRow = dbc.ReceptionTable.NewRow();
                        newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                        newRow["PatientID"] = textBoxChartNum.Text;
                        newRow["ReceptionTime"] = comboBoxTime1.Text + comboBoxTime2.Text;
                        newRow["ReceptionDate"] = dateTimePicker1.Value.ToString("yy/MM/dd");
                        newRow["SubjectName"] = comboBoxSubjcet.Text;
                        for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                        {
                            if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == textBoxReceptionist.Text)
                            {
                                newRow["ReceptionistCode"] = i + 1;
                            }
                        }
                        newRow["ReceptionInfo"] = textBoxPurpose.Text;
                        newRow["ReceptionType"] = 1;

                        dbc.ReceptionTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "Reception");
                        dbc.DS.AcceptChanges();


                        MessageBox.Show("접수 완료.", "알림");
                        TextBoxClear();
                        patientName.Clear();
                        comboBoxSubjcet.Text = comboBoxSubjcet.Items[0].ToString();
                        DBGrid.DataSource = null;
                        DBGrid.Rows.Clear();
                        DBGrid.Columns.Clear();

                        patientName.Focus();

                        if (checkBox2.Checked == false)
                        {
                            checkBox2.Checked = true;
                        }

                        // 접수현황 업데이트
                        ReceptionUpdate(1);
                        ReceptionListUpdate(0);
                        DBGrid.DataSource = null;
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
        }

        // 접수자 변경 버튼    
        private void button6_Click(object sender, EventArgs e)
        {
            // 접수자 변경 메뉴 
            Receptionist receptionist = new Receptionist();
            receptionist.ReceptionistName = textBoxReceptionist.Text;
            receptionist.ShowDialog();
            textBoxReceptionist.Text = receptionist.ReceptionistName;

            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
        }

        // 날짜정보 금일 버튼
        private void button21_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
            button21.Enabled = false;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button20_Click(sender, e);
            }
        }

        // 수진자 조회 출생년도 검색
        private void button20_Click(object sender, EventArgs e)
        {

            if (patientBirth.Text == "" || patientBirth.Text == " ")
            {
                MessageBox.Show("생년월일을 입력해주세요.", "알림");
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
                    MessageBox.Show("검색 결과가 없습니다.", "알림");
                }
            }
            
        }

        // 접수현황 수진자명 검색
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems[3].Text == textBox2.Text)
                {
                    listView1.Items[i].BackColor = Color.Yellow;
                    patientSearch = true;
                }
                else
                {
                    listView1.Items[i].BackColor = Color.White;
                }
            }

            if (patientSearch == false)
            {
                MessageBox.Show("검색 결과가 없습니다.", "알림");
            }
            patientSearch = false;
            textBox2.Clear();
        }
        // 접수현황 수진자명 검색 엔터이벤트
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        // 수납현황 수진자명 검색
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView3.Items.Count; i++)
            {
                if (listView3.Items[i].SubItems[3].Text == textBox3.Text)
                {
                    listView3.Items[i].BackColor = Color.Yellow;
                    patientSearch = true;
                }
                else
                {
                    listView3.Items[i].BackColor = Color.White;
                }
            }

            if (patientSearch == false)
            {
                MessageBox.Show("검색 결과가 없습니다.", "알림");
            }
            patientSearch = false;
            textBox3.Clear();
        }
        // 수납현황 수진자명 검색 엔터이벤트
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        // 접수취소 버튼
        private void button12_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("취소할 항목이 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("선택된 접수를 취소합니다.\r\n수진자명 : " + listViewIndexPatientNameL, "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    try
                    {

                        dbc.FindReceptionCode(listView1.Items[SelectRow].SubItems[5].Text, DateTime.Now.ToString("yy-MM-dd"), listView1.Items[SelectRow].SubItems[1].Text.Substring(0, 2) + listView1.Items[SelectRow].SubItems[1].Text.Substring(5, 2));
                        dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                        dbc3.Reception_Open();
                        dbc3.ReceptionTable = dbc3.DS.Tables["reception"];
                        DataColumn[] PrimaryKey = new DataColumn[1];
                        PrimaryKey[0] = dbc3.ReceptionTable.Columns["receptionID"];
                        dbc3.ReceptionTable.PrimaryKey = PrimaryKey;
                        DataRow delRow = dbc3.ReceptionTable.Rows.Find(listViewIndexID1);
                        int rowCount = dbc3.ReceptionTable.Rows.Count; // 삭제전 전체 row 갯수
                        receptionlist.FindDocument(DBClass.hospiID, DateTime.Now.ToString("yyyy-MM-dd"), listView1.Items[SelectRow].SubItems[1].Text.Substring(0, 2) + ":" + listView1.Items[SelectRow].SubItems[1].Text.Substring(5, 2), listView1.Items[SelectRow].SubItems[5].Text);
                        dbc.Delay(200);
                        delRow.Delete();
                        int receptionID = Convert.ToInt32(listViewIndexID1);
                        // listViewIndexID1 을 증감시킬경우 for문에 영향을 주므로 변수를 따로 지정해서 사용

                        

                        //  열 하나가 삭제될 경우 열의 인덱스가 삭제 대상보다 높은경우 모두 -1 해줌
                        // ex) 10개열 테이블에서 7번열 삭제시 8ㅡ>7 / 9-ㅡ>8 / 10ㅡ>9
                        for (int i = 0; i < (rowCount - Convert.ToInt32(listViewIndexID1)); i++)
                        {
                            delRow = dbc3.ReceptionTable.Rows[rowCount - (rowCount - receptionID)];
                            delRow.BeginEdit();

                            dbc.Delay(200);
                            delRow["receptionID"] = Convert.ToInt32(delRow["receptionID"]) - 1;
                            delRow.EndEdit();
                            receptionID += 1;

                        }

                        dbc3.DBAdapter.Update(dbc3.DS, "reception");
                        dbc3.DS.AcceptChanges();


                        
                        receptionlist.Delete_Reception();
                        dbc.Delay(200);

                        ReceptionListUpdate(1);

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
                if (listViewModeL == 1)
                {
                    button2_Click(sender, e); // 진료대기버튼
                }
                else if (listViewModeL == 2)
                {
                    button5_Click(sender, e); // 진료보류버튼
                }

                listViewIndexID1 = null; // 삭제완료후 null값 넣어줌
            }
        }

        // 접수수정 버튼
        private void button19_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("수정할 항목이 선택되지 않았습니다.", "알림");
            }
            else if (listViewIndexID1 != null)
            {
                ReceptionUpdate receptionUpdate = new ReceptionUpdate();
                receptionUpdate.ReceptionID = Convert.ToInt32(listViewIndexID1);
                receptionUpdate.SelectedSubjectName = selectedSubjectName;
                receptionUpdate.ShowDialog();

                if (listViewModeL == 1)
                {
                    button2_Click(sender, e); // 접수내역버튼
                }
                else if (listViewModeL == 2)
                {
                    button5_Click(sender, e); // 진료보류버튼
                }
                listViewIndexID1 = null;
            }
        }

        // 진료보류, 접수복구 버튼
        private void button22_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("보류할 항목이 선택되지 않았습니다.", "알림");
            }
            else
            {
                if(listViewModeL == 1)
                {
                    messageTypeL = "접수를 보류합니다.";
                }
                else if (listViewModeL == 2)
                {
                    messageTypeL = "접수를 복구합니다.";
                }
                DialogResult ok = MessageBox.Show(messageTypeL + "\r\n\n수진자명 : " + listViewIndexPatientNameL, "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc3.Reception_Open();
                        dbc3.ReceptionTable = dbc3.DS.Tables["reception"];
                        DataRow upRow = dbc3.ReceptionTable.Rows[Convert.ToInt32(listViewIndexID1) - 1];

                        if (listViewModeL == 1)
                        {
                            upRow.BeginEdit();
                            upRow["receptionType"] = 4;
                            upRow.EndEdit();
                            dbc3.DBAdapter.Update(dbc3.DS, "reception");
                            dbc3.DS.AcceptChanges();

                            button2_Click(sender, e); // 진료대기버튼
                        }
                        else if (listViewModeL == 2)
                        {
                            upRow.BeginEdit();
                            upRow["receptionType"] = 1;
                            upRow.EndEdit();
                            dbc3.DBAdapter.Update(dbc3.DS, "reception");
                            dbc3.DS.AcceptChanges();

                            button5_Click(sender, e); // 진료 보류 버튼
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
                else
                {
                    if (listViewModeL == 1)
                    {
                        button2_Click(sender, e); // 접수내역버튼
                    }
                    else if (listViewModeL == 2)
                    {
                        button5_Click(sender, e); // 진료보류버튼
                    }
                }
            }
            listViewIndexID1 = null;
        }

        // 처방확인 버튼
        private void button15_Click(object sender, EventArgs e)
        {
            if (prescriptionArr[0] != null)
            {
                Prescription prescription = new Prescription();
                prescription.PatientID = prescriptionArr[0];
                prescription.ReceptionDate = prescriptionArr[1];
                prescription.ReceptionTime = prescriptionArr[2];

                prescription.Patient = listView3.Items[listView3SelectedRow].SubItems[3].Text;
                prescription.PatientAge = listView3.Items[listView3SelectedRow].SubItems[4].Text;
                prescription.SubjectName = listView3.Items[listView3SelectedRow].SubItems[5].Text;

                prescription.PrescriptionType = prescriptionType;

                prescription.ShowDialog();

                if(button8.Text == "▶ 수납대기 ◀")
                {
                    button8_Click(sender, e);
                }
                else if (button13.Text == "▶ 수납완료 ◀")
                {
                    button2_Click(sender, e);   // 처방전 출력 이후 수납대기 새로고침
                }
            }
            else if (prescriptionArr[0] == null)
            {
                MessageBox.Show("조회할 항목이 선택되지 않았습니다", "알림");
            }
        }

        // 수납현황 listView 클릭
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // prescription배열에 ( patientID, receptionDate, receptionTime ) 넣기
            if (listView3.SelectedItems.Count != 0)
            {
                listView3SelectedRow = listView3.SelectedItems[0].Index;
                int selectRow = listView3.SelectedItems[0].Index;
                prescriptionArr[0] = listView3.Items[selectRow].SubItems[2].Text;
                prescriptionArr[1] = dateTimePicker2.Value.ToString("yy-MM-dd");
                prescriptionArr[2] = listView3.Items[selectRow].SubItems[1].Text.Substring(0,2)+listView3.Items[selectRow].SubItems[1].Text.Substring(5,2);
                prescriptionArr[3] = listView3.Items[selectRow].SubItems[3].Text;
                prescriptionArr[4] = listView3.Items[selectRow].SubItems[5].Text;

                listViewIndexID2 = listView3.Items[selectRow].SubItems[7].Text;
                listViewIndexPatientNameR = listView3.Items[selectRow].SubItems[3].Text;
            }
        }

        // 접수현황 listView 클릭
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // listViewIndexID1에 7번 컬럼값 넣기
            if (listView1.SelectedItems.Count != 0)
            {
                SelectRow = listView1.SelectedItems[0].Index;
                int selectRow = listView1.SelectedItems[0].Index;
                listViewIndexID1 = listView1.Items[selectRow].SubItems[7].Text;
                listViewIndexPatientNameL = listView1.Items[selectRow].SubItems[3].Text;

                selectedSubjectName = listView1.Items[selectRow].SubItems[5].Text;
            }
        }

        // 이전 진료기록 더블클릭
        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listView2.SelectedItems.Count > 0)
            {
                for(int i=0; i<listView2.Items.Count; i++)
                {
                    if(listView2.Items[i].Selected == true)
                    {
                        selectedListViewItemIndex = i;
                        break;
                    }
                }
            }
            if(selectedListViewItemIndex != -1)
            {
                Reception_HistoryInfo reception_HistoryInfo = new Reception_HistoryInfo();
                reception_HistoryInfo.ReceptionInfo = hisTable.Rows[selectedListViewItemIndex]["receptionInfo"].ToString();
                reception_HistoryInfo.ShowDialog();
                string history = reception_HistoryInfo.ReceptionInfo;
                if(history != "")
                {
                    textBoxPurpose.Text = history;
                }

            }
            selectedListViewItemIndex = -1;
        }

        // 수납완료 버튼
        private void button14_Click(object sender, EventArgs e)
        {
            if (listViewIndexID2 == null)
            {
                MessageBox.Show("완료하실 수납 정보가 선택되지 않았습니다.", "알림");
            }
            else
            {
                if (listViewModeR == 1)
                {
                    Payment payment = new Payment();
                    payment.PatientID = prescriptionArr[0];
                    payment.PatientName = prescriptionArr[3];
                    payment.SubjectName = prescriptionArr[4];
                    payment.HospitalID = hospitalID;
                    payment.ReceptionDate = dateTimePicker2.Value.ToString("yy-MM-dd");
                    payment.ReceptionTime = prescriptionArr[2];
                    payment.ShowDialog();

                    button8_Click(sender, e);
                }
            }
            listViewIndexID1 = null;
        }

        // 날짜정보 dateTimePicker 변경시
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            button21.Enabled = true;
            button2_Click(sender, e); // 진료대기버튼
            button8_Click(sender, e); // 수납대기버튼

            dbc2.Reception_Update(date, 1);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount1.Text = "진료대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
            dbc2.Reception_Update(date, 4);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount1.Text = receptionCount1.Text + "\r\n진료보류 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");


            dbc2.Reception_Update(date, 2);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount2.Text = "수납대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
            dbc2.Reception_Update(date, 3);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount2.Text = receptionCount2.Text + "\r\n수납완료 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
        }

        private void inquirybutton_Click(object sender, EventArgs e)
        {
            InquiryCheck inquiry = new InquiryCheck();
            inquiry.HospitalID = hospitalID;
            inquiry.ShowDialog();
        }

        // listView 정렬막기
        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView3_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView3.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)      // 현재 체크박스가 체크상태일때만 시간 수정
            {
                comboBoxTime2.Text = DateTime.Now.ToString("mm");
                if (comboBoxTime2.Text == "59")
                {
                    comboBoxTime1.Text = DateTime.Now.ToString("HH");
                }
            }

            //
            if(button2.Text == "▶ 진료대기 ◀")
            {
                dbc5.Reception_Update(dateTimePicker2.Value.ToString("yy-MM-dd"), 1);
                dbc5.ReceptionTable = dbc5.DS.Tables["reception"];
                if(dbc5.ReceptionTable.Rows.Count != listView1.Items.Count)
                {
                    button2_Click(sender, e);
                }
            }
            else if(button5.Text == "▶ 진료보류 ◀")
            {
                dbc5.Reception_Update(dateTimePicker2.Value.ToString("yy-MM-dd"), 4);
                dbc5.ReceptionTable = dbc5.DS.Tables["reception"];
                if (dbc5.ReceptionTable.Rows.Count != listView1.Items.Count)
                {
                    button5_Click(sender, e);
                }
            }


            if (button8.Text == "▶ 수납대기 ◀")
            {
                dbc5.Reception_Update(dateTimePicker2.Value.ToString("yy-MM-dd"), 2);
                dbc5.ReceptionTable = dbc5.DS.Tables["reception"];
                if (dbc5.ReceptionTable.Rows.Count != listView3.Items.Count)
                {
                    button8_Click(sender, e);
                }
            }
            else if (button13.Text == "▶ 진료보류 ◀")
            {
                dbc5.Reception_Update(dateTimePicker2.Value.ToString("yy-MM-dd"), 3);
                dbc5.ReceptionTable = dbc5.DS.Tables["reception"];
                if (dbc5.ReceptionTable.Rows.Count != listView3.Items.Count)
                {
                    button13_Click(sender, e);
                }
            }

            dbc2.Reception_Update(date, 1);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount1.Text = "진료대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
            dbc2.Reception_Update(date, 4);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount1.Text = receptionCount1.Text + "\r\n진료보류 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");

            dbc2.Reception_Update(date, 2);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount2.Text = "수납대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
            dbc2.Reception_Update(date, 3);
            dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
            receptionCount2.Text = receptionCount2.Text + "\r\n수납완료 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
        }

     

        // 수진자명 조회 엔터 이벤트
        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button9_Click(sender, e);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                ReceptionAdd();
                dbc.Delay(200);

                ReceptionUpdate(1);
                ReceptionListAdd();
            }
            catch(Exception ex)
            {
                MessageBox.Show("이미 당일 예약을 완료하였습니다.", "알림");
            }
        }



        //예약 -> 접수
        public void ReceptionAdd()
        {
            if (reserve.list.Count != 0)
            {
                for (int i = 0; i < reserve.list.Count; i++)
                {
                    if (reserve.list[i].reservationStatus == 1)
                    {
                        try
                        {
                            dbc.Delay(200);
                            dbc.Visitor_Open();
                            dbc.VisitorTable = dbc.DS.Tables["Visitor"];
                            dbc.Reception_Open();
                            dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                            DataRow newRow = dbc.ReceptionTable.NewRow();
                            newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                            reserve.FindPatient(reserve.list[i].id);
                            dbc.Delay(200);
                            for (int j = 0; j < dbc.VisitorTable.Rows.Count; j++)
                            {
                                if (dbc.VisitorTable.Rows[j]["PATIENTNAME"].ToString() == reserve.patientName)
                                {
                                    newRow["PATIENTID"] = j + 1;
                                }
                            }

                            newRow["ReceptionTime"] = reserve.list[i].reservationTime.Substring(0, 2) + reserve.list[i].reservationTime.Substring(3, 2);
                            newRow["ReceptionDate"] = reserve.list[i].reservationDate.Substring(2, 8);
                            newRow["SubjectName"] = reserve.list[i].department;
                            for (int j = 0; j < dbc.ReceptionistTable.Rows.Count; j++)
                            {
                                if (dbc.ReceptionistTable.Rows[j]["receptionistName"].ToString() == receptionistName)
                                {
                                    newRow["ReceptionistCode"] = j + 1;
                                }
                            }

                            newRow["ReceptionInfo"] = reserve.list[i].symptom;
                            newRow["ReceptionType"] = 1;
                            reserve.FindDocument(hospitalID, reserve.list[i].reservationTime, reserve.list[i].id, reserve.list[i].reservationDate, reserve.list[i].department);
                            dbc.Delay(200);


                            newRow["ReceptionCode"] = Reserve.documentName;

                            acceptReserve = 1;
                            dbc.ReceptionTable.Rows.Add(newRow);
                            dbc.DBAdapter.Update(dbc.DS, "Reception");
                            dbc.DS.AcceptChanges();



                        }
                        catch (Exception e)
                        {
                            dbc.Delay(200);
                            reserve.FindPatient(reserve.list[i].id);
                            dbc.Delay(200);
                            dbc.Visitor_Open();
                            dbc.VisitorTable = dbc.DS.Tables["Visitor"];
                            DataRow newRow = dbc.VisitorTable.NewRow();
                            newRow["PatientID"] = dbc.VisitorTable.Rows.Count + 1;
                            newRow["PatientName"] = reserve.patientName;
                            if (Convert.ToInt32(reserve.patientBirth.Substring(0, 4)) < 2000)
                            {
                                newRow["PatientBirthcode"] = reserve.patientBirth.Substring(2, 2) + reserve.patientBirth.Substring(5, 2) + reserve.patientBirth.Substring(8, 2) + "-0";
                            }
                            else if (Convert.ToInt32(reserve.patientBirth.Substring(0, 4)) > 2000)
                            {
                                newRow["PatientBirthcode"] = reserve.patientBirth.Substring(2, 2) + reserve.patientBirth.Substring(5, 2) + reserve.patientBirth.Substring(8, 2) + "-5";
                            }
                            newRow["PatientPhone"] = reserve.patientPhone.Substring(0, 3) + reserve.patientPhone.Substring(4, 4) + reserve.patientPhone.Substring(9, 4);
                            newRow["PatientAddress"] = reserve.patientAddress;
                            newRow["MemberID"] = reserve.patientId;
                            newRow["PatientMemo"] = "";

                            dbc.VisitorTable.Rows.Add(newRow);
                            dbc.DBAdapter.Update(dbc.DS, "Visitor");
                            dbc.DS.AcceptChanges();

                            dbc.Reception_Open();
                            dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                            newRow = dbc.ReceptionTable.NewRow();
                            newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                            for (int j = 0; j < dbc.VisitorTable.Rows.Count; j++)
                            {
                                if (dbc.VisitorTable.Rows[j]["PATIENTNAME"].ToString() == reserve.patientName)
                                {
                                    newRow["PATIENTID"] = j + 1;
                                }
                            }

                            newRow["ReceptionTime"] = reserve.list[i].reservationTime.Substring(0, 2) + reserve.list[i].reservationTime.Substring(3, 2);
                            newRow["ReceptionDate"] = reserve.list[i].reservationDate.Substring(2, 8);
                            newRow["SubjectName"] = reserve.list[i].department;
                            for (int j = 0; j < dbc.ReceptionistTable.Rows.Count; j++)
                            {
                                if (dbc.ReceptionistTable.Rows[j]["receptionistName"].ToString() == receptionistName)
                                {
                                    newRow["ReceptionistCode"] = j + 1;
                                }
                            }

                            newRow["ReceptionInfo"] = reserve.list[i].symptom;
                            newRow["ReceptionType"] = 1;
                            reserve.FindDocument(hospitalID, reserve.list[i].reservationTime, reserve.list[i].id, reserve.list[i].reservationDate, reserve.list[i].department);
                            dbc.Delay(300);


                            newRow["ReceptionCode"] = Reserve.documentName;

                            acceptReserve = 1;
                            dbc.ReceptionTable.Rows.Add(newRow);
                            dbc.DBAdapter.Update(dbc.DS, "Reception");
                            dbc.DS.AcceptChanges();

                        }
                    }
                }
                if(acceptReserve == 1) { MessageBox.Show("당일 예약 등록이 완료되었습니다.", "알림"); }
                else if(acceptReserve == 0) { MessageBox.Show("승인된 예약이 없습니다.", "알림"); }
            }
            else if (reserve.list.Count == 0)
            {
                    MessageBox.Show("당일 예약이 없습니다.", "알림");
            }
        }

        private void 초진환자등록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reception_First receipt_First = new Reception_First();
            receipt_First.ShowDialog();
            patientName.Text = receipt_First.VisitorName; // 수진자명 받아와서 텍스트 대입
            if (patientName.Text != "")
            {
                button9_Click(sender, e);                                   // 조회 클릭
                VisitorText(0);                                                     // 수진자 정보 넣기
            }
        }

        private void 예약확인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();
            reservation.HospitalID = hospitalID;
            reservation.Receptionist = receptionistName;
            reservation.ShowDialog();

            button2_Click(sender, e);
        }

        private void 문의확인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InquiryCheck inquiry = new InquiryCheck();
            inquiry.HospitalID = hospitalID;
            inquiry.ShowDialog();
        }

        private void 병원정보설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.HospitalID = hospitalID;
            checkMasterPW.FormNum = 1;
            checkMasterPW.ShowDialog();
        }

        private void 접수자변경ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 접수자 변경 메뉴 
            Receptionist receptionist = new Receptionist();
            receptionist.ReceptionistName = textBoxReceptionist.Text;
            receptionist.ShowDialog();
            textBoxReceptionist.Text = receptionist.ReceptionistName;

            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
        }

        private void 환자정보수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientSetting Psetting = new PatientSetting();
            Psetting.ShowDialog();
        }

        public void ReceptionListAdd()
        {
            for (int i = 0; i < reserve.list.Count; i++)
            {
                if (reserve.list[i].reservationStatus == 1)
                {
                    string receptionDate = reserve.list[i].reservationDate;
                    string receptionTime = reserve.list[i].reservationTime;

                    reserve.FindPatient(reserve.list[i].id);
                    dbc.Delay(200);
                    dbc4.countWaiting(reserve.list[i].department, reserve.list[i].reservationTime.Substring(0, 2) + reserve.list[i].reservationTime.Substring(3, 2), reserve.list[i].reservationDate.Substring(2, 8));
                    dbc4.WaitingTable = dbc4.DS.Tables["Reception"];
                    dbc.FindDoctor(reserve.list[i].department);
                    dbc.SubjectTable = dbc.DS.Tables["Subject"];
                    try
                    {
                        receptionlist.ReceptionAccept(reserve.list[i].department, dbc.SubjectTable.Rows[0][0].ToString(), reserve.list[i].id, reserve.patientName, receptionDate,receptionTime, Convert.ToInt32(dbc4.WaitingTable.Rows[0][0].ToString()));
                    }
                    catch
                    {
                        receptionlist.ReceptionAccept(reserve.list[i].department, dbc.SubjectTable.Rows[0][0].ToString(), reserve.list[i].id, reserve.patientName, receptionDate,receptionTime, waitingIsNull);
                    }

                    reserve.FindDocument(hospitalID, reserve.list[i].reservationTime, reserve.list[i].id, reserve.list[i].reservationDate, reserve.list[i].department);
                    dbc4.Delay(200);
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (button12.Enabled == false)
            {
                MessageBox.Show("보류 상태에서는 취소할 수 없습니다. 대기상태로 전환 후에 삭제해주세요", "알림");
            }
        }

        private void listView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView2.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void comboBoxSubjcet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void 끝내기XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }



        //접수 = 0 그 외 =1
        public void ReceptionListUpdate(int status)
        {

            if (status == 0)
            {
                updatewait = 1;
                receptionlist.TodayReceptionOpen(DBClass.hospiID, comboBoxSubjcet.Text);
                dbc.Delay(100);
                for (int i = 0; i < receptionlist.list.Count; i++)
                {

                    dbc4.countWaiting(comboBoxSubjcet.Text , receptionlist.list[i].receptionTime.Substring(0, 2) + receptionlist.list[i].receptionTime.Substring(3, 2), DateTime.Now.ToString("yy-MM-dd"));
                    dbc4.WaitingTable = dbc4.DS.Tables["Reception"];


                    
                    receptionlist.FindDocument(hospitalID, receptionlist.list[i].receptionDate, receptionlist.list[i].receptionTime, comboBoxSubjcet.Text);
                    dbc.Delay(100);
                    MessageBox.Show(receptionlist.documentName);
                    try
                    {
                        receptionlist.watingNumberUpdate(Convert.ToInt32(dbc4.WaitingTable.Rows[0][0]));
                    }
                    catch
                    {
                        receptionlist.watingNumberUpdate(waitingIsNull);
                    }
                    dbc.Delay(100);
                }
            }
            if (status == 1)
            {
                updatewait = 1;
                receptionlist.TodayReceptionOpen(hospitalID, listView1.Items[SelectRow].SubItems[5].Text);
                dbc.Delay(100);
                for (int i = 0; i < receptionlist.list.Count; i++)
                {

                    dbc4.countWaiting(listView1.Items[SelectRow].SubItems[5].Text, receptionlist.list[i].receptionTime.Substring(0, 2) + receptionlist.list[i].receptionTime.Substring(3, 2), DateTime.Now.ToString("yy-MM-dd"));
                    dbc4.WaitingTable = dbc4.DS.Tables["Reception"];

                    receptionlist.FindDocument(hospitalID, receptionlist.list[i].receptionDate, receptionlist.list[i].receptionTime, receptionlist.list[i].department);
                    dbc.Delay(100);
                    try
                    {
                        receptionlist.watingNumberUpdate(Convert.ToInt32(dbc4.WaitingTable.Rows[0][0]));
                    }
                    catch
                    {
                        receptionlist.watingNumberUpdate(waitingIsNull);
                    }
                    dbc.Delay(100);
                }
            }
            updatewait = 0;
        }

        public string FindDay(string Date)
        {
            string day = "";
            DateTime date = new DateTime();
            date = Convert.ToDateTime(Date);

            if (date.DayOfWeek == DayOfWeek.Monday)
                day = "(월)";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                day = "(화)";
            else if (date.DayOfWeek == DayOfWeek.Wednesday)
                day = "(수)";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                day = "(목)";
            else if (date.DayOfWeek == DayOfWeek.Friday)
                day = "(금)";
            else if (date.DayOfWeek == DayOfWeek.Saturday)
                day = "(토)";
            else if (date.DayOfWeek == DayOfWeek.Sunday)
                day = "(일)";

            return day;
        }

        public void push()
        {
            new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText("HOSPI")
                                    .AddText("다음 환자 호출")
                                    .Show();
        }
    }

}

