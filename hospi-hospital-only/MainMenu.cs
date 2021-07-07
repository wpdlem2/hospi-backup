using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace hospi_hospital_only
{
    public partial class MainMenu : Form
    {
        string hospitalID;
        DBClass dbc = new DBClass();

        Main main = new Main();
        CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
        Inquiry inquiry = new Inquiry();
        int indexNum;
        string noticeID;

        public MainMenu()
        {
            InitializeComponent();
            dbc.FireConnect();
            inquiry.FireConnect();
        }

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //병원 정보 불러오기
            dbc.Hospital_Open(hospitalID);
            dbc.Delay(400);
            // 날짜정보
            string date = DateTime.Now.ToString("yyyy-MM-dd ddd요일 ", cultures);
            string time = DateTime.Now.ToString("HH:mm:ss");
            label1.Text = date + " " + time;
            timer1.Start();
            // 접수처
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;
                if (name.Substring(length - 1) != ")")
                {
                    comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }
            comboBoxReceptionist.Text = dbc.ReceptionistTable.Rows[0]["receptionistName"].ToString();

            // 진료실
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            for (int i = 0; i < DBClass.hospidepartment.Length; i++)
            {
                comboBoxOffice.Items.Add(DBClass.hospidepartment[i]);
            }

            comboBoxOffice.Text = DBClass.hospidepartment[0];

            label3.Text = dbc.Hospiname;



            // 공지사항 띄우기
            listView2.Items.Clear();
            dbc.Notice_Open();
            dbc.NoticeTable = dbc.DS.Tables["Notice"];

            for (int i = 0; i < dbc.NoticeTable.Rows.Count; i++)
            {
                ListViewItem items = new ListViewItem();
                items.Text = (listView2.Items.Count + 1).ToString();
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeTitle"].ToString());
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeWriter"].ToString());
                string startDate = "20" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(0, 2) + "-" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(2, 2) + "-" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(4, 2);
                items.SubItems.Add(startDate);
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeID"].ToString());
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeEndDate"].ToString());
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString());

                if (Convert.ToInt32(dbc.NoticeTable.Rows[i]["NoticeEndDate"].ToString()) > Convert.ToInt32(DateTime.Now.ToString("yyMMdd")))
                {
                    listView2.Items.Add(items);
                }
            }
        }

        // 진료실
        private void buttonOffice_Click(object sender, EventArgs e)
        {
            if (comboBoxOffice.Text != "" && comboBoxOffice.Text != "진료과목 선택")
            {
                Office office = new Office();
                office.SubjectID = comboBoxOffice.Text;

                office.Show();


                comboBoxOffice.Text = "진료과목 선택";
            }
            else
            {
                MessageBox.Show("과목을 선택해주세요.", "알림");
            }
        }

        // 접수처
        private void buttonReception_Click(object sender, EventArgs e)
        {
            
            if (comboBoxReceptionist.Text != "" && comboBoxReceptionist.Text != "접수자 선택")
            {
                Reception reception = new Reception();
                reception.HospitalID = hospitalID;
                reception.ReceptionistName = comboBoxReceptionist.Text;

                reception.Show();

                comboBoxReceptionist.Text = "접수자 선택";
            }
            else
            {
                MessageBox.Show("접수자를 선택해주세요.", "알림");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 병원설정변경
        private void button3_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.HospitalID = hospitalID;
            checkMasterPW.FormNum = 1;
            checkMasterPW.ShowDialog();
        }

        // 문의확인
        private void button4_Click(object sender, EventArgs e)
        {
            InquiryCheck inquiry = new InquiryCheck();
            inquiry.HospitalID = hospitalID;
            inquiry.ShowDialog();
        }

        // 공지사항 톱니바퀴 클릭
        private void setting1_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.HospitalID = hospitalID;
            checkMasterPW.FormNum = 2;
            checkMasterPW.ShowDialog();

            listView2.Items.Clear();
            dbc.Notice_Open();
            dbc.NoticeTable = dbc.DS.Tables["Notice"];

            for (int i = 0; i < dbc.NoticeTable.Rows.Count; i++)
            {
                ListViewItem items = new ListViewItem();
                items.Text = (listView2.Items.Count + 1).ToString();
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeTitle"].ToString());
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeWriter"].ToString());
                string startDate = "20" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(0, 2) + "-" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(2, 2) + "-" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(4, 2);
                items.SubItems.Add(startDate);
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeID"].ToString());
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeEndDate"].ToString());
                items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString());

                if (Convert.ToInt32(dbc.NoticeTable.Rows[i]["NoticeEndDate"].ToString()) > Convert.ToInt32(DateTime.Now.ToString("yyMMdd")))
                {
                    listView2.Items.Add(items);
                }
            }
        }

        private void listView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
           //e.NewWidth = listView2.Columns[e.ColumnIndex].Width;
           // e.Cancel = true;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                noticeID = listView2.Items[listView2.FocusedItem.Index].SubItems[4].Text.ToString();
            }
            
        }

        // 관리자 메뉴 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.FormNum = 4;
            checkMasterPW.ShowDialog();
        }

        // 접수자, 과목 수정 톱니바퀴
        private void setting2_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMaserPW = new CheckMasterPW();
            checkMaserPW.FormNum = 6;
            checkMaserPW.ShowDialog();

            comboBoxReceptionist.Items.Clear();
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;
                if (name.Substring(length - 1) != ")")
                {
                    comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }
            comboBoxReceptionist.Text = dbc.ReceptionistTable.Rows[0]["receptionistName"].ToString();

        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (noticeID != "")
            {
                NoticeInfo noticeInfo = new NoticeInfo();
                noticeInfo.NoticeID = noticeID;
                noticeInfo.ShowDialog();

                if (1 == noticeInfo.Update)
                {
                    listView2.Items.Clear();
                    dbc.Notice_Open();
                    dbc.NoticeTable = dbc.DS.Tables["Notice"];

                    for (int i = 0; i < dbc.NoticeTable.Rows.Count; i++)
                    {
                        ListViewItem items = new ListViewItem();
                        items.Text = (listView2.Items.Count + 1).ToString();
                        items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeTitle"].ToString());
                        items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeWriter"].ToString());
                        string startDate = "20" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(0, 2) + "-" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(2, 2) + "-" + dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString().Substring(4, 2);
                        items.SubItems.Add(startDate);
                        items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeID"].ToString());
                        items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeEndDate"].ToString());
                        items.SubItems.Add(dbc.NoticeTable.Rows[i]["NoticeStartDate"].ToString());

                        if (Convert.ToInt32(dbc.NoticeTable.Rows[i]["NoticeEndDate"].ToString()) > Convert.ToInt32(DateTime.Now.ToString("yyMMdd")))
                        {
                            listView2.Items.Add(items);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        // 촬영실 열기
        private void button2_Click_1(object sender, EventArgs e)
        {
            AddRadiography radiography = new AddRadiography();
            radiography.ShowDialog();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd ddd요일 ", cultures);
            string time = DateTime.Now.ToString("HH:mm:ss");
            label1.Text = date + " " + time;
        }

        private void 관리자계정관리MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void 운영정보설정SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void 과목및접수자편집EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setting2_Click(sender, e);
        }

        private void 공지사항등록DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setting1_Click(sender, e);
        }

        private void ddddToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void 끝내기XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBoxReceptionist_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxOffice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Hospi  ver1.0", "버전 정보");
        }
    }
}

