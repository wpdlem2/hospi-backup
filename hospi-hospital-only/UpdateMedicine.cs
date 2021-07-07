using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace hospi_hospital_only
{
    public partial class UpdateMedicine : Form
    {
        DBClass dbc = new DBClass();
        public string item_name = "";

        public UpdateMedicine()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void UpdateMedicine_Load(object sender, EventArgs e)
        {
            dbc.Medicine_Open();
            dbc.MedicineTable = dbc.DS.Tables["medicine"];
            //label1.Text = " 마지막 업데이트 : " + dbc.MedicineTable.Rows[0]["medicineUpdate"].ToString();
        }


        public void DeleteMedicine()
        {
            DialogResult ok = MessageBox.Show(" 약품정보를 업데이트 하시겠습니까? \r\n 이 작업은 최대 10분 소요됩니다. \r\n 강제로 종료하지 마세요.", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                button5.Enabled = false;
                button1.Enabled = false;
                label1.Text = "업데이트 진행중입니다. 강제로 종료하지 마세요.";

                // 삭제
                for (int i = 0; i < dbc.MedicineTable.Rows.Count; i++)
                {
                    DataColumn[] PrimaryKey = new DataColumn[1];
                    PrimaryKey[0] = dbc.MedicineTable.Columns["medicine"];
                    dbc.MedicineTable.PrimaryKey = PrimaryKey;
                    DataRow delRow = dbc.MedicineTable.Rows[i];
                    delRow.Delete();
                }
                dbc.DBAdapter.Update(dbc.DS, "medicine");
                dbc.DS.AcceptChanges();

                MedicineLoad();
            }
        }


        private async void MedicineLoad()
        {
            int aaa = 0;
            // 추가
            await Task.Run(() => {
                for (int i = 1; i < 100; i++)
                {
                    string url = "http://apis.data.go.kr/1470000/MdcinGrnIdntfcInfoService/getMdcinGrnIdntfcInfoList?ServiceKey=jgj5koEUBWf6YgyDdiDqk2jre4EIbOEXoyAF5JSYUFM7ZEM563jRAWIokqD9H8GWV8suKbPyUKp9vP9/Q3I/yg==&item_name=" + item_name + "&pageNo=" + i + "&numOfRows=100"; // URL

                    XmlDocument xml = new XmlDocument();

                    xml.Load(url);

                    XmlNodeList xnList = xml.SelectNodes("/response/body/items/item");
                    foreach (XmlNode xn in xnList)
                    {
                        try
                        {
                            //aaa++;
                            dbc.Medicine_Open();
                            dbc.MedicineTable = dbc.DS.Tables["medicine"];
                            DataRow newRow = dbc.MedicineTable.NewRow();
                            newRow["medicineID"] = dbc.MedicineTable.Rows.Count;
                            newRow["medicineCode"] = xn["ITEM_SEQ"].InnerText;
                            newRow["medicineName"] = xn["ITEM_NAME"].InnerText;
                            newRow["medicineUpdate"] = DateTime.Now.ToString("yyyy-MM-dd");
                            dbc.MedicineTable.Rows.Add(newRow);
                            dbc.DBAdapter.Update(dbc.DS, "medicine");
                            dbc.DS.AcceptChanges();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            });
            MessageBox.Show(aaa.ToString());
            MessageBox.Show("업데이트가 완료되었습니다.", "알림");
           // label1.Text = "업데이트 완료 : " + dbc.MedicineTable.Rows[0]["medicineUpdate"].ToString();
            button1.Enabled = true;
        }

        // 업데이트 시작
        private void button5_Click(object sender, EventArgs e)
        {
            DeleteMedicine();
            //MedicineLoad();
        }

    }
}
