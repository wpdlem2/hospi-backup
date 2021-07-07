using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class AddRadiography : Form
    {
        DBClass dbc = new DBClass();
        string location;
        string initialDirectory;
        string selectPatient;
        string receptionID;

        Image newImage;


        public AddRadiography()
        {
            InitializeComponent();
        }

        //  image > byte[] 변환
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            dbc.Reception_Radiography(dateTimePicker1.Value.ToString("yy-MM-dd"));
            dbc.ReceptionTable = dbc.DS.Tables["Reception"];

            
            for (int i = 0; i < dbc.ReceptionTable.Rows.Count; i++)
            {
                ListViewItem items = new ListViewItem();

                items.Text = (listView1.Items.Count + 1).ToString();
                items.SubItems.Add(dbc.ReceptionTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + dbc.ReceptionTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                items.SubItems.Add(dbc.ReceptionTable.Rows[i]["patientID"].ToString());
                items.SubItems.Add(dbc.ReceptionTable.Rows[i]["patientName"].ToString());
                items.SubItems.Add(dbc.ReceptionTable.Rows[i]["subjectName"].ToString());
                items.SubItems.Add(dbc.ReceptionTable.Rows[i]["receptionID"].ToString());

                listView1.Items.Add(items);
            }

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            pictureBox1.Image = null;
        }

        // 셀크기변경 막기
        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void Radiography_Load(object sender, EventArgs e)
        {
            // User\Documents 폴더
            string saveLocation = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\HospiDirectory";

            if (Properties.Settings.Default.SaveLocation == "" )
            {
                DirectoryInfo di = new DirectoryInfo(@saveLocation);
                if (di.Exists == false)
                {
                    Directory.CreateDirectory(@saveLocation);
                }
                Properties.Settings.Default.SaveLocation = saveLocation;
                Properties.Settings.Default.Save();
                textBox1.Text = Properties.Settings.Default.SaveLocation;

            }
            else if (Properties.Settings.Default.SaveLocation != "")
            {
                textBox1.Text = Properties.Settings.Default.SaveLocation;
            }

            button1_Click(sender, e);
            
        }

        // 기본경로 수정
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            string path = folderBrowserDialog.SelectedPath;

            if(path != "")
            {
                Properties.Settings.Default.SaveLocation = path;
                Properties.Settings.Default.Save();
                textBox1.Text = Properties.Settings.Default.SaveLocation;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DirectoryInfo di = new DirectoryInfo(@textBox1.Text);
            if(di.Exists == true)
            {
                openFileDialog.InitialDirectory = @textBox1.Text;
                openFileDialog.Filter = "이미지 파일(*.jpg; *.jpeg; *.gif; *.bmp; *.png) |*.jpg; *.jpeg; *.gif; *.bmp; *.png ";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    textBox2.Text = openFileDialog.FileName;
                    // 사진 띄우기
                    Image image = Image.FromFile(textBox2.Text);
                    pictureBox1.Image = image;
                }
            }
            else
            {
                MessageBox.Show("지정 경로가 존재하지 않습니다.", "알림");
                button3_Click(sender, e);
            }
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                selectPatient = listView1.Items[listView1.FocusedItem.Index].Text;
                receptionID = listView1.Items[listView1.FocusedItem.Index].SubItems[5].Text;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectPatient != "")
            {
                textBox3.Text = listView1.Items[Convert.ToInt32(selectPatient)-1].SubItems[2].Text;
                textBox4.Text = listView1.Items[Convert.ToInt32(selectPatient)-1].SubItems[3].Text;

                button2.Enabled = true;
            }
        }

        // 등록
        private void button4_Click_1(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                Image image = Image.FromFile(textBox2.Text);
                byte[] rawData = ImageToByteArray(image);

                dbc.Image_Open();
                dbc.ImageTable = dbc.DS.Tables["Image"];

                DataRow newRow = dbc.ImageTable.NewRow();
                newRow["ImageID"] = dbc.ImageTable.Rows.Count;
                newRow["patientID"] = textBox3.Text;
                newRow["ImageDate"] = dateTimePicker1.Value.ToString("yy-MM-dd");
                newRow["ImageSource"] = rawData;

                dbc.ImageTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "Image");
                dbc.DS.AcceptChanges();

                // 보류상태에서 진료대기로
                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["reception"];

                DataRow upRow = dbc.ReceptionTable.Rows[Convert.ToInt32(receptionID)-1];
                upRow.BeginEdit();
                upRow["receptionType"] = 5;
                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "reception");
                dbc.DS.AcceptChanges();

                MessageBox.Show("저장이 완료되었습니다", "알림");

                button1_Click(sender, e);
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                pictureBox1.Image = null;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("파일이 선택되지 않았습니다.", "알림");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            pictureBox1.Image = null;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            pictureBox1.Image = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocation = "";
            Properties.Settings.Default.Save();
        }
    }
}
