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
    public partial class PDFTest : Form
    {
        public static Bitmap bmpScreenCapture;

        public PDFTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmpScreenCapture))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, 
                                              Screen.PrimaryScreen.Bounds.Y, 0, 0, bmpScreenCapture.Size, 
                                              CopyPixelOperation.SourceCopy);
            }

        }
    }
}
