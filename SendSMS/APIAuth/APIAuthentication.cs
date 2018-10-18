using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SendSMS.APIAuth
{
    public class APIAuthentication : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            throw new HttpResponseException(challengeMessage);
        }

        private bool Authorize(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                var someCode = (from h in actionContext.Request.Headers where h.Key == "Authorization" select h.Value.First()).FirstOrDefault();
                
                string base64Encoded = someCode; //Value is 'base64 encoded string'
                string base64Decoded;
                byte[] data = System.Convert.FromBase64String(base64Encoded);
                base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
                
                if (base64Decoded.Split(':').Count() != 2)
                {
                    return false;
                }

                string username = base64Decoded.Split(':')[0];
                string password = base64Decoded.Split(':')[1];

                return CheckIfValidCredentials(username, password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckIfValidCredentials(string username, string password)
        {
            //USE SQL QUERY to check credentials here...
            //Temporary I am checking hard code credentials (username: jay && password: pass)
            if (username == "jay" && password == "pass")
            {
                return true;
            }
            return false;
        }
    }
}