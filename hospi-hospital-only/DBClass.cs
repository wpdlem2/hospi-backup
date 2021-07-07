using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using Google.Cloud.Firestore;
using MySqlConnector;
using MySqlDataAdapter = MySqlConnector.MySqlDataAdapter;
using MySqlCommandBuilder = MySqlConnector.MySqlCommandBuilder;
using System.IO;
using System.Configuration;

namespace hospi_hospital_only
{
    [FirestoreData]
    class DBClass
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

        public static string hospiID;
        public static string hospiPW;
        public static string hospiaddress;
        public static string[] hospidepartment;
        public static string hospiholiday_close;
        public static string hospiholiday_open;
        public static Boolean hospiholiday_status;
        public static string hospikind;
        public static string hospiname;
        public static string hospisaturday_close;
        public static string hospisaturday_open;
        public static Boolean hospisaturday_status;
        public static Boolean hospistatus;
        public static string hospitel;
        public static Boolean hospitoday_reservation;
        public static string hospiweekday_close;
        public static string hospiweekday_open;


        public static string documentname;

        string path;

        string connectionString = "Server =" + Sname + "; Database =" + DBname + "; Uid =" + sqlid + "; Pwd =" + pass + ";";
        string commandString;
        MySqlDataAdapter dBAdapter;
        DataSet dS;
        MySqlCommandBuilder myCommandBuilder;
        DataTable hospitalTable, visitorTable, subjectTable, receptionTable, receptionistTable, medicineTable,
                        prescriptionTable, noticeTable, watingTable, masterTable, mobileTable, imageTable;
        FirestoreDb fs;

        private static string Sname = ConfigurationManager.AppSettings["DBAddress"];
        public static string DBname;
        private static string sqlid = ConfigurationManager.AppSettings["DBId"];
        private static string pass = ConfigurationManager.AppSettings["DBPwd"];

        // 프로퍼티
        public MySqlDataAdapter DBAdapter
        {
            get { return dBAdapter; }
            set { dBAdapter = value; }
        }
        public MySqlCommandBuilder MyCommandBuilder
        {
            get { return myCommandBuilder; }
            set { myCommandBuilder = value; }
        }
        public DataSet DS
        {
            get { return dS; }
            set { dS = value; }
        }
        public DataTable HospitalTable
        {
            get { return hospitalTable; }
            set { hospitalTable = value; }
        }
        public DataTable VisitorTable
        {
            get { return visitorTable; }
            set { visitorTable = value; }
        }
        public DataTable SubjectTable
        {
            get { return subjectTable; }
            set { subjectTable = value; }
        }
        public DataTable ReceptionTable
        {
            get { return receptionTable; }
            set { receptionTable = value; }
        }
        public DataTable ReceptionistTable
        {
            get { return receptionistTable; }
            set { receptionistTable = value; }
        }
        public DataTable MedicineTable
        {
            get { return medicineTable; }
            set { medicineTable = value; }
        }
        public DataTable PrescriptionTable
        {
            get { return prescriptionTable; }
            set { prescriptionTable = value; }
        }
        public DataTable WaitingTable
        {
            get { return watingTable; }
            set { watingTable = value; }
        }
        public string Hospiname
        {
            get { return hospiname; }
            set { hospiname = value; }
        }
        public string HospiTell
        {
            get { return hospitel; }
            set { hospitel = value; }
        }
        public DataTable NoticeTable
        {
            get { return noticeTable; }
            set { noticeTable = value; }
        }
        public DataTable MasterTable
        {
            get { return masterTable; }
            set { masterTable = value; }
        }
        public DataTable MobileTable
        {
            get { return mobileTable; }
            set { mobileTable = value; }
        }
        public DataTable ImageTable
        {
            get { return imageTable; }
            set { imageTable = value; }
        }

