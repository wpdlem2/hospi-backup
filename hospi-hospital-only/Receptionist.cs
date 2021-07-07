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
    public partial class Receptionist : Form
    {
        DBClass dbc = new DBClass();
        int hospitalID;
        string receptionistName;

       
        public string ReceptionistName
        {
            get { return receptionistName; }
            set { receptionistName = value; }
        }

        public Receptionist()
        {
            InitializeComponent();
        }

        private void Receptionist_Load(object sender, EventArgs e)
        {
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["Receptionist"];

            for(int i=0; i<dbc.ReceptionistTable.Rows.Count; i++)     // comboBox1에 접수자 추가
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;

                if (name.Substring(length - 1) != ")")
                {
                    comboBox1.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }
            comboBox1.Text = receptionistName;
        }

        // 변경 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            receptionistName = comboBox1.Text;
            Dispose();
        }

        private void settingLabel_Click(object sender, EventArgs e)
        {
            UpdateReceptionist updateReceptionist = new UpdateReceptionist();
            updateReceptionist.ShowDialog();

            if (updateReceptionist == null || updateReceptionist.IsDisposed)
            {
                comboBox1.Items.Clear();
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)     // comboBox1에 접수자 추가
                {
                    string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                    int length = name.Length;

                    if (name.Substring(length - 1) != ")")
                    {
                        comboBox1.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                    }
                }
                comboBox1.Text = receptionistName;
            }
        }

        // 취소 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
