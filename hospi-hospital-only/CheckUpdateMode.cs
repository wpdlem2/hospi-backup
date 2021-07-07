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
    public partial class CheckUpdateMode : Form
    {
        public CheckUpdateMode()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateReceptionist updateReceptionist = new UpdateReceptionist();
            updateReceptionist.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateSubject updateSubject = new UpdateSubject();
            updateSubject.ShowDialog();
        }
    }
}
