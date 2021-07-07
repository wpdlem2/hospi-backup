using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    public partial class Hospital_SignUp : Form
    {
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string pw { get; set; }

        int listBoxIndex;
        int iDCheck;
        DBClass dbc = new DBClass();
        string SHApw;
        
        Boolean holistate;
        Boolean endState;
        Boolean status;
        List<string> department = new List<string>();

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";

        FirestoreDb fs;

        public Hospital_SignUp()
        {
            InitializeComponent();
        }

        private void Hospital_SignUp_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxHospitalID;
            FireConnect();
            dbc.Delay(400);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            // DB 조회후 중복되지 않을경우 iDCheck 값 1로 변경
            if (textBoxHospitalID.Text == "" && textBoxHospitalID.Text == " ")
            {
                MessageBox.Show("ID를 입력해주세요", "알림");
                textBoxHospitalID.Focus();
            }
            else
            {
                
                Check(textBoxHospitalID.Text);
                dbc.Delay(1000);
                if (iDCheck == 1)
                {
                    MessageBox.Show("사용 가능한 ID입니다!", "알림");
                    IDCheck.BackColor = Color.Green;
                }
            }
        }

        //Firestore 연결
        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }


        //ID 체크
        public async void Check(string hospitalID)
        {
            Query qref = fs.Collection("hospitalAccountList").WhereEqualTo("id", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();


            iDCheck = 1;

            foreach (DocumentSnapshot docsnap in snap)
            {
                DBClass fp = docsnap.ConvertTo<DBClass>();
                if (fp.id == hospitalID)
                {
                    MessageBox.Show("중복된 ID가 있습니다!", "알림");
                    iDCheck = 0;
                    break;
                }
            }
        }


        //ID추가
        public void IdAdd(string hospitalID, string pass)
        {
            CollectionReference coll = fs.Collection("hospitalAccountList");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"id", hospitalID},
                {"pw", pass }
            };
            coll.AddAsync(data1);
        }

        //과 추가
        public void AddDepartment()
        {
            if (checkBox1.Checked)
                department.Add(checkBox1.Text);
            if (checkBox2.Checked)
                department.Add(checkBox2.Text);
            if (checkBox3.Checked)
                department.Add(checkBox3.Text);
            if (checkBox4.Checked)
                department.Add(checkBox4.Text);
            if (checkBox5.Checked)
                department.Add(checkBox5.Text);
            if (checkBox6.Checked)
                department.Add(checkBox6.Text);
            if (checkBox7.Checked)
                department.Add(checkBox7.Text);
            if (checkBox8.Checked)
                department.Add(checkBox8.Text);
            if (checkBox9.Checked)
                department.Add(checkBox9.Text);
            if (checkBox10.Checked)
                department.Add(checkBox10.Text);
            if (checkBox11.Checked)
                department.Add(checkBox11.Text);
            if (checkBox12.Checked)
                department.Add(checkBox12.Text);
            for(int i=0; i<listBox1.Items.Count; i++)
            {
                department.Add(listBox1.Items[i].ToString());
            }
        }

        //Hospital추가
        public void AddHospital()
        {
            if (HoliState.Text == "휴원")
            { holistate = false; }
            else if (HoliState.Text == "개원")
            { holistate = true; }

            if (EndState.Text == "휴원")
            { endState = false; }
            else if (EndState.Text == "개원")
            { endState = true; }

            
            CollectionReference coll = fs.Collection("hospitals");
            if (textBoxAddAddress.Text == "" && textBoxAddAddress.Text == " ")
            {
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                {"address", textBoxHospitalAddress.Text},
                {"department", department},
                {"holidayClose", HoliClose1.Text + ":" + HoliClose2.Text },
                {"holidayOpen",  holiOpen1.Text + ":" + holiOpen2.Text},
                {"holidayStatus", holistate},
                {"id", textBoxHospitalID.Text},
                {"kind", HospitalType.Text},
                {"lunchTime", lunch1.Text + ":" + lunch2.Text },
                {"name", textBoxHospitalName.Text},
                {"saturdayClose", EndClose1.Text + ":" + EndClose2.Text},
                {"saturdayOpen", EndOpen1.Text + ":" + EndOpen2.Text },
                {"saturdayStatus", endState},
                {"status", true},
                {"tel", textBoxTell1.Text + "-" + textBoxTell2.Text + "-" + textBoxTell3.Text },
                {"todayReservation", true},
                {"weekdayClose", DayClose1.Text + ":" + DayClose2.Text},
                {"weekdayOpen", DayOpen1.Text + ":" + DayOpen2.Text}
                };
                coll.AddAsync(data1);
            }
            else if (textBoxAddAddress.Text != "" && textBoxAddAddress.Text != " ")
            {
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                {"address", textBoxHospitalAddress.Text + " " + textBoxAddAddress.Text},
                {"department", department},
                {"holidayClose", HoliClose1.Text + ":" + HoliClose2.Text },
                {"holidayOpen",  holiOpen1.Text + ":" + holiOpen2.Text},
                {"holidayStatus", holistate},
                {"id", textBoxHospitalID.Text},
                {"kind", HospitalType.Text},
                {"lunchTime", lunch1.Text + ":" + lunch2.Text },
                {"name", textBoxHospitalName.Text},
                {"saturdayClose", EndClose1.Text + ":" + EndClose2.Text},
                {"saturdayOpen", EndOpen1.Text + ":" + EndOpen2.Text },
                {"saturdayStatus", endState},
                {"status", true},
                {"tel", textBoxTell1.Text + "-" + textBoxTell2.Text + "-" + textBoxTell3.Text },
                {"todayReservation", true},
                {"weekdayClose", DayClose1.Text + ":" + DayClose2.Text},
                {"weekdayOpen", DayOpen1.Text + ":" + DayOpen2.Text}
                };
                coll.AddAsync(data1);
            }
        }

        //PW1
        private void textBoxPw1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPw1.Text.Length >= 4)
            {
                pwLabel1.Text = "✓";
                pwLabel1.ForeColor = Color.Green;
            }
            else if (textBoxPw1.Text.Length < 4)
            {
                pwLabel1.Text = "X";
                pwLabel1.ForeColor = Color.Red;
            }

            if (textBoxPw1.Text != textBoxPw2.Text)
            {
                pwLabel2.Text = "X";
                pwLabel2.ForeColor = Color.Red;
            }
            else if (textBoxPw1.Text == textBoxPw2.Text)
            {
                pwLabel2.Text = "✓";
                pwLabel2.ForeColor = Color.Green;
            }
        }

        //PW2
        private void textBoxPw2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPw2.Text == textBoxPw1.Text && pwLabel1.Text == "✓")
            {
                pwLabel2.Text = "✓";
                pwLabel2.ForeColor = Color.Green;
            }
            else if (textBoxPw2.Text != textBoxPw1.Text || pwLabel1.Text == "X")
            {
                pwLabel2.Text = "X";
                pwLabel2.ForeColor = Color.Red;
            }
        }

        //기타 항목 추가
        private void buttonSubjectAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBoxEtc.Text);
            textBoxEtc.Clear();
        }

        //기타 항목 제거
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBoxIndex);
        }
        
        //선택된 기타 항목
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxIndex = listBox1.SelectedIndex;
        }

        //등록완료 버튼
        private void button2_Click(object sender, EventArgs e)
        {

            // iDCheck 값이 중복확인후 1로 변경되어야 진행
            if (iDCheck == 1 && pwLabel1.Text == "✓" && pwLabel2.Text == "✓")
            {
                //병원정보 확인
                if (textBoxHospitalName.Text != "" && HospitalType.Text != "" && textBoxTell1.Text != "" && textBoxTell2.Text != "" && textBoxTell3.Text != "" && textBoxHospitalAddress.Text != "")
                {
                    //영업시간 확인
                    if (DayOpen1.Text != "" && DayOpen2.Text != "" && DayClose1.Text != "" && DayClose2.Text != "" && EndClose1.Text != "" && EndClose2.Text != "" && EndOpen1.Text != "" && EndOpen2.Text != "")
                    {
                        //영업시간 확인2
                        if (lunch1.Text != "" && lunch2.Text != "" & holiOpen1.Text != "" && holiOpen2.Text != "" && HoliClose1.Text != "" && HoliClose2.Text != "" && HoliState.Text != "" && EndState.Text != "")
                        {
                            AddDepartment();
                            if (department.Count == 0)
                            {
                                MessageBox.Show("진료 과를 선택해주세요.", "알림");
                            }
                            else
                            {
                                SHApw = dbc.SHA256Hash(textBoxPw1.Text, textBoxHospitalID.Text);

                                IdAdd(textBoxHospitalID.Text, SHApw);
                                dbc.Delay(200);
                                AddHospital();
                                MessageBox.Show("회원가입이 완료되었습니다.", "알림");
                                Dispose();
                            }
                        }
                        else { MessageBox.Show("영업시간을 확인해주세요.", "알림"); }
                    }
                    else { MessageBox.Show("영업시간을 확인해주세요.", "알림"); }
                }
                else { MessageBox.Show("병원정보를 확인해주세요.", " 알림"); }
            }
            else { MessageBox.Show("아이디 혹은 패스워드를 확인해주세요.", " 알림"); }
        }

        //ID텍스트 박스 변경시
        private void textBoxHospitalID_TextChanged(object sender, EventArgs e)
        {
            iDCheck = 0;
            IDCheck.BackColor = SystemColors.Control;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        //주소 검색버튼 클릭
        private void button4_Click_1(object sender, EventArgs e)
        {
            SearchAddress frm = new SearchAddress();
            frm.ShowDialog();

            if(frm.Tag == null) { return; }
            DataRow dr = (DataRow)frm.Tag;

            textBoxAdCode.Text = dr["zonecode"].ToString();
            textBoxHospitalAddress.Text = dr["ADDR1"].ToString() + " " + dr["EX"].ToString();
            textBoxAddAddress.Text = "";

            textBoxAddAddress.Focus();
        }
    }
}
