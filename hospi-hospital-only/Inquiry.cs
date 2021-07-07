using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Toolkit.Uwp.Notifications;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Inquiry
    {
        [FirestoreProperty]
        public string answer { get; set; }
        [FirestoreProperty]
        public Boolean checkedAnswer { get; set; }
        [FirestoreProperty]
        public string content { get; set; }
        [FirestoreProperty]
        public string documentID { get; set; }
        [FirestoreProperty]
        public string hospitalID { get; set; }
        [FirestoreProperty]
        public string hospitalName { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public long timestamp { get; set; }
        [FirestoreProperty]
        public string title { get; set; }

        FirestoreDb fs;
        public static int count;
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


        public async void checkinquiry(string hospitalid)
        {
            int i = 0;
            Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalid);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Inquiry fp = docsnap.ConvertTo<Inquiry>();
                if (docsnap.Exists)
                {
                    if(fp.checkedAnswer == false)
                    {
                        i++;
                    }
                }
            }
            count = i;
        }

        public void UpdateWait(string hospitalid)
        {
            CollectionReference citiesRef = fs.Collection("inquiryList");
            Query query = fs.Collection("inquiryList").WhereEqualTo("hospitalId", DBClass.hospiID).WhereEqualTo("checkedAnswer", false);

            FirestoreChangeListener listener = query.Listen(async snapshot =>
            {
                DateTime dt = DateTime.Now;
                long ss = Convert.ToInt64(dt.AddSeconds(-3).ToString("yyyyMMddHHmmss"));
                foreach (DocumentChange change in snapshot.Changes)
                {
                    if (change.ChangeType.ToString() == "Added")
                    {
                        Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", DBClass.hospiID).WhereEqualTo("checkedAnswer", false);
                        QuerySnapshot snap = await qref.GetSnapshotAsync();
                        foreach (DocumentSnapshot docsnap in snap)
                        {
                            Inquiry fp = docsnap.ConvertTo<Inquiry>();
                            if (docsnap.Exists)
                            {
                                if (fp.checkedAnswer == false && Convert.ToInt64(ConvertDate(fp.timestamp).ToString("yyyyMMddHHmmss")) >= ss)
                                {
                                    new ToastContentBuilder()
                                        .AddArgument("action", "viewConversation")
                                        .AddArgument("conversationId", 9813)
                                        .AddText("HOSPI")
                                        .AddText("새로운 문의가 등록 되었습니다!!")
                                        .Show();
                                    break;
                                }
                            }
                        }
                    }
                }
            });
        }

        public DateTime ConvertDate(long timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
            return dtDateTime;

        }

        public static long MillisecondsTimestamp(DateTime date)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalMilliseconds;
        }
    }
}
