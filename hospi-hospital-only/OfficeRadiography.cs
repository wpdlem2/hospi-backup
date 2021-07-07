using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace hospi_hospital_only
{
    public partial class OfficeRadiography : Form
    {
        DBClass dbc = new DBClass();
        Image returnImage;
        Image newImage;

        // 마우스 이벤트 변수
        Point imgPoint;
        Rectangle imgRect;
        double ratio = 1.0F;
        Point clickPoint;
        Point LastPoint;

        // 변수
        string date;
        string patientID;
        string patientName;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        public OfficeRadiography()
        {
            InitializeComponent();

            pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);

            imgPoint = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            imgRect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            ratio = 1.0;
            clickPoint = imgPoint;

            pictureBox1.Invalidate();
        }

        // byte[] > image 변환
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = Image.FromStream(ms, true);
            }
            catch { }
            return returnImage;
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                ratio *= 1.1F;
                if (ratio > 100.0) ratio = 100.0f;

                imgRect.Width = (int)Math.Round(pictureBox1.Width * ratio);
                imgRect.Height = (int)Math.Round(pictureBox1.Height * ratio);
                imgRect.X = -(int)Math.Round(1.1F * (imgPoint.X - imgRect.X) - imgPoint.X);
                imgRect.Y = -(int)Math.Round(1.1F * (imgPoint.Y - imgRect.Y) - imgPoint.Y);
            }
            else if (lines < 0)
            {
                ratio *= 0.9F;
                if (ratio < 1) ratio = 1;

                imgRect.Width = (int)Math.Round(pictureBox1.Width * ratio);
                imgRect.Height = (int)Math.Round(pictureBox1.Height * ratio);
                imgRect.X = -(int)Math.Round(0.9F * (imgPoint.X - imgRect.X) - imgPoint.X);
                imgRect.Y = -(int)Math.Round(0.9F * (imgPoint.Y - imgRect.Y) - imgPoint.Y);
            }

            if (imgRect.X > 0) imgRect.X = 0;
            if (imgRect.Y > 0) imgRect.Y = 0;
            if (imgRect.X + imgRect.Width < pictureBox1.Width) imgRect.X = pictureBox1.Width - imgRect.Width;
            if (imgRect.Y + imgRect.Height < pictureBox1.Height) imgRect.Y = pictureBox1.Height - imgRect.Height;
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void OfficeRadiography_Load(object sender, EventArgs e)
        {
            textBox1.Text = patientID;
            textBox2.Text = patientName;

            dbc.Image_Open(patientID, date);
            dbc.ImageTable = dbc.DS.Tables["Image"];

            byte[] imageByte = (byte[])dbc.ImageTable.Rows[0]["imageSource"];
            newImage = byteArrayToImage(imageByte);

            pictureBox1.Image = newImage;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                e.Graphics.DrawImage(pictureBox1.Image, imgRect);
                pictureBox1.Focus();
            }
        }

        // zoom시점 사진 이동
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickPoint = new Point(e.X, e.Y);
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X) / 5);
                if (imgRect.X >= 0) imgRect.X = 0;
                if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - pictureBox1.Width)) imgRect.X = -(imgRect.Width - pictureBox1.Width);
                imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);
                if (imgRect.Y >= 0) imgRect.Y = 0;
                if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - pictureBox1.Height)) imgRect.Y = -(imgRect.Height - pictureBox1.Height);
            }
            else
            {
                LastPoint = e.Location;
            }

            pictureBox1.Invalidate();
        }
    }
}
