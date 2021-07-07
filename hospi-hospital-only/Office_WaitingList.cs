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
    public partial class Office_WaitingList : Form
    {
        DBClass dbc = new DBClass();
        DataTable watingTable;
        string date;
        string subjectID;
        int patientEnter;   // 현재 office에서 진료중인 환자가 있으면 1, 진료중인 환자가 없으면 0 

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string SubjectID
        {
            get { return subjectID; }
            set { subjectID = value; }
        }
        public int PatientEnter
        {
            get { return patientEnter; }
            set { patientEnter = value; }
        }

        public Office_WaitingList()
        {
            InitializeComponent();
        }

        public DataTable WatingTable
        {
            get { return watingTable; }
            set { watingTable = value; }
        }

        private void Office_WaitingList_Load(object sender, EventArgs e)
        {
            textBoxSubName.Text = subjectID;

            dbc.Reception_Office2(date, subjectID);
            dbc.ReceptionTable = dbc.DS.Tables["reception"];

            if (dbc.ReceptionTable.Rows.Count != 0)
            {
                for (int i = 0; i < dbc.ReceptionTable.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = (listView1.Items.Count + 1).ToString("00");
                    item.SubItems.Add(dbc.ReceptionTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + dbc.ReceptionTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                    item.SubItems.Add(dbc.ReceptionTable.Rows[i]["patientID"].ToString());
                    item.SubItems.Add(dbc.ReceptionTable.Rows[i]["patientName"].ToString());
                    // Age
                    int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                    if (dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "1" || dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "2" || dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "0")
                    {
                        item.SubItems.Add((year - Convert.ToInt32(dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString());
                    }
                    else if (dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "3" || dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "4" || dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "5")
                    {
                        item.SubItems.Add((year - Convert.ToInt32(dbc.ReceptionTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString());
                    }
                    item.SubItems.Add(dbc.ReceptionTable.Rows[i]["receptionistName"].ToString());
                    item.SubItems.Add("의료영상 등록");
                    listView1.Items.Add(item);
                }
            }
            for (int i = 0; i < watingTable.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (listView1.Items.Count + 1).ToString("00");
                item.SubItems.Add(watingTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + watingTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                item.SubItems.Add(watingTable.Rows[i]["patientID"].ToString());
                item.SubItems.Add(watingTable.Rows[i]["patientName"].ToString());
                // Age
                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "1" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "2" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "0")
                {
                    item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString());
                }
                else if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "3" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "4" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "5")
                {
                    item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString());
                }
                item.SubItems.Add(watingTable.Rows[i]["receptionistName"].ToString());
                item.SubItems.Add("일반 진료 대기중");
                listView1.Items.Add(item);
            }

            if (patientEnter == 0)
            {
            }
            else if (patientEnter == 1)
            {
                listView1.Items[0].BackColor = Color.Yellow;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        // 컬럼 크기변경 막기
        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