        //Firestore 연결
        public void FireConnect()
        {
            FBKey fbKey = new FBKey();

            try
            {
                string keyFileName = "service-account.hos";
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Hospi\";

                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo fileInfo = new FileInfo(filePath + keyFileName);
                if (!fileInfo.Exists)
                {
                    di.Create();
                    Utils.FtpDownload(keyFileName, filePath);
                }
                
                fbKey.DecryptFile();
                path = fbKey.TempKeyFilePath;
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                fs = FirestoreDb.Create("hospi-edcf9");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                fbKey.DeleteTemp();
            }

            fbKey.DeleteTemp();
        }

        //firestore login
        public async void FireLogin(string pw)
        {
            hospiID = "";
            hospiPW = "";
            Query qref = fs.Collection("hospitalAccountList").WhereEqualTo("pw", pw);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                DBClass fp = docsnap.ConvertTo<DBClass>();


                hospiID = fp.id;
                hospiPW = fp.pw;
            }
        }

        //병원 정보 검색
        public async void FindDocument(string hospitalID)
        {
            Query qref = fs.Collection("hospitals").WhereEqualTo("id", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                DBClass fp = docsnap.ConvertTo<DBClass>();

                documentname = docsnap.Id;
            }
        }


        //SHA256암호화
        public string SHA256Hash(string pw, string id)
        {

            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(pw));
            byte[] salt = sha.ComputeHash(Encoding.ASCII.GetBytes(id));
            byte[] DesSaltHash = SaltHash(hash, salt);
            StringBuilder stringBuilder = new StringBuilder(DesSaltHash.Length * 2 + (DesSaltHash.Length / 8));
            for (int i = 0; i < DesSaltHash.Length; i++)
            {
                stringBuilder.Append(BitConverter.ToString(DesSaltHash, i, 1));
            }
            string rKey = stringBuilder.ToString().TrimEnd(new char[] { ' ' }).ToLower();
            return rKey;
        }

        public static byte[] SaltHash(byte[] hash, byte[] salt)
        {
            HashAlgorithm sha256 = new SHA256CryptoServiceProvider();
            byte[] combined = salt.Concat(hash).ToArray();
            byte[] hashed = sha256.ComputeHash(combined);
            return hashed;
        }

        //딜레이 주기
        public DateTime Delay(int MS)
        {
            // Thread 와 Timer보다 효율 적으로 사용할 수 있음.
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        // hospitalID 열기
        public async void Hospital_Open(string hospitalID)
        {
            try
            {
                Query qref = fs.Collection("hospitals").WhereEqualTo("id", hospitalID);
                QuerySnapshot snap = await qref.GetSnapshotAsync();

                foreach (DocumentSnapshot docsnap in snap)
                {
                    DBClass fp = docsnap.ConvertTo<DBClass>();

                    if (docsnap.Exists)
                    {
                        hospiID = fp.id;
                        hospiaddress = fp.address;
                        hospidepartment = fp.department;
                        hospiholiday_close = fp.holidayClose;
                        hospiholiday_open = fp.holidayOpen;
                        hospiholiday_status = fp.holidayStatus;
                        hospikind = fp.kind;
                        hospiname = fp.name;
                        hospisaturday_close = fp.saturdayClose;
                        hospisaturday_open = fp.saturdayOpen;
                        hospisaturday_status = fp.saturdayStatus;
                        hospistatus = fp.status;
                        hospitel = fp.tel;
                        hospitoday_reservation = fp.todayReservation;
                        hospiweekday_close = fp.weekdayClose;
                        hospiweekday_open = fp.weekdayOpen;
                    }
                }
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 병원정보 조회용
        /*public void Hospital_Open(int hospitalID)
        {
            try
            {
                commandString = "select H.HOSPITALID,  H.HOSPITALNAME, T.HOSPITALTYPENAME, H.HOSPITALADDRESS, H.HOSPITALTELL, H.OPENTIME, H.CLOSETIME, H.SUNDAYOPEN, H.WEEKENDOPENTIME, H.WEEKENDCLOSETIME, H.RESERVATION, H.HOSPITALPW FROM HOSPITAL H, HOSPITALTYPE T WHERE H.HOSPITALTYPE = T.HOSPITALTYPE AND H.HOSPITALID = " + hospitalID;
               // commandString = "select * from hospital where HOSPITALID = " + hospitalID;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "hospital");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }*/

        // 병원정보 수정용 ( 조회용에서는 HospitalTypeName 속성을 HospitalType에서  조인해 오기 떄문에 수정이 불가함 )
        public async void Hospital_Update()
        {
            try
            {
                DocumentReference docref = fs.Collection("hospitals").Document(documentname);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"weekdayOpen", hospiweekday_open },
                    {"weekdayClose", hospiweekday_close },
                    {"saturdayOpen", hospisaturday_open },
                    {"saturdayClose", hospisaturday_close },
                    {"saturdayStatus", hospisaturday_status },
                    {"todayReservation", hospitoday_reservation },
                    {"holidayOpen", hospiholiday_open },
                    {"holidayClose", hospiholiday_close },
                    {"holidayStatus", hospiholiday_status }
                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 병원 과목정보 조회용
        public void Subject_Open()
        {
            try
            {
                commandString = " select * from subjectName order by subjectCode asc";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "SubjectName");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 환자정보 
        public void Visitor_Open()
        {
            try
            {
                commandString = "select * from visitor";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 초진환자 차트번호 조회용
        public void Visitor_Chart()
        {
            try
            {
                commandString = "select count(*) from visitor ";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 수진자 이름 조회용
        public void Visitor_Name(string patientName)
        {
            try
            {
                commandString = "select * from visitor where patientName like '%" + patientName + "%' order by patientID desc";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 수진자 이름 + 출생년도 조회
        public void Visitor_BirthName(string patientName, string birth)
        {
            try
            {
                commandString = "select * from visitor where left(patientBirthCode,2) like'%" + birth + "%' and patientName like '%" + patientName + "%' order by patientID desc";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        //환자 정보 조회
        public void Visitor_Select(string patientId)
        {
            try
            {
                commandString = "select * from visitor where patientID ="+patientId;
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 이전진료 조회
        public void Visitor_Chart(int chart)
        {
            try
            {
                commandString = "select r.receptionDate, r.receptionTime, r.subjectName, r.receptionInfo from reception r where r.patientID = " + chart + " order by r.receptionDate desc ";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 차트번호로 환자 개인정보 조회 ( 처방전 )
        public void PatientInfo(int patientNum)
        {
            try
            {
                commandString = "select * from visitor where patientID = " + patientNum;
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수 등록
        public void Reception_Open()
        {
            try
            {
                commandString = "select * from Reception order by receptionID ASC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수 조회 ( reception )
        public void Reception_Update(string receptionDate, int receptionType)
        {
            try
            {
                if (receptionType == 4)
                {
                    commandString = "select  r.receptionTime, r.patientID, v.patientName, v.patientBirthCode, r.subjectName," +
                        " i.receptionistName, r.receptionID,  r.receptionType" +
                        " from receptionist i, reception r, Visitor v" +
                        " where receptionType = 4 " +
                        " and r.patientID = V.patientID" +
                        " and r.receptionistCode = i.receptionistCode " +
                        " and r.receptionDate = '" + receptionDate + "' or r.patientID = V.patientID  " +
                        " and r.receptionistCode = i.receptionistCode " +
                        " and receptiontype = 5" +
                        " and r.receptionDate = '" + receptionDate + "'";
                }
                else if (receptionType == 0)    // office폼 대가목록 조회버튼에서 사용
                {
                    commandString = "select  r.receptionTime, r.patientID, v.patientName, v.patientBirthCode, r.subjectName," +
                        " i.receptionistName, r.receptionID,  r.receptionType" +
                        " from receptionist i, reception r, Visitor v" +
                        " where receptionType = 1 " +
                        " and r.patientID = V.patientID" +
                        " and r.receptionistCode = i.receptionistCode " +
                        " and r.receptionDate = '" + receptionDate + "' or r.patientID = V.patientID  " +
                        " and r.receptionistCode = i.receptionistCode " +
                        " and receptiontype = 5" +
                        " and r.receptionDate = '" + receptionDate + "'";
                }
                else
                {
                    commandString = "select distinct r.receptionTime, r.patientID, v.patientName, v.patientBirthCode, r.subjectName, i.receptionistName , r.receptionID, r.receptiontype  from receptionist i, " +
                                "SubjectName s, Reception r, Visitor v  where r.patientID = v.patientID   and i.receptionistCode = r.receptionistCode" +
                                " and r.receptionDate like '%" + receptionDate + "%'  and receptionType = " + receptionType + "  order by receptionTime asc";
                }

                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수 조회 ( reception ) receptionType = 5


        // 접수 조회 ( office )
        public void Reception_Office(string receptionDate, string subjectID)
        {
            try
            {
                commandString = "select distinct r.receptionTime, r.patientID, v.patientName, v.patientBirthCode, v.patientMemo, r.subjectName, i.receptionistName , r.receptionID, r.receptionInfo, r.receptionType, r.receptionDate from receptionist i, " +
                    "SubjectName s, Reception r, Visitor v  where r.patientID = v.patientID   and i.receptionistCode = r.receptionistCode" +
                    " and r.receptionDate like '%" + receptionDate + "%' and receptionType = 1 and r.subjectName = '" + subjectID + "' order by receptionTime asc";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        // 접수 조회 ( office ) 사진등록 5번상태
        public void Reception_Office2(string receptionDate, string subjectID)
        {
            try
            {
                commandString = "select distinct r.receptionTime, r.patientID, v.patientName, v.patientBirthCode, v.patientMemo, r.subjectName, i.receptionistName , r.receptionID, r.receptionInfo, r.receptionType, r.receptionDate from receptionist i, " +
                    "SubjectName s, Reception r, Visitor v  where r.patientID = v.patientID   and i.receptionistCode = r.receptionistCode" +
                    " and r.receptionDate like '%" + receptionDate + "%' and receptionType = 5 and r.subjectName = '" + subjectID + "' order by receptionTime asc";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        // 접수 조회 ( office ) 진료 보류
        public void Reception_Office3(string receptionDate, string subjectName, string patientID)
        {
            try
            {
                commandString = "select receptionID, receptionType from reception where receptionDate like '%" + receptionDate + "%' and subjectName like '%" + subjectName + "%' and patientID =" + patientID+" order by receptionID DESC";


                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        // 접수 조회 ( office ) 진료보류 목록 + 사진등록 목록


        //  접수조회 ( Radiography, 보류 )
        public void Reception_Radiography(string date)
        {
            try
            {
                commandString = "select r.receptiontime, r.patientID, v.patientName,  r.subjectName, r.receptionID , receptionType from reception r, visitor v where r.patientID = v.patientID and r.receptionType = 4 and r.receptionDate = '" + date + "' order by r.receptionID ASC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수자 조회
        public void Receptionist_Open()
        {
            try
            {
                commandString = "select * from Receptionist order by receptionistCode";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Receptionist");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 수진자 이전 내원기록 조회
        public void Reception_Before(int hospitalID, string patientID)
        {
            try
            {
                commandString = "select receptionDate, receptionTime, subjectName, receptionInfo from reception where hospitalid = " + hospitalID + " and patientid = " + patientID + " order by receptionDate desc";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 해당 날짜로 Reception조회
        public void Reception_Date(string date, string time, string patientID)
        {
            try
            {
                commandString = "select * from Reception where ReceptionDate ='" + date + "' and ReceptionTime = '" + time + "' and patientID = '" + patientID + "' order by ReceptionID ASC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        // 약품정보 조회
        public void Medicine_Open()
        {
            try
            {
                commandString = "select * from medicine order by medicineID";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Medicine");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 약품정보 이름으로 조회
        public void Medicine_Open(string medicineName)
        {
            try
            {
                commandString = "select * from medicine where medicineName like '%" + medicineName + "%'";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Medicine");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 처방전 (등록)
        public void Prescription_Open()
        {
            try
            {
                commandString = "select * from Prescription order by PrescriptionID ASC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Prescription");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        public void Presctiption_Select(string patientID, string receptionDate, string receptionTime)
        {
            try
            {
                commandString = "select m.medicineName, medicineperiod, count, medicinedosage  from prescription p, medicine m where m.medicineID = p.medicineID and patientID = " + patientID + " and receptionDate like  '%" + receptionDate + "%' and receptionTime = " + receptionTime + " order by p.prescriptionID ASC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Prescription");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        public void Presctiption_History(string patientID, string receptionDate, string receptionTime)
        {
            try
            {
                commandString = "select medicineName,medicineperiod, medicinedosage from prescription where patientID = " +patientID+" and receptionDate like '%"+receptionDate+"%' and receptionTime = "+receptionTime;
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Prescription");
            }
            catch (Exception DE)
            {
                //MessageBox.Show(DE.Message);
                MessageBox.Show(commandString);
            }
        }

        public void FindReceptionIndex(string Date, string Time)
        {
            try
            {
                commandString = "select receptionID from reception where receptionDate = '" + Date + "' and receptionTime = '" + Time + "'";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        
        //대기인원 확인
        public void countWaiting(string department, string time, string Date)
        {
            try
            {
                commandString = "select count(*) from reception where subjectName='" + department + "' and ReceptionDate ='" + Date + "' and receptionTime < '" + time + "' and receptionType = 1";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 환자 초진 재진 조회
        public void FirstReception(int patientID)
        {
            try
            {
                commandString = "select patientID from reception where patientID = " + patientID;
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 공지사항 조회
        public void Notice_Open()
        {
            try
            {
                commandString = "select * from Notice order by NoticeID DESC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "notice");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 공지사항 번호로 조회 (info띄우기)
        public void Notice_Open(int noticeID)
        {
            try
            {
                commandString = "select * from Notice where noticeID = " + noticeID + " order by NoticeID DESC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "notice");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 주사제 조회
        public void MediType()
        {
            try
            {
                commandString = "select medicineType";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        public void FindReceptionCode(string department, string Date, string time)
        {
            try
            {
                commandString = "select receptionCode from reception where subjectName='" + department + "' and ReceptionDate ='" + Date + "' and receptionTime = '" + time + "'";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // Master user 확인
        public void Master_Open()
        {
            try
            {
                commandString = "select * from Master";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "master");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        //모바일 앱 사용자 구분
        public void Mobile_Use(int patientID)
        {
            try
            {
                commandString = "select memberID from visitor where PatientID =" + patientID;
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }

        }

        //의사 소견 찾기
        public void FindOpinion(int patientID, string Date, string Time)
        {
            try
            {
                commandString = "select Opinion, MEDICINEID from Prescription where PatientID = " + patientID + " and ReceptionDate = '" + Date + "' and ReceptionTime = '" + Time + "'";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Prescription");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }

        }

        //처방 내역찾기
        public void Select_Prescription(string patientID, string receptionDate, string receptionTime)
        {
            try
            {
                commandString = "select MEDICINENAME, MEDICINEPERIOD, MEDICINEDOSAGE, COUNT  from prescription where patientID = " + patientID + " and receptionDate like  '%" + receptionDate + "%' and receptionTime = " + receptionTime + " order by prescriptionID ASC";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Prescription");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        public void Mobile_Visitor(string patientID)
        {
            try
            {
                commandString = "select MEMBERID from Visitor where patientID = " + patientID;
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 이미지 등록
        public void Image_Open()
        {
            try
            {
                commandString = "select * from Image order by imageID";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Image");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 이미지 조회
        public void Image_Open(string patientID, string date)
        {
            try
            {
                commandString = "select * from Image where patientID = '" + patientID + "' and Imagedate = '" + date + "' order by imageID";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Image");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        //의사 이름 조회
        public void FindDoctor(string department)
        {
            try
            {
                commandString = "select doctorName from subjectName where subjectName = '" + department + "'";
                DBAdapter = new MySqlDataAdapter(commandString, connectionString);
                MyCommandBuilder = new MySqlCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Subject");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
    }
}
