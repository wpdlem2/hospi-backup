using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;


namespace hospi_hospital_only
{
    [FirestoreData]
    class PrescriptionList
    {
        [FirestoreProperty]
        string departemnt { get; set; }
        [FirestoreProperty]
        string hospitalId { get; set; }
        [FirestoreProperty]
        string hospitalName { get; set; }
        [FirestoreProperty]
        string id { get; set; }
        [FirestoreProperty]
        List<string> medicine { get; set; }
        [FirestoreProperty]
        string opinion { get; set; }
        [FirestoreProperty]
        long timestamp { get; set; }

        public static string DB_NAME = "prescriptionList";

        public FirestoreDb fs;

        string path;

        public void FireConnect()
        {
            FBKey fbKey = new FBKey();
            fbKey.DecryptFile();
            path = fbKey.TempKeyFilePath;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
            fbKey.DeleteTemp();
        }

        public void PrescriptionAdd(string department, string patientID, string opinion, List<string> Medicine)
        {
            CollectionReference coll = fs.Collection(DB_NAME);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"department", department },
                {"hospitalId", DBClass.hospiID},
                {"hospitalName", DBClass.hospiname },
                {"id", patientID },
                {"medicine", Medicine },
                {"opinion", opinion },
                {"timestamp", MillisecondsTimestamp(DateTime.Now) }
            };
            coll.AddAsync(data1);
        }

        public static long MillisecondsTimestamp(DateTime date)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalMilliseconds;
        }
    }
}
