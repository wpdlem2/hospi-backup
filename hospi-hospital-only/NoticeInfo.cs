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
    public partial class NoticeInfo : Form
    {
        DBClass dbc = new DBClass();
        string noticeID;
        int update;     // 수정 진행시 1

        public string NoticeID
        {
            get { return noticeID; }
            set { noticeID = value; }
        }
        public int Update
        {
            get { return update; }
            set { update = value; }
        }

        public NoticeInfo()
        {
            InitializeComponent();
        }

        private void NoticeInfo_Load(object sender, EventArgs e)
        {
            dbc.Notice_Open(Convert.ToInt32(noticeID));
            dbc.NoticeTable = dbc.DS.Tables["Notice"];

            textBoxTitle.Text = dbc.NoticeTable.Rows[0]["NoticeTitle"].ToString();
            textBoxWriter.Text = dbc.NoticeTable.Rows[0]["NoticeWriter"].ToString();
            string startDate = "20" + dbc.NoticeTable.Rows[0]["NoticeStartDate"].ToString().Substring(0, 2) + "-" + dbc.NoticeTable.Rows[0]["NoticeStartDate"].ToString().Substring(2, 2) + "-" + dbc.NoticeTable.Rows[0]["NoticeStartDate"].ToString().Substring(4, 2);
            textBoxDate.Text = startDate;
            textBoxInfo.Text = dbc.NoticeTable.Rows[0]["NoticeInfo"].ToString();

            if(Convert.ToInt32(dbc.NoticeTable.Rows[0]["NoticeEndDate"]) == 999999)
            {
                textBoxEndDate.Text = "종료일 없음";
            }
            else
            {
                string endDate = "20" + dbc.NoticeTable.Rows[0]["NoticeEndDate"].ToString().Substring(0, 2) + "-" + dbc.NoticeTable.Rows[0]["NoticeEndDate"].ToString().Substring(2, 2) + "-" + dbc.NoticeTable.Rows[0]["NoticeEndDate"].ToString().Substring(4, 2);
                textBoxEndDate.Text = endDate;
            }

        }

        // 종료
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 수정
        private void button2_Click(object sender, EventArgs e)
        {
            CheckMasterPW checkMasterPW = new CheckMasterPW();
            checkMasterPW.FormNum = 3;
            checkMasterPW.NoticeWriter = textBoxWriter.Text;
            checkMasterPW.ShowDialog();

            int passwordOK = 1;
            // checkMasterPW 에서 로그인 성공시 checkMasterPW.PasswordOK = 1;
            if (passwordOK == checkMasterPW.PasswordOK)
            {
                textBoxTitle.ReadOnly = false;
                textBoxInfo.ReadOnly = false;
                textBoxEndDate.Visible = false;
                dateTimePicker1.Visible = true;
                checkBox1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                groupBox1.Text = "공지사항 수정";
                textBoxTitle.Focus();
                button1.Width = 207;
                button3.Width = 207;
                Point p = new Point(170, 289);
                button1.Location = p;
                button3.Location = p;

            }
            update = 1;
        }

        // 취소 버튼 (Visible false)
        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                dateTimePicker1.Enabled = false;
            }
            else if(checkBox1.Checked == false)
            {
                dateTimePicker1.Enabled = true;
            }
        }

        // 저장 후 종료 버튼 (Visible = false)
        private void button3_Click(object sender, EventArgs e)
        {
            dbc.Notice_Open();
            dbc.NoticeTable = dbc.DS.Tables["notice"];
            DataRow upRow = dbc.NoticeTable.Rows[dbc.NoticeTable.Rows.Count - (Convert.ToInt32(noticeID)+1)];
            upRow.BeginEdit();
            upRow["NoticeTitle"] = textBoxTitle.Text;
            if (checkBox1.Checked == true)
            {
                upRow["NoticeEndDate"] = "999999";
            }
            else if(checkBox1.Checked == false)
            {
                upRow["NoticeEndDate"] = dateTimePicker1.Value.ToString("yyMMdd");
            }
            upRow["NoticeInfo"] = textBoxInfo.Text;
            upRow.EndEdit();
            dbc.DBAdapter.Update(dbc.DS, "Notice");
            dbc.DS.AcceptChanges();

            MessageBox.Show("수정이 완료되었습니다.", "알림");
            Dispose();
        }

        // 삭제 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            dbc.Notice_Open();
            dbc.NoticeTable = dbc.DS.Tables["notice"];
            DataRow upRow = dbc.NoticeTable.Rows[dbc.NoticeTable.Rows.Count - (Convert.ToInt32(noticeID) + 1)];
            upRow.BeginEdit();
            upRow["NoticeEndDate"] = "0";
            upRow.EndEdit();
            dbc.DBAdapter.Update(dbc.DS, "Notice");
            dbc.DS.AcceptChanges();

            MessageBox.Show("삭제가 완료되었습니다.", "알림");
            Dispose();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int date1 = Convert.ToInt32(textBoxDate.Text.Substring(0, 4) + textBoxDate.Text.Substring(5, 2) + textBoxDate.Text.Substring(8, 2));
            int date2 = Convert.ToInt32(dateTimePicker1.Text.Substring(0, 4) + dateTimePicker1.Text.Substring(5, 2) + dateTimePicker1.Text.Substring(8, 2));
            if (date1 > date2)
            {
                MessageBox.Show("게시종료일은 게시일보다 빠를 수 없습니다.", "알림");
                dateTimePicker1.Value = DateTime.Now;
            }
        }
    }
}