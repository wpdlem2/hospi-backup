using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    class SignUp
    {
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string pw { get; set; }
        [FirestoreProperty]
        public string address { get; set; }
        [FirestoreProperty]
        public string[] department { get; set; }
        [FirestoreProperty]
        public string holidayClose { get; set; }
        [FirestoreProperty]
        public string holidayOpen { get; set; }
        [FirestoreProperty]
        public Boolean holidayStatus { get; set; }
        [FirestoreProperty]
        public string kind { get; set; }
        [FirestoreProperty]
        public string name { get; set; }
        [FirestoreProperty]
        public string saturdayClose { get; set; }
        [FirestoreProperty]
        public string saturdayOpen { get; set; }
        [FirestoreProperty]
        public Boolean saturdayStatus { get; set; }
        [FirestoreProperty]
        public Boolean status { get; set; }
        [FirestoreProperty]
        public string tel { get; set; }
        [FirestoreProperty]
        public Boolean todayReservation { get; set; }
        [FirestoreProperty]
        public string weekdayClose { get; set; }
        [FirestoreProperty]
        public string weekdayOpen { get; set; }

        public static Boolean IDCheck;
        

        FirestoreDb fs;

        string path;

        //Firestore 연결
        public void FireConnect()
        {
            FBKey fbKey = new FBKey();
            fbKey.DecryptFile();
            path = fbKey.TempKeyFilePath;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
            fbKey.DeleteTemp();
        }

        //ID 체크
        public async void IdCheck(string hospitalID)
        {
            Query qref = fs.Collection("hospitalAccountList").WhereEqualTo("id", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                DBClass fp = docsnap.ConvertTo<DBClass>();
                if(fp.id == hospitalID)
                {
                    MessageBox.Show("중복된 ID가 있습니다!", "알림");
                    IDCheck = false;
                }
                else
                {
                    MessageBox.Show("사용하실 수 있는 ID 입니다.", "알림");
                    IDCheck = true;
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

    }
}
