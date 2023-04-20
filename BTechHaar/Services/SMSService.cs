using System.Net;
using System.Text;
using System.Web;

namespace BTechHaar.Main.Services
{
    public interface ISMSService
    {
        Task SendOTP(string mobileNumber, string OTPValue);
    }

    public class SMSService : ISMSService
    {
        public async Task SendOTP(string mobileNumber, string OTPValue)
        {
            await SendSMS(mobileNumber, OTPValue);
        }

        public async Task SendSMS(string mobileNumber, string OTPValue)
        {
            //Your authentication key  
            string userName = "harsol";
            string pass = "harsol123";

            //Multiple mobiles numbers separated by comma  

            //Sender ID,While using route4 sender id should be 6 characters long.  
            string senderId = "HARSLN";
            string templateid = "1207168179303341963";
            //Your message to send, Add URL encoding here.  

            string message = string.Format("{0} is your OTP to login your account. Thank you for choosing Haar Solutions", OTPValue);
            string route = "trans1";


            string MainUrl = "http://173.45.76.227/send.aspx?"; //Here need to give SMS API URL
              
            string URL = "";
            URL = MainUrl + "username=" + userName + "&pass=" + pass + "&route=trans1&senderid=" + senderId + "&numbers=" + mobileNumber.Trim() + "&message=" + message +  "&templateid=1207168179303341963";
             
            string strResponce = GetResponse(URL);
            string msg = "";
            if (strResponce.Equals("Fail"))
            {
                msg = "Fail";
            }
            else
            {
                msg = strResponce;
            } 

            //Prepare you post parameters  
            //StringBuilder sbPostData = new StringBuilder();
            //sbPostData.AppendFormat("username={0}", userName);
            //sbPostData.AppendFormat("pass={0}", pass);
            //sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            //sbPostData.AppendFormat("&message={0}", message);
            //sbPostData.AppendFormat("&senderid={0}", senderId);
            //sbPostData.AppendFormat("&templateid={0}", templateid);
            //sbPostData.AppendFormat("&route={0}", route);


            ////Call Send SMS API  
            //string sendSMSUri = "http://173.45.76.227/send.aspx";
            ////Create HTTPWebrequest  
            //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            ////Prepare and Add URL Encoded data  
            //UTF8Encoding encoding = new UTF8Encoding();
            //byte[] data = encoding.GetBytes(sbPostData.ToString());
            ////Specify post method  
            //httpWReq.Method = "POST";
            //httpWReq.ContentType = "application/x-www-form-urlencoded";
            //httpWReq.ContentLength = data.Length;
            //using (Stream stream = httpWReq.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            ////Get the response  
            //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string responseString = reader.ReadToEnd();

            ////Close the response  
            //reader.Close();

            //response.Close(); 
        }

        public static string GetResponse(string smsURL)
        {
            try
            {
                WebClient objWebClient = new WebClient();
                System.IO.StreamReader reader = new System.IO.StreamReader(objWebClient.OpenRead(smsURL));
                string ResultHTML = reader.ReadToEnd();
                return ResultHTML;
            }
            catch (Exception)
            {
                return "Fail";
            }
        }
    }
}
