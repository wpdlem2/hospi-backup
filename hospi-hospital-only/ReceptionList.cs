using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Microsoft.Toolkit.Uwp.Notifications;

namespace hospi_hospital_only
{
    [FirestoreData]
    class ReceptionList
    {
        [FirestoreProperty]
        public string department { get; set; }
        [FirestoreProperty]
        public string doctor { get; set; }
        [FirestoreProperty]
        public string hospitalID { get; set; }
        [FirestoreProperty]
        public string hospitalName { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string office { get; set; }
        [FirestoreProperty]
        public string patient { get; set; }
        [FirestoreProperty]
        public string receptionDate { get; set; }
        [FirestoreProperty]
        public int status { get; set; }
        [FirestoreProperty]
        public int waitingNumber { get; set; }
        [FirestoreProperty]
        public string receptionTime { get; set; }



        public string today = DateTime.Now.ToString("yyyy-MM-dd");
        public FirestoreDb fs;
        public string documentName;
        string path;

        public List<ReceptionList> list = new List<ReceptionList>();

        public void FireConnect()
        {
            FBKey fbKey = new FBKey();
            fbKey.DecryptFile();
            path = fbKey.TempKeyFilePath;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
            fbKey.DeleteTemp();
        }

        async public void TodayReceptionOpen(string hospitalId, string department)
        {
            Query qref = fs.Collection("receptionList").WhereEqualTo("hospitalId", hospitalId).WhereEqualTo("department", department).WhereEqualTo("receptionDate", today);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                ReceptionList fp = docsnap.ConvertTo<ReceptionList>();
                if (docsnap.Exists)
                {
                    ReceptionList receptionlist = fp;
                    list.Add(receptionlist);

                }
            }
        }
        //예약 접수
        public void ReceptionAccept(string depart, string doctor, string id, string name, string Date,string Time, int number)
        {
            CollectionReference coll = fs.Collection("receptionList");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"department", depart },
                {"doctor", doctor },
                {"hospitalId", DBClass.hospiID },
                {"hospitalName", DBClass.hospiname },
                {"id", id },
                {"office", "진료1실" },
                {"patient", name },
                {"receptionDate", Date },
                {"receptionTime", Time },
                {"waitingNumber", number }
             };
            coll.AddAsync(data1);
        }

        async public void watingNumberUpdate(int waitingNumber)
        {
            try
            {
                DocumentReference docref = fs.Collection("receptionList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"waitingNumber", waitingNumber},
            };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch
            {

            }
        }

        //문서 이름찾기
        async public void FindDocument(string hospitalId, string receptionDate, string receptionTime, string department)
        {
            documentName = null;
            Query qref = fs.Collection("receptionList").WhereEqualTo("hospitalId", hospitalId).WhereEqualTo("receptionDate", receptionDate).WhereEqualTo("receptionTime", receptionTime).WhereEqualTo("department", department);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                ReceptionList fp = docsnap.ConvertTo<ReceptionList>();
                if (docsnap.Exists)
                {
                    documentName = docsnap.Id;

                }
            }
        }


        public void Delete_Reception()
        {
            try 
            {
                DocumentReference docref = fs.Collection("receptionList").Document(documentName);
                docref.DeleteAsync();
            }
            catch
            {

            }
            
        }


    }
}
