using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Toolkit.Uwp.Notifications;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Reserve
    {
        [FirestoreProperty]
        public string symptom { get; set; }
        [FirestoreProperty]
        public string department { get; set; }
        [FirestoreProperty]
        public string hospitalId { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string reservationDate { get; set; }
        [FirestoreProperty]
        public int reservationStatus { get; set; }
        [FirestoreProperty]
        public string reservationTime { get; set; }
        [FirestoreProperty]
        public long timestamp { get; set; }
        [FirestoreProperty]
        public string name { get; set; }
        [FirestoreProperty]
        public string phone { get; set; }
        [FirestoreProperty]
        public string address { get; set; }
        [FirestoreProperty]
        public string birth { get; set; }
        [FirestoreProperty]
        public Dictionary<string, List<string>> reserveMap{ get; set; }
        [FirestoreProperty]
        public string token { get; set; }
        [FirestoreProperty]
        public string cancelComment { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public string sex { set; get; }

        DBClass dbc = new DBClass();
        ReceptionList reception = new ReceptionList();

        public FirestoreDb fs;
        public static int count;
        public string patientId;
        public string patientName;
        public string patientPhone;
        public string patientAddress;
        public string patientBirth;
        public string patientSex;
        public static string documentName;
        public static string reserveDocument;
        public static string UserToken;
        public static string cancelcomment;

        public string time;
        public string Date;
        public string comment;

        public int receptionIndex;

        string path;

        public Dictionary<string, List<string>> reservemap;
        public List<Reserve> list = new List<Reserve>(); // 문의내역 리스트
        public List<string> UserEmail = new List<string>();

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

        async public void ReserveOpen(string hospitalID)
        {
            list.Clear();
            Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    Reserve reserve = fp;

                    list.Add(reserve);
                }
            }
        }

        //당일 예약 땡겨오기
        async public void TodayReserveOpen(string hospitalID)
        {
            list.Clear();
            Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    if (fp.reservationDate == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        Reserve reserve = fp;

                        list.Add(reserve);
                    }
                }
            }
        }

        async public void ConvertToName(string Name)
        {
            UserEmail.Clear();
            Query qref = fs.Collection("userList").WhereEqualTo("name", Name);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    UserEmail.Add(fp.email);
                }
            }
        }
        async public void FindPatient(string id)
        {
            Query qref = fs.Collection("userList").WhereEqualTo("email", id);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    patientId = fp.email;
                    patientName = fp.name;
                    patientPhone = fp.phone;
                    patientAddress = fp.address;
                    patientBirth = fp.birth;
                    patientSex = fp.sex;
                    UserToken = fp.token;
                }
            }
        }

        //문서 이름찾기
        async public void FindDocument(string hospitalID, string time, string id, string Date, string department)
        {
            Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalID).WhereEqualTo("reservationTime", time).WhereEqualTo("id", id).WhereEqualTo("reservationDate", Date).WhereEqualTo("department", department);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    documentName = docsnap.Id;
                    time = fp.reservationTime;
                    Date = fp.reservationDate;
                    comment = fp.symptom;

                }
            }
        }


        async public void EndDocument(string hospitalID, string department, string id, string Date, string Time)
        {
            Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalID).WhereEqualTo("department", department).WhereEqualTo("id", id).WhereEqualTo("reservationDate", Date).WhereEqualTo("reservationTime", Time);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    documentName = docsnap.Id;
                }
            }
        }

        //예약 시간 문서이름 찾기
        async public void FindReserveDocument(string hospitalID, string department)
        {
            Query qref = fs.Collection("reservedList").WhereEqualTo("hospitalId", hospitalID).WhereEqualTo("department", department);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    reserveDocument = docsnap.Id;
                    reservemap = fp.reserveMap;
                }
            }
        }

        //예약 승인
        async public void ReserveAccept()
        {
            try
            {
                DocumentReference docref = fs.Collection("reservationList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservationStatus", 1 }

                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //예약 시간 추가
        async public void ReserveTimeAdd()
        {
            try
            {
                DocumentReference docref = fs.Collection("reservedList").Document(reserveDocument);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservedMap", reservemap }

                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        //예약 취소
        async public void ReserveCancel(string Comment)
        {
            try
            {
                DocumentReference docref = fs.Collection("reservationList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservationStatus", -1 },
                    {"cancelComment", Comment }

                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //진료 완료
        async public void ReserveEnd()
        {
            try
            {
                DocumentReference docref = fs.Collection("reservationList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservationStatus", 2 }

                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        public void RemoveReserveTime(string Date, string Time)
        {
            DocumentReference docref = fs.Collection("reservedList").Document(reserveDocument);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"reservedMap."+Date, FieldValue.ArrayRemove(Time) }
            };
            docref.UpdateAsync(data);
        }

        public void ReserveUpdateWait(string hospitalid)
        {
            CollectionReference citiesRef = fs.Collection("reservationList");
            Query query = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalid).WhereEqualTo("reservationStatus", 0);

            FirestoreChangeListener listener = query.Listen(async snapshot =>
            {
                DateTime dt = DateTime.Now;
                long ss = Convert.ToInt64(dt.AddSeconds(-3).ToString("yyyyMMddHHmmss"));

                Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalid);
                QuerySnapshot snap = await qref.GetSnapshotAsync();
                foreach (DocumentSnapshot docsnap in snap)
                {
                    Reserve fp = docsnap.ConvertTo<Reserve>();
                    if (docsnap.Exists)
                    {
                        if (fp.reservationStatus == 0 && Convert.ToInt64(ConvertDate(fp.timestamp).ToString("yyyyMMddHHmmss")) >= ss)
                        {
                            new ToastContentBuilder()
                                .AddArgument("action", "viewConversation")
                                .AddArgument("conversationId", 9813)
                                .AddText("HOSPI")
                                .AddText("새로운 예약 신청이 있습니다.")
                                .Show();
                            break;
                        }
                    }
                }

            });
        }

        public void ReserveCancelWait(string hospitalid)
        {
            CollectionReference citiesRef = fs.Collection("reservationList");
            Query query = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalid);

            FirestoreChangeListener listener = query.Listen(async snapshot =>
            {
                foreach (DocumentChange change in snapshot.Changes)
                {
                    if (change.ChangeType.ToString() == "Modified")
                    {
                        try
                        {
                            
                            Query qref = fs.Collection("reservationList").WhereEqualTo("reservationStatus",-1);
                            QuerySnapshot snap = await qref.GetSnapshotAsync();
                            foreach (DocumentSnapshot docsnap in snap)
                            {
                                Reserve fp = docsnap.ConvertTo<Reserve>();
                                if (docsnap.Exists)
                                {
                                    if (docsnap.Id == change.Document.Id)
                                    {


                                        dbc.FindReceptionIndex(fp.reservationDate.Substring(2, 8), fp.reservationTime.Substring(0, 2) + fp.reservationTime.Substring(3, 2));
                                        dbc.ReceptionTable = dbc.DS.Tables["reception"];
                                        receptionIndex = Convert.ToInt32(dbc.ReceptionTable.Rows[0][0].ToString());

                                        dbc.Reception_Open();
                                        dbc.ReceptionTable = dbc.DS.Tables["reception"];
                                        DataColumn[] PrimaryKey = new DataColumn[1];
                                        PrimaryKey[0] = dbc.ReceptionTable.Columns["receptionID"];
                                        dbc.ReceptionTable.PrimaryKey = PrimaryKey;
                                        DataRow delRow = dbc.ReceptionTable.Rows.Find(receptionIndex);
                                        int rowCount = dbc.ReceptionTable.Rows.Count; // 삭제전 전체 row 갯수
                                        delRow.Delete();
                                        int receptionID = Convert.ToInt32(receptionIndex);
                                        // listViewIndexID1 을 증감시킬경우 for문에 영향을 주므로 변수를 따로 지정해서 사용

                                        //  열 하나가 삭제될 경우 열의 인덱스가 삭제 대상보다 높은경우 모두 -1 해줌
                                        // ex) 10개열 테이블에서 7번열 삭제시 8ㅡ>7 / 9-ㅡ>8 / 10ㅡ>9
                                        for (int i = 0; i < (rowCount - Convert.ToInt32(receptionIndex)); i++)
                                        {
                                            delRow = dbc.ReceptionTable.Rows[rowCount - (rowCount - receptionID)];
                                            delRow.BeginEdit();
                                            delRow["receptionID"] = Convert.ToInt32(delRow["receptionID"]) - 1;
                                            delRow.EndEdit();
                                            receptionID += 1;
                                        }
                                        dbc.DBAdapter.Update(dbc.DS, "reception");
                                        dbc.DS.AcceptChanges();
                                        reception.FireConnect();
                                        dbc.Delay(200);
                                        reception.FindDocument(DBClass.hospiID, fp.reservationDate, fp.reservationTime, fp.department);
                                        dbc.Delay(200);
                                        reception.Delete_Reception();


                                    }
                                }
                            }
                        }
                        catch
                        {
                            
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


    }

        
}


