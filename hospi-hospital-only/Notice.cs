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
    public partial class Notice : Form
    {
        DBClass dbc = new DBClass();
        string writer;

        public string Writer
        {
            get { return writer; }
            set { writer = value; }
        }

        public Notice()
        {
            InitializeComponent();
        }

        private void Notice_Load(object sender, EventArgs e)
        {
            textBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBoxWriter.Text = writer;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePickerEndDate.Enabled = false;
                dateTimePickerEndDate.Value = DateTime.Now;
            }
            else if (checkBox1.Checked == false)
            {
                dateTimePickerEndDate.Enabled = true;
            }
        }

        private void textBoxTitle_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text == "제목을 입력하세요.")
            {
                textBoxTitle.SelectAll();
            }
        }

        private void textBoxInfo_Click(object sender, EventArgs e)
        {
            if (textBoxInfo.Text == "내용을 입력하세요.")
            {
                textBoxInfo.SelectAll();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text == "제목을 입력하세요.")
            {
                MessageBox.Show("제목을 입력해주세요.", "알림");
            }
            else if (textBoxInfo.Text == "내용을 입력하세요.")
            {
                MessageBox.Show("내용을 입력해주세요.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("공지사항을 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc.Notice_Open();
                        //dbc.DBAdapter.Fill(dbc.DS, "notice");
                        dbc.NoticeTable = dbc.DS.Tables["notice"];
                        DataRow newRow = dbc.NoticeTable.NewRow();

                        newRow["NoticeID"] = dbc.NoticeTable.Rows.Count;
                        newRow["NoticeTitle"] = textBoxTitle.Text;
                        newRow["NoticeInfo"] = textBoxInfo.Text;
                        newRow["NoticeStartDate"] = textBoxStartDate.Text.Substring(2, 2) + textBoxStartDate.Text.Substring(5, 2) + textBoxStartDate.Text.Substring(8, 2);
                        if (checkBox1.Checked == true)
                        {
                            newRow["NoticeEndDate"] = "999999";
                        }
                        else if (checkBox1.Checked == false)
                        {
                            newRow["NoticeEndDate"] = dateTimePickerEndDate.Value.ToString("yyMMdd");
                        }
                        newRow["NoticeWriter"] = textBoxWriter.Text;

                        dbc.NoticeTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "Notice");
                        dbc.DS.AcceptChanges();

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
            }
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            int date1 = Convert.ToInt32(textBoxStartDate.Text.Substring(0, 4) + textBoxStartDate.Text.Substring(5, 2) + textBoxStartDate.Text.Substring(8, 2));
            int date2 = Convert.ToInt32(dateTimePickerEndDate.Text.Substring(0, 4) + dateTimePickerEndDate.Text.Substring(5, 2) + dateTimePickerEndDate.Text.Substring(8, 2));
            if (date1 > date2)
            {
                MessageBox.Show("게시종료일은 게시일보다 빠를 수 없습니다.", "알림");
                dateTimePickerEndDate.Value = DateTime.Now;
            }
        }
    }
}

