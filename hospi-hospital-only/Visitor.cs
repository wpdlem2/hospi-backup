using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Visitor
    {
        [FirestoreProperty]
        public string address { get; set; }
        [FirestoreProperty]
        public bool admin { get; set; }
        [FirestoreProperty]
        public string birth { get; set; }
        [FirestoreProperty]
        public string documentId { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public string name { get; set; }
        [FirestoreProperty]
        public string phone { get; set; }
        [FirestoreProperty]
        public string token { get; set; }

        public string PatientName;
        public string PatientPhone;
        public string PatientAddress;
        public string UserToken;

        FirestoreDb fs;

        string path;

        //Firestore 연결
        public void FireConnect()
        {
            try
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.GetApplicationDefault(),
                });
            }
            catch (Exception e)
            { }

            fs = FirestoreDb.Create("hospi-edcf9");
        }

        async public void FindPatient(string email)
        {
            Query qref = fs.Collection("userList").WhereEqualTo("email", email);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Visitor fp = docsnap.ConvertTo<Visitor>();
                if (docsnap.Exists)
                {
                    PatientName = fp.name;
                    PatientPhone = fp.phone;
                    PatientAddress = fp.address;
                    UserToken = fp.token;
                }
            }
        }

    }
}
