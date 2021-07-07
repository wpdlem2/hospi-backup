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
    public partial class Hospital_Setting : Form
    {
        DBClass dbc = new DBClass();
        string hospitalID; // 병원코드

        public Hospital_Setting()
        {
            InitializeComponent();
            dbc.FireConnect();
        }

        // 프로퍼티 
        public string HospitalID // Main폼에서 입력된 병원코드를 Reception을 거쳐서 받아옴
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        // 종료
        private void button1_Click(object sender, EventArgs e)
        {
            // 수정버튼 클릭하지 않고 종료시 바로 종료
            if (comboBox1.Enabled == false)
            {
                Dispose();
            }
            // 수정 상태일 경우 DB업데이트 후 종료
            else if (comboBox1.Enabled == true)
            {

                try
                {
                    DialogResult ok = MessageBox.Show("정보 수정을 완료 하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ok == DialogResult.Yes)
                    {
                        DBClass.hospiweekday_open = comboBox1.Text + ":" + comboBox2.Text;
                        DBClass.hospiweekday_close = comboBox3.Text + ":" + comboBox4.Text;
                        DBClass.hospisaturday_open = comboBox9.Text + ":" + comboBox8.Text;
                        DBClass.hospisaturday_close = comboBox7.Text + ":" + comboBox6.Text;
                        DBClass.hospiholiday_open = comboBox14.Text + ":" + comboBox13.Text;
                        DBClass.hospiholiday_close = comboBox12.Text + ":" + comboBox11.Text;
                        if (comboBox5.Text == "개원")
                        {
                            DBClass.hospisaturday_status = true;
                        }
                        else if (comboBox5.Text == "휴원")
                        {
                            DBClass.hospisaturday_status = false;
                        }
                        if (comboBox10.Text == "개원")
                        {
                            DBClass.hospiholiday_status = true;
                        }
                        else if (comboBox10.Text == "휴원")
                        {
                            DBClass.hospiholiday_status = false;
                        }

                        dbc.Hospital_Update();

                        Dispose();
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

        private void Hospital_Setting_Load(object sender, EventArgs e)
        {
            // 로드시 포커스 설정
            this.ActiveControl = button1;

            // DB오픈
            dbc.Hospital_Open(hospitalID);

            textBox1.Text = DBClass.hospiID;
            textBox2.Text = DBClass.hospiname;
            textBox3.Text = DBClass.hospikind;
            textBox4.Text = DBClass.hospiaddress;
            textBox5.Text = DBClass.hospitel;
            comboBox1.Text = DBClass.hospiweekday_open.ToString().Substring(0, 2);
            comboBox2.Text = DBClass.hospiweekday_open.ToString().Substring(3, 2);
            comboBox3.Text = DBClass.hospiweekday_close.ToString().Substring(0, 2);
            comboBox4.Text = DBClass.hospiweekday_close.ToString().Substring(3, 2);
            comboBox9.Text = DBClass.hospisaturday_open.ToString().Substring(0, 2);
            comboBox8.Text = DBClass.hospisaturday_open.ToString().Substring(3, 2);
            comboBox7.Text = DBClass.hospisaturday_close.ToString().Substring(0, 2);
            comboBox6.Text = DBClass.hospisaturday_close.ToString().Substring(3, 2);
            comboBox12.Text = DBClass.hospiholiday_close.ToString().Substring(0, 2);
            comboBox11.Text = DBClass.hospiholiday_close.ToString().Substring(3, 2);
            comboBox14.Text = DBClass.hospiholiday_open.ToString().Substring(0, 2);
            comboBox13.Text = DBClass.hospiholiday_open.ToString().Substring(3, 2);
            if (Convert.ToInt32(DBClass.hospisaturday_status) == 0)
            {
                comboBox5.Text = "휴원";
            }
            else if (Convert.ToInt32(DBClass.hospisaturday_status) == 1)
            {
                comboBox5.Text = "개원";
            }
            if (Convert.ToInt32(DBClass.hospitoday_reservation) == 1)
            {
                button18_Click(sender, e);
            }
            else if (Convert.ToInt32(DBClass.hospitoday_reservation) == 0)
            {
                button17_Click(sender, e);
            }
            if (Convert.ToInt32(DBClass.hospiholiday_status) == 0)
            {
                comboBox10.Text = "휴원";
            }
            else if (Convert.ToInt32(DBClass.hospiholiday_status) == 1)
            {
                comboBox10.Text = "개원";
            }
        }

        // 모바일 예약 재개
        private void button18_Click(object sender, EventArgs e)
        {
            // 라벨 
            label13.Text = "금일 모바일 예약 접수중";
            label13.ForeColor = Color.Black;

            // 버튼
            button18.Text = "▶ " + button18.Text + " ◀";
            button17.Text = "모바일 예약 마감";

            //DB
            try
            {
                DBClass.hospitoday_reservation = true;
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

        // 모바일 예약 마감
        private void button17_Click(object sender, EventArgs e)
        {
            // 라벨
            label13.Text = "금일 모바일 예약 마감";
            label13.ForeColor = Color.Red;

            // 버튼
            button17.Text = "▶ " + button17.Text + " ◀";
            button18.Text = "모바일 예약 재개";

            //DB
            try
            {
                DBClass.hospitoday_reservation = false;
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

        // 수정 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Enabled == false)
            {
                button2.Focus();
                // 수정클릭
                button2.Text = "저장 후 종료";
                button3.Enabled = false;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;
                comboBox7.Enabled = true;
                comboBox8.Enabled = true;
                comboBox9.Enabled = true;
                comboBox10.Enabled = true;
                comboBox11.Enabled = true;
                comboBox12.Enabled = true;
                comboBox13.Enabled = true;
                comboBox14.Enabled = true;
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (Convert.ToInt32(comboBox2.Text) > 59)
                {
                    comboBox2.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                if (Convert.ToInt32(comboBox4.Text) > 59)
                {
                    comboBox4.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox8_TextChanged(object sender, EventArgs e)
        {
            if (comboBox8.Text != "")
            {
                if (Convert.ToInt32(comboBox8.Text) > 59)
                {
                    comboBox8.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox6_TextChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text != "")
            {
                if (Convert.ToInt32(comboBox6.Text) > 59)
                {
                    comboBox6.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox13_TextChanged(object sender, EventArgs e)
        {
            if (comboBox13.Text != "")
            {
                if (Convert.ToInt32(comboBox13.Text) > 59)
                {
                    comboBox13.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text != "")
            {
                if (Convert.ToInt32(comboBox11.Text) > 59)
                {
                    comboBox11.Text = "00";
                    MessageBox.Show("0~59 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (Convert.ToInt32(comboBox1.Text) > 23)
                {
                    comboBox1.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                if (Convert.ToInt32(comboBox3.Text) > 23)
                {
                    comboBox3.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox9_TextChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text != "")
            {
                if (Convert.ToInt32(comboBox9.Text) > 23)
                {
                    comboBox9.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            if (comboBox7.Text != "")
            {
                if (Convert.ToInt32(comboBox7.Text) > 23)
                {
                    comboBox7.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox14_TextChanged(object sender, EventArgs e)
        {
            if (comboBox14.Text != "")
            {
                if (Convert.ToInt32(comboBox14.Text) > 23)
                {
                    comboBox14.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }

        private void comboBox12_TextChanged(object sender, EventArgs e)
        {
            if (comboBox12.Text != "")
            {
                if (Convert.ToInt32(comboBox12.Text) > 23)
                {
                    comboBox12.Text = "00";
                    MessageBox.Show("0~23 사이의 숫자만 입력할 수 있습니다.");
                }
            }
        }
    }
}
