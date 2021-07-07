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
    public partial class UpdateSubject : Form
    {
        DBClass dbc = new DBClass();

        public UpdateSubject()
        {
            InitializeComponent();
        }

        private void UpdateSubject_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.Subject_Open();
                dbc.SubjectTable = dbc.DS.Tables["SubjectName"];
                for(int i=0; i<dbc.SubjectTable.Rows.Count; i++)
                {
                    string name = dbc.SubjectTable.Rows[i]["SubjectName"].ToString();
                    int length = name.Length;

                    if (name.Substring(length - 1) != ")")
                    {
                        listBoxSubject.Items.Add(dbc.SubjectTable.Rows[i]["SubjectName"]);
                    }
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

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
