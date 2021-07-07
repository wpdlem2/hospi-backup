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
    public partial class ReserveCancel : Form
    {
        Reserve reserve = new Reserve();
        DBClass dbc = new DBClass();
        Fcm fcm = new Fcm();
        Reservation reservation = new Reservation();

        public int receptionIndex;
        public string hospitalID;
        public string date;
        public string time;
        public string status;

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public ReserveCancel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Trim() != "")
            {
                if (status == "승인됨")
                {
                    try
                    {
                        
                        reserve.RemoveReserveTime(date, time);

                        reserve.ReserveCancel(richTextBox1.Text);
                        MessageBox.Show("예약이 취소되었습니다.", "알림");

                        fcm.PushNotificationToFCM(DBClass.hospiname, Reserve.UserToken, "[" + date + " " + FindDay(date) + " " + time + "] " + " 예약이 취소되었습니다.");

                        dbc.Delay(200);

                        this.DialogResult = DialogResult.OK;
                        //Dispose();

                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (status == "대기")
                {
                    reserve.RemoveReserveTime(date, time);
                    dbc.Delay(200);
                    reserve.ReserveCancel(richTextBox1.Text);

                    fcm.PushNotificationToFCM(DBClass.hospiname, Reserve.UserToken, "[" + date + " " + FindDay(date) + " " + time + "] " + " 예약이 취소되었습니다.");

                    MessageBox.Show("예약이 취소되었습니다.", "알림");

                    this.DialogResult = DialogResult.OK;
                }
                else if (status == "취소됨")
                {
                    MessageBox.Show("이미 취소된 예약입니다.", "알림");

                    Dispose();
                }
            }
            else
            {
                MessageBox.Show("취소 사유를 입력해주세요.", "알림");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        public string FindDay(string Date)
        {
            string day = "";
            DateTime date = new DateTime();
            date = Convert.ToDateTime(Date);

            if (date.DayOfWeek == DayOfWeek.Monday)
                day = "(월)";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                day = "(화)";
            else if (date.DayOfWeek == DayOfWeek.Wednesday)
                day = "(수)";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                day = "(목)";
            else if (date.DayOfWeek == DayOfWeek.Friday)
                day = "(금)";
            else if (date.DayOfWeek == DayOfWeek.Saturday)
                day = "(토)";
            else if (date.DayOfWeek == DayOfWeek.Sunday)
                day = "(일)";

            return day;
        }

        private void ReserveCancel_Load(object sender, EventArgs e)
        {
            reserve.FireConnect();
            reserve.ReserveOpen(hospitalID);
            dbc.Delay(400);
            
        }
    }
}
