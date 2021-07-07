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
    public partial class CheckMasterPW : Form
    {
        DBClass dbc = new DBClass();
        string hospitalID;
        int passwordOK = 0; // 공지사항 수정버튼, 관리자정보 비밀번호 확인버튼 // 비밀번호 참:1 오류:0
        string noticeWriter;    // 공지사항 작성자 문자열
        int masterID;             // master 아이디 저장

        int formNum;
        /* 
         1 = Hospital_Setting
         2 = Notice
         3 = NoticeInfo (수정버튼)
         4 = MasterMenu
         5 = MasterInfomation 비밀번호 확인 버튼
         6 = SelectUpdateMode (접수과, 과목 선택 폼)
             */

        public int FormNum
        {
            get { return formNum; }
            set { formNum = value; }
        }
        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public int PasswordOK
        {
            get { return passwordOK; }
            set { passwordOK = value; }
        }
        public string NoticeWriter
        {
            get { return noticeWriter; }
            set { noticeWriter = value; }
        }

        public CheckMasterPW()
        {
            InitializeComponent();
        }

        public void Open_Form(int formNum)
        {
            if (formNum == 1)
            {
                Hospital_Setting hospital_Setting = new Hospital_Setting();
                hospital_Setting.HospitalID = hospitalID;
                hospital_Setting.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBoxPW.Text == "")
            {
                MessageBox.Show("비밀번호를 입력해주세요.", "알림");
                textBoxPW.Focus();
            }
            else
            {
                dbc.Master_Open();
                dbc.MasterTable = dbc.DS.Tables["master"];

                if (dbc.MasterTable.Rows[masterID]["masterPassword"].ToString() == textBoxPW.Text)
                {
                    if (formNum == 1)    // 병원정보 설정
                    {
                        Hospital_Setting hospital_Setting = new Hospital_Setting();
                        hospital_Setting.HospitalID = hospitalID;
                        hospital_Setting.ShowDialog();
                        if (hospital_Setting.IsDisposed || hospital_Setting == null)
                        {
                            Dispose();
                        }
                    }
                    else if (formNum == 2)    // 공지사항 등록
                    {
                        Notice notice = new Notice();
                        notice.Writer = dbc.MasterTable.Rows[masterID]["MasterName"].ToString();
                        notice.ShowDialog();
                        if (notice.IsDisposed || notice == null)
                        {
                            Dispose();
                        }
                    }
                    else if (formNum == 3)   // 공지사항 수정
                    {
                        passwordOK = 1;
                        Dispose();
                    }
                    else if (formNum == 4)   // 관리자 메뉴
                    {
                        MasterMenu masterMenu = new MasterMenu();
                        masterMenu.MasterName = comboBoxMaster.Text;
                        masterMenu.ShowDialog();
                        if (masterMenu.IsDisposed || masterMenu == null)
                        {
                            Dispose();
                        }
                    }
                    else if (formNum == 5)   // 관리자정보 비밀번호 확인버튼
                    {
                        passwordOK = 1;
                        Dispose();
                    }
                    else if (formNum == 6)
                    {
                        CheckUpdateMode checkUpdateMode = new CheckUpdateMode();
                        checkUpdateMode.ShowDialog();
                        Dispose();
                    }

                }
                else if(dbc.MasterTable.Rows[masterID]["masterPassword"].ToString() != textBoxPW.Text)
                {
                    MessageBox.Show("비밀번호를 확인해주세요.", "알림");
                    textBoxPW.Focus();
                    textBoxPW.SelectAll();
                }
            }
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 엔터이벤트
        private void textBoxPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void CheckMasterPW_Load(object sender, EventArgs e)
        {
            dbc.Master_Open();
            dbc.MasterTable = dbc.DS.Tables["master"];
            
            if(dbc.MasterTable.Rows.Count == 0)
            {
                DataRow newRow = dbc.MasterTable.NewRow();
                newRow["MasterID"] = 0;
                newRow["MasterName"] = "Master";
                newRow["MasterPassword"] = "0000";

                dbc.MasterTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "master");
                dbc.DS.AcceptChanges();

                MessageBox.Show("기본 최상위 관리자가 생성되었습니다. \r\n\r\n관리자명 : [ Master ]\r\n비밀번호 : [ 0000 ]\r\n\r\n관리자 메뉴에서 비밀번호를 변경해주세요.", "알림");

            }

            for(int i=0; i<dbc.MasterTable.Rows.Count; i++)
            {
                comboBoxMaster.Items.Add(dbc.MasterTable.Rows[i]["masterName"].ToString());
            }
            comboBoxMaster.Text = comboBoxMaster.Items[0].ToString();

            // 공지사항 수정일 경우 콤보박스 Enable = false, 관리자명고정 
            if(formNum == 3)
            {
                comboBoxMaster.Enabled = false;
                comboBoxMaster.Text = noticeWriter;
            }

            // 관리자 정보 비밀번호 확인 버튼일 경우 콤보박스 Enable = false, 관리자명고정 
            else if (formNum == 5)
            {
                groupBox7.Text = "비밀번호 재확인";
                comboBoxMaster.Enabled = false;
                comboBoxMaster.Text = dbc.MasterTable.Rows[0]["masterName"].ToString();
            }

        }

        private void comboBoxMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            masterID = comboBoxMaster.SelectedIndex;
            textBoxPW.Clear();
            textBoxPW.Focus();
        }

        private void comboBoxMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'º')
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void textBoxPW_Enter(object sender, EventArgs e)
        {
            textBoxPW.SelectAll();
        }

        // 관리자명 문자입력 방지 (콤보박스로만 변경할 수 있도록)

    }
}
