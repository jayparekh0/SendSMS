using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SendSMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string encoding(string toEncode)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
            string toReturn = System.Convert.ToBase64String(bytes);
            return toReturn;
        }

        public void CallSMSAPI(ref string mobile, ref string msg)
        {
            string URL = "http://localhost:51727/api/sendsms/";

            var request = (HttpWebRequest)WebRequest.Create(URL);

            var postData = "mobile="+mobile;
            postData += "&msg="+msg;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers.Add("Authorization", encoding("jay:pass"));

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        [HttpPost]
        public ActionResult SendSMS()
        {
            string mobile = Request["mobile"], msg = Request["msg"];
            Task.Factory.StartNew(() => { CallSMSAPI(ref mobile, ref msg); });
            return View("Index");
        }
    }
}
