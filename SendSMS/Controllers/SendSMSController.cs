using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using SendSMS.APIAuth;
using System.IO;
using System.Threading;

namespace SendSMS.Controllers
{
    public class SendSMSController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [APIAuthentication]
        public string Post(mydata mydata)
        {
            string mobile = mydata.mobile;
            string msg = mydata.msg;
            string mymobile = "<registered_mobile_no_of_way2sms_user>";
            string password = "<password_of_way2sms_user>";
            string key = "<key_of_way2sms_user>";
            string URL = "https://smsapi.engineeringtgr.com/send/?Mobile=" + mymobile + "&Password=" + password + "&Message=" + msg + "&To=" + mobile + "&Key=" + key;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        // POST api/values
        //[SendSMS.APIAuth.APIAuthentication] //Calls overridden OnAuthorization method inside APIAuthentication class
        //public string Post(mydata mydata)
        //{
        //    string mobile = mydata.mobile;
        //    string msg = mydata.msg;
        //    string mymobile = "";
        //    string password = "";
        //    string key = "";
        //    string URL = "https://smsapi.engineeringtgr.com/send/?Mobile="+mymobile+"&Password="+password+"&Message="+msg+"&To="+mobile+"&Key="+key;

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        //    request.Method = "GET";
        //    request.ContentType = "text/html; charset=UTF-8";// "application/json";
        //    //request.ContentLength = 0;// DATA.Length;
        //    request.Timeout = Timeout.Infinite;
        //    request.KeepAlive = true;
        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    byte[] data = encoding.GetBytes(URL);
        //    request.ContentLength = data.Length;

        //    try
        //    {
        //        WebResponse webResponse = request.GetResponse();
        //        Stream webStream = webResponse.GetResponseStream();
        //        StreamReader responseReader = new StreamReader(webStream);
        //        string response = responseReader.ReadToEnd();
        //        Console.Out.WriteLine(response);
        //        responseReader.Close();
        //        return "Sent";
        //    }
        //    catch (Exception xe)
        //    {
        //        return "#Error#: "+xe.Message;
        //    }
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}