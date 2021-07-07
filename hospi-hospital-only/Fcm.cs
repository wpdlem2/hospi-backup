using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using FirebaseAdmin.Messaging;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Fcm
    {
        [FirestoreProperty]
        public string token { get; set; }

        FirestoreDb fs;
        public static string Title;
        public static string UserId;



        public async Task PushNotificationToFCM(string title, string UserToken, string body)
        {
            try
            {
                var message = new FirebaseAdmin.Messaging.Message()
                {
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = body,
                    },
                    Token = UserToken,
                };

                await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

        }


    }
}
