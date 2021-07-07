using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace hospi_hospital_only
{
    public partial class Office : Form
    {
        DBClass dbc = new DBClass();
        DBClass dbc2 = new DBClass(); // timer 전용
        DBClass dbc3 = new DBClass(); // 이전처방용

        ReceptionList reception = new ReceptionList();
        Reserve reserve = new Reserve();
        Visitor visitor = new Visitor();
        Fcm fcm = new Fcm();

        string date;
        string subjectID; // 과목코드
        string listView1Index; // 리스트뷰1 아이템 클릭시 인덱스 저장을 위함
        string medicienID; // 약품번호 저장을 위함
        int receptionID; // 접수번호 저장을 위함 ( 진료완료시 dbc.ReceptionTable 에서 접수번호의 row를 찾아 receptionType를 3으로 수정하기 위함 )
        int receptionTime; // 접수시간 저장을 위함 ( reception 정보 받아올때 저장해서 처방전에 receptionTime으로 저장 )
        int selectedListViewItemIndex; // 이전 진료기록 리스트뷰의 선택 인덱스 저장
        DataTable hisTable; // 수진자 정보 조회시 이전 진료기록을 담은 테이블 ( 이전 진료기록 띄울때 사용하고, 이전 진료기록중 내원목적 확인시에 재사용 )
        int pageNo;
        public string item_name;
        public int waitingIsNull = 0;
        public int SelectRow;
        bool mobileUse;
        string mobileID;
        bool pohwa;

        public string SubjectID
        {
            get { return subjectID; }
            set { subjectID = value; }
        }

        public Office()
        {
            InitializeComponent();
        }

        // 이전진료, 이전처방 조회
        public void VisitorHistory()
        {
            dbc.Visitor_Chart(Convert.ToInt32(textBoxChartNum.Text));
            hisTable = dbc.DS.Tables["visitor"];
            if(hisTable.Rows.Count != 0)
            {
                for(int i=1; i<hisTable.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = hisTable.Rows[i]["receptionDate"].ToString();
                    item.SubItems.Add(hisTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + hisTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                    item.SubItems.Add(hisTable.Rows[i]["subjectName"].ToString());
                    item.SubItems.Add(hisTable.Rows[i]["receptionInfo"].ToString());
                    listView2.Items.Add(item);
                }
            }
        }

        // 진료대기인원 업데이트
        /*public void ReceptionCountUpdate()
        {
            dbc.Reception_Office(date, subjectID);
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            int type1Count = dbc.ReceptionTable.Rows.Count;

            dbc.Reception_Office2(date, subjectID);
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            int type2Count = dbc.ReceptionTable.Rows.Count;

            textBoxReceptionCount.Text = (type1Count + type2Count ).ToString();
        }*/
        
        // 진료완료 (receptionType 1 ㅡ> 2)
        public void ReceptionTypeChange()
        {
            try
            {
                // VisitorRow
                dbc.Visitor_Open();
                dbc.VisitorTable = dbc.DS.Tables["Visitor"];
                DataRow vRow = dbc.VisitorTable.Rows[Convert.ToInt32(textBoxChartNum.Text) - 1]; // 환자

                vRow.BeginEdit();
                vRow["patientMemo"] = textBoxMemo.Text;
                vRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "visitor");

                // ReceptionRow
                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                DataRow rRow = dbc.ReceptionTable.Rows[receptionID - 1];

                rRow.BeginEdit();
                rRow["receptionType"] = 2;
                rRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "reception");

                dbc.DS.AcceptChanges();


                ReceptionListUpdate();

                MessageBox.Show("진료 완료 처리되었습니다.", "알림");
                buttonNextReception.Enabled = true;
                buttonReceptionEnd.Enabled = false;

                TextBoxClear();
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

        // 텍스트박스 비우기, 버튼Enable 초기화
        public void TextBoxClear()
        {
            // 텍스트박스
            textBoxPatientName.Clear();
            textBoxChartNum.Clear();
            textBoxGener.Clear();
            textBoxAge.Clear();
            textBoxPurpose.Clear();
            textBoxMemo.Clear();
            textBoxReceptionCount.Clear();
            textBoxMedicineName.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            // 처방완료라벨
            labelPresctiptionEnd.Visible = false;
            // 버튼
            button4.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            buttonMedicineAdd.Enabled = true;
        }

        // Medicine VIEW 띄우기
        /*public void MedicineView(int medicineType)
        {
            dbc.Medicine_Open(medicineType);
            dbc.MedicineTable = dbc.DS.Tables["Medicine"];
            DBGrid.DataSource = dbc.MedicineTable.DefaultView;

            DBGrid.CurrentCell = null;
            DBGrid.RowHeadersVisible = false; //  좌측 빈 컬럼 지우기
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

            // 헤더 변경
            DBGrid.Columns[0].HeaderText = "No.";
            DBGrid.Columns[1].HeaderText = "약품명";
            DBGrid.Columns[3].HeaderText = "투약방법";
            DBGrid.Columns[0].Width = 40;
            DBGrid.Columns[1].Width = 220;
            DBGrid.Columns[3].Width = 110;
            DBGrid.Columns[2].Visible = false;
        }*/

        // 종료 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            if(labelPresctiptionEnd.Visible == true)
            {
                MessageBox.Show("처방이 등록된 진료가 존재합니다. \r\n진료를 완료 한 후 종료해주세요.", "알림");
            }
            else
            {
                Dispose();
            }
        }

        private void Office_Load(object sender, EventArgs e)
        {
            // 로드시 포커스
            dbc.FireConnect();
            reception.FireConnect();
            reserve.FireConnect();
            visitor.FireConnect();
            dbc.Delay(200);
            this.ActiveControl = buttonReceptionEnd;
            date = DateTime.Now.ToString("yy-MM-dd");

            try
            {
                // 과목
                dbc.Subject_Open();
                dbc.SubjectTable = dbc.DS.Tables["subjectName"];
                textBoxSubjectName.Text = subjectID;
                dbc.FindDoctor(subjectID);
                dbc.SubjectTable = dbc.DS.Tables["subject"];
                textBox1.Text = dbc.SubjectTable.Rows[0][0].ToString();

                dbc.Reception_Office(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
                dbc.ReceptionTable = dbc.DS.Tables["reception"];

                textBoxReceptionCount.Text = dbc.ReceptionTable.Rows.Count.ToString();

                dbc.Reception_Office2(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
                dbc.ReceptionTable = dbc.DS.Tables["reception"];

                textBoxReceptionCount.Text = (Convert.ToInt32(textBoxReceptionCount.Text) + dbc.ReceptionTable.Rows.Count).ToString();

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

        // 날짜정보 수정
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value.ToString("yy-MM-dd");
            buttonToday.Enabled = true;
        }

        // 날자정보 금일 버튼
        private void buttonToday_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToString("yy-MM-dd");
            buttonToday.Enabled = false;
        }

        //다음진료 버튼
        private void buttonNextReception_Click(object sender, EventArgs e)
        {
            try
            {
                buttonNextReception.Enabled = false;
                // DB오픈
                dbc.Reception_Office2(date, subjectID);
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                if (dbc.ReceptionTable.Rows.Count == 0)
                {
                    dbc.Reception_Office(date, subjectID);
                    dbc.ReceptionTable = dbc.DS.Tables["reception"];
                    if (dbc.ReceptionTable.Rows.Count == 0)
                    {
                        MessageBox.Show("[ " + textBoxSubjectName.Text + " ] 의 대기환자가 없습니다.", "알림");
                        button5.Enabled = false;
                    }
                    else
                    {

                        DataRow rRow = dbc.ReceptionTable.Rows[0];
                        textBoxPatientName.Text = rRow["patientName"].ToString();
                        textBoxChartNum.Text = rRow["patientID"].ToString();
                        dbc.Mobile_Use(Convert.ToInt32(rRow["patientID"]));
                        dbc.MobileTable = dbc.DS.Tables["Visitor"];
                        if (dbc.MobileTable.Rows[0][0].ToString() != "")  
                        {
                            mobileUse = true;
                            mobileID = dbc.MobileTable.Rows[0][0].ToString();
                            visitor.FindPatient(mobileID);
                            dbc.Delay(200);
                            //알림 메시지 전송
                            fcm.PushNotificationToFCM(DBClass.hospiname, visitor.UserToken, visitor.PatientName+ "님 진료실로 입실 해주세요.");
                        }
                        else if (dbc.MobileTable.Rows[0][0].ToString() == "")
                        {
                            mobileUse = false;
                        }

                        if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "1" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "3")
                        {
                            textBoxGener.Text = "남성";
                        }
                        else if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "2" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "4")
                        {
                            textBoxGener.Text = "여성";
                        }
                        textBoxAge.Text = rRow["patientBirthCode"].ToString().Substring(0, 6);

                        String year = DateTime.Now.ToString("yyyy");
                        if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "1" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "2")
                        {
                            textBoxAge.Text = (Convert.ToInt32(year) - Convert.ToInt32(rRow["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString() + "세";
                        }
                        else if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "3" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "4")
                        {
                            textBoxAge.Text = (Convert.ToInt32(year) - Convert.ToInt32(rRow["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString() + "세";
                        }
                        textBoxPurpose.Text = rRow["receptionInfo"].ToString();
                        textBoxMemo.Text = rRow["patientMemo"].ToString();
                        textBoxReceptionCount.Text = (dbc.ReceptionTable.Rows.Count - 1).ToString();
                        receptionTime = Convert.ToInt32(rRow["receptionTime"]);
                        receptionID = Convert.ToInt32(rRow["receptionID"]);
                        if (dbc.MobileTable.Rows[0][0].ToString() != "")
                        {
                            reception.FindDocument(DBClass.hospiID, "20" + rRow["receptionDate"].ToString(), rRow["receptionTime"].ToString().Substring(0, 2) + ":" + rRow["receptionTime"].ToString().Substring(2, 2), textBoxSubjectName.Text);
                            dbc.Delay(200);
                            reception.Delete_Reception();
                        }
                        buttonNextReception.Enabled = false;
                        buttonReceptionEnd.Enabled = true;
                        buttonMedicineAdd.Enabled = true;

                        VisitorHistory();
                        button5.Enabled = false;
                        button6.Enabled = true;


                    }
                }
                else if (dbc.ReceptionTable.Rows.Count != 0)
                {
                    DataRow rRow = dbc.ReceptionTable.Rows[0];
                    textBoxPatientName.Text = rRow["patientName"].ToString();
                    textBoxChartNum.Text = rRow["patientID"].ToString();
                    dbc.Mobile_Use(Convert.ToInt32(rRow["patientID"]));
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

                    if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "1" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "3")
                    {
                        textBoxGener.Text = "남성";
                    }
                    else if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "2" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "4")
                    {
                        textBoxGener.Text = "여성";
                    }
                    textBoxAge.Text = rRow["patientBirthCode"].ToString().Substring(0, 6);

                    String year = DateTime.Now.ToString("yyyy");
                    if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "1" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "2")
                    {
                        textBoxAge.Text = (Convert.ToInt32(year) - Convert.ToInt32(rRow["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString() + "세";
                    }
                    else if (rRow["patientBirthCode"].ToString().Substring(7, 1) == "3" || rRow["patientBirthCode"].ToString().Substring(7, 1) == "4")
                    {
                        textBoxAge.Text = (Convert.ToInt32(year) - Convert.ToInt32(rRow["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString() + "세";
                    }
                    textBoxPurpose.Text = rRow["receptionInfo"].ToString();
                    textBoxMemo.Text = rRow["patientMemo"].ToString();
                    textBoxReceptionCount.Text = (dbc.ReceptionTable.Rows.Count - 1).ToString();
                    receptionTime = Convert.ToInt32(rRow["receptionTime"]);
                    receptionID = Convert.ToInt32(rRow["receptionID"]);
                    buttonNextReception.Enabled = false;
                    buttonReceptionEnd.Enabled = true;
                    VisitorHistory();
                    button5.Enabled = true;
                    button5_Click(sender, e);

                    Reception.pushAlim = true;
                }
            }
                
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 처방 DBGrid 셀 더블클릭
        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBoxMedicineName.Text = DBGrid.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            //medicienID = DBGrid.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
        }

        // 투약정보 등록버튼
        private void buttonMedicineAdd_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 9)
            {
                ListViewItem item = new ListViewItem();
                item.Text = textBoxMedicineName.Text;
                item.SubItems.Add(comboBoxDosingDays.Text);
                item.SubItems.Add(comboBoxCount.Text);
                item.SubItems.Add(comboBox1Dose.Text);
                item.SubItems.Add(listView4.Items[SelectRow].SubItems[0].Text);
                listView1.Items.Add(item);
            }
            else if (listView1.Items.Count >= 9)
            {
                MessageBox.Show("처방 가능한 약재의 갯수를 초과했습니다.", "알림");
            }
        }

        // 처방 전체삭제 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("삭제할 처방정보가 존재하지 않습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("등록된 모든 처방정보를 삭제하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    listView1.Items.Clear();
                }
            }
        }

        // 처방 정정 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1Index != null)
            {
                listView1.Items.RemoveAt(Convert.ToInt32(listView1Index));
                listView1Index = null;
            }
            else if (listView1Index == null)
            {
                MessageBox.Show("삭제할 처방정보를 선택해주세요.", "알림");
            }
        }

        // 투약정보 리스트뷰 아이템 클릭
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                int selectRow = listView1.SelectedItems[0].Index;
                listView1Index = selectRow.ToString();
            }
        }

        // 처방 등록 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxPatientName.Text == "")
            {
                MessageBox.Show("수진자 정보가 존재하지 않습니다.", "알림");
            }
            else if (listView1.Items.Count == 0)
            {
                MessageBox.Show("등록할 처방 정보가 존재하지 않습니다.", "알림");
            }
            else
            {
                try
                {
                    dbc.Prescription_Open();
                    dbc.PrescriptionTable = dbc.DS.Tables["prescription"];

                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        DataRow newRow = dbc.PrescriptionTable.NewRow();
                        newRow["prescriptionID"] = dbc.PrescriptionTable.Rows.Count + 1;
                        newRow["patientID"] = textBoxChartNum.Text;
                        newRow["receptionDate"] = dateTimePicker1.Value.ToString("yy-MM-dd");
                        newRow["receptionTime"] = receptionTime;
                        newRow["medicineID"] = listView1.Items[i].SubItems[4].Text;
                        newRow["medicinePeriod"] = listView1.Items[i].SubItems[1].Text;
                        newRow["medicineDosage"] = listView1.Items[i].SubItems[3].Text;
                        newRow["Count"] = listView1.Items[i].SubItems[2].Text;
                        newRow["MedicineName"] = listView1.Items[i].SubItems[0].Text;
                        if(textBoxOpinion.Text =="")
                        {
                            newRow["Opinion"] = "등록된 의사소견이 없습니다.";
                        }
                        else
                        {
                            newRow["Opinion"] = textBoxOpinion.Text;
                        }
                        
                        dbc.PrescriptionTable.Rows.Add(newRow);
                    }
                    dbc.DBAdapter.Update(dbc.DS, "Prescription");
                    dbc.DS.AcceptChanges();

                    MessageBox.Show("처방 등록이 완료되었습니다.", "알림");
                    labelPresctiptionEnd.Visible = true;
                    buttonMedicineAdd.Enabled = false;
                    button4.Enabled = false; // 전체삭제 버튼
                    button2.Enabled = false; // 처방정정 버튼
                    button3.Enabled = false; // 처방등록 버튼
                    button6.Enabled = false;
                    buttonMedicineAdd.Enabled = false; // 약품등록버튼
                    textBoxOpinion.Clear();
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

        // 진료완료 버튼
        private void buttonReceptionEnd_Click(object sender, EventArgs e)
        {
            if (labelPresctiptionEnd.Visible == false)
            {
                DialogResult ok = MessageBox.Show("처방정보가 등록되지 않았습니다. 진료를 완료하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    button6.Enabled = false;
                    if (mobileUse)
                    {
                        reserve.EndDocument(DBClass.hospiID, textBoxSubjectName.Text, mobileID, DateTime.Now.ToString("yyyy-MM-dd"), receptionTime.ToString().Substring(0, 2) + ":" + receptionTime.ToString().Substring(2, 2));
                        dbc.Delay(200);
                        reserve.ReserveEnd();
                    }
                    
                    ReceptionTypeChange();
                }
            }
            else
            {
                if (mobileUse)
                {
                    reserve.EndDocument(DBClass.hospiID, textBoxSubjectName.Text, mobileID, DateTime.Now.ToString("yyyy-MM-dd"), receptionTime.ToString().Substring(0, 2) + ":" + receptionTime.ToString().Substring(2, 2));
                    dbc.Delay(200);
                    reserve.ReserveEnd();
                }
                textBox4.Text = "";
                listView4.Items.Clear();

                ReceptionTypeChange();

                
            }
        }

        // 대기목록 버튼
        private void buttonReceptionList_Click(object sender, EventArgs e)
        {
            dbc.Reception_Office(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
            dbc.ReceptionTable = dbc.DS.Tables["reception"];

            if(buttonNextReception.Enabled == false)
            {
                textBoxReceptionCount.Text = (dbc.ReceptionTable.Rows.Count-1).ToString();
            }
            else if (buttonNextReception.Enabled == true)
            {
                textBoxReceptionCount.Text = (dbc.ReceptionTable.Rows.Count).ToString();
            }
            //dbc.Reception_Update(date, 0);
            DataTable restTable = dbc.DS.Tables["reception"];
            Office_WaitingList office_WaitingList = new Office_WaitingList();
            office_WaitingList.WatingTable = restTable;
            office_WaitingList.Date = dateTimePicker1.Value.ToString("yy-MM-dd");
            office_WaitingList.SubjectID = subjectID;

            if(textBoxPatientName.Text == "")
            {
                office_WaitingList.PatientEnter = 0;    // 현재 진료중인 환자가 없을떄
            }
            else if (textBoxPatientName.Text != "")
            {  
                office_WaitingList.PatientEnter = 1;    // 현재 진료중인 환자가 있을때
            }

            office_WaitingList.ShowDialog();

        }

        string receptiedTime;
        string receptiedDate;
        // 이전 진료기록 더블클릭
        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView2.Items.Count ; i++)
                {
                    if (listView2.Items[i].Selected == true)
                    {
                        selectedListViewItemIndex = i;
                        receptiedDate = listView2.Items[i].SubItems[0].Text;
                        receptiedTime = listView2.Items[i].SubItems[1].Text.Substring(0, 2) + listView2.Items[i].SubItems[1].Text.Substring(5, 2);
                        if(receptiedTime.Substring(0,1) == "0")
                        {
                            receptiedTime = receptiedTime.Substring(1, receptiedTime.Length - 1);
                        }
                        break;
                    }
                }
            }
            // 이전 처방
            if (listView2.Items.Count > 0)
            {
                listView3.Items.Clear();
                dbc3.Presctiption_History(textBoxChartNum.Text, receptiedDate, receptiedTime);
                dbc3.PrescriptionTable = dbc3.DS.Tables["Prescription"];
                for (int i = 0; i < dbc3.PrescriptionTable.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = dbc3.PrescriptionTable.Rows[i][0].ToString();
                    item.SubItems.Add(dbc3.PrescriptionTable.Rows[i][1].ToString());
                    item.SubItems.Add(dbc3.PrescriptionTable.Rows[i][2].ToString());
                    listView3.Items.Add(item);
                }
                dbc3.PrescriptionTable.Clear();
            }

            if (selectedListViewItemIndex != -1)
            {
                Reception_HistoryInfo reception_HistoryInfo = new Reception_HistoryInfo();
                reception_HistoryInfo.ReceptionInfo = listView2.Items[selectedListViewItemIndex].SubItems[3].Text;
                reception_HistoryInfo.ParentType = "office";
                reception_HistoryInfo.ShowDialog();
                string history = reception_HistoryInfo.ReceptionInfo;

                


                if (history != "")
                {
                    textBoxPurpose.Text = history;
                }

            }
            selectedListViewItemIndex = -1;
        }



        private async Task buttonMedicienSearch_ClickAsync(object sender, EventArgs e)
        {
            if(textBox4.Text.Trim() == "")
            {
                MessageBox.Show("약품명을 입력해주세요.", "알림");
            }
            else
            {
                string SearchStr = textBox4.Text.Trim();
                textBox4.Text = SearchStr;
                pohwa = false;
                item_name = textBox4.Text;
                listView4.Items.Clear();
                await Task.Run(() => {

                    for (int i = 1; i < 30; i++)
                    {
                        string url = "http://apis.data.go.kr/1470000/MdcinGrnIdntfcInfoService/getMdcinGrnIdntfcInfoList?ServiceKey=" + ConfigurationManager.AppSettings["MedicineAPIKey"] + "&item_name=" + item_name + "&pageNo=" + i + "&numOfRows=100"; // URL

                        XmlDocument xml = new XmlDocument();

                        xml.Load(url);

                        XmlNodeList xnList = xml.SelectNodes("/response/body/items/item");

                        foreach (XmlNode xn in xnList)
                        {

                            ListViewItem item = new ListViewItem();

                            item.Text = xn["ITEM_SEQ"].InnerText;
                            item.SubItems.Add(xn["ITEM_NAME"].InnerText);

                            listView4.Items.Add(item);

                            if(listView4.Items.Count > 20)
                            {
                                pohwa = true;
                                break;
                            }

                        }
                        if(pohwa)
                        {
                            break;
                        }
                    }
                });
            }
        }

        


        private void buttonM1_Click(object sender, EventArgs e)
        {
            //MedicineView(1);
            comboBoxDosingDays.Text = "3";
            comboBoxCount.Text = "3";
            comboBox1Dose.Text = "1";
        }

        private void buttonM2_Click(object sender, EventArgs e)
        {
            //MedicineView(2);
            comboBoxDosingDays.Text = "3";
            comboBoxCount.Text = "3";
            comboBox1Dose.Text = "1";
        }

        private void buttonM3_Click(object sender, EventArgs e)
        {
            //MedicineView(3);
            comboBoxDosingDays.Text = "3";
            comboBoxCount.Text = "3";
            comboBox1Dose.Text = "1";
        }

        private void buttonM4_Click(object sender, EventArgs e)
        {
            //MedicineView(4);
            comboBoxDosingDays.Text = "0";
            comboBoxCount.Text = "0";
            comboBox1Dose.Text = "1";
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonMedicienSearch_ClickAsync(sender, e);
            }
        }

        public void ReceptionListUpdate()
        {
            reception.TodayReceptionOpen(DBClass.hospiID, textBoxSubjectName.Text);
            dbc.Delay(100);
            for (int i = 0; i < reception.list.Count; i++)
            {

                dbc.countWaiting(textBoxSubjectName.Text, reception.list[i].receptionTime.Substring(0, 2) + reception.list[i].receptionTime.Substring(3, 2), DateTime.Now.ToString("yy-MM-dd"));
                dbc.WaitingTable = dbc.DS.Tables["Reception"];

                reception.FindDocument(DBClass.hospiID, reception.list[i].receptionDate, reception.list[i].receptionTime, reception.list[i].department);
                dbc.Delay(100);
                try
                {
                    reception.watingNumberUpdate(Convert.ToInt32(dbc.WaitingTable.Rows[0][0]));
                }
                catch
                {
                    reception.watingNumberUpdate(waitingIsNull);
                }
                dbc.Delay(100);
            }
        }

        private void listView4_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            SelectRow = listView4.SelectedItems[0].Index;

            textBoxMedicineName.Text = listView4.Items[SelectRow].SubItems[1].Text;
            buttonMedicineAdd.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "OfficeRadiography") // 열린 폼의 이름 검사
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {  // 폼을 최소화시켜 하단에 내려놓았는지 검사
                        openForm.WindowState = FormWindowState.Normal;

                        openForm.Location = new Point(this.Location.X + this.Width, this.Location.Y);

                    }
                    openForm.Activate();
                    return;
                }
            }
            OfficeRadiography officeRadiography = new OfficeRadiography();
            officeRadiography.Date = dateTimePicker1.Value.ToString("yy-MM-dd");
            officeRadiography.PatientID = textBoxChartNum.Text;
            officeRadiography.PatientName = textBoxPatientName.Text;

            officeRadiography.StartPosition = FormStartPosition.Manual;  // 원하는 위치를 직접 지정해서 띄우기 위해
            officeRadiography.Location = new Point(this.Location.X + this.Width - officeRadiography.Width, this.Location.Y); // 메인폼의 오른쪽에 위치토록
            officeRadiography.Width = 819;
            officeRadiography.Height = 842;
            officeRadiography.Show();
        }

        // 진료 보류 버튼
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult ok = MessageBox.Show("진료를 보류하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                dbc.Reception_Office3(date, textBoxSubjectName.Text, textBoxChartNum.Text);
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                DataRow upRow = dbc.ReceptionTable.Rows[0];

                upRow.BeginEdit();
                upRow["receptionType"] = "4";
                upRow.EndEdit();

                dbc.DBAdapter.Update(dbc.DS, "reception");
                dbc.DS.AcceptChanges();

                buttonNextReception.Enabled = true;
                buttonReceptionEnd.Enabled = false;
                button6.Enabled = false;

                TextBoxClear();
                ReceptionListUpdate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (buttonNextReception.Enabled == true)
            {
                dbc2.Reception_Office(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
                dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                textBoxReceptionCount.Text = dbc2.ReceptionTable.Rows.Count.ToString();

                dbc2.Reception_Office2(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
                dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                textBoxReceptionCount.Text = (Convert.ToInt32(textBoxReceptionCount.Text) + dbc2.ReceptionTable.Rows.Count).ToString();
            }
            else if (buttonNextReception.Enabled == false)
            {
                dbc2.Reception_Office(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
                dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                textBoxReceptionCount.Text = (dbc2.ReceptionTable.Rows.Count-1).ToString();

                dbc2.Reception_Office2(dateTimePicker1.Value.ToString("yy-MM-dd"), subjectID);
                dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                textBoxReceptionCount.Text = (Convert.ToInt32(textBoxReceptionCount.Text) + dbc2.ReceptionTable.Rows.Count).ToString();
            }
        }

        private void listView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView3_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView4_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void comboBoxDosingDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        private void comboBoxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        private void comboBox1Dose_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        private void buttonMedicienSearch_Click(object sender, EventArgs e)
        {
            buttonMedicienSearch_ClickAsync(sender, e);
        }
    }
}
