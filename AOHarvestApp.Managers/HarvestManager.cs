using AOHarvestApp.Manager.Interfaces;
using AOHarvestApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using AOHarvestApp.Models.Requests;
using AOHarvestApp.Models.Responses;

namespace AOHarvestApp.Managers
{
    public class HarvestManager : IHarvestManager
    {
        public string SubDomain { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        private string EmailAndPassword
        {
            get
            {
                return string.Format("{0}:{1}", Email, Password);
            }
        }

        private bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public GetDailyEntriesResponse GetDailyEntries(GetDailyEntriesRequest request)
        {
            var response = new GetDailyEntriesResponse();

            if (request == null)
                return response;

            var uri = string.Format("https://{0}.harvestapp.com/daily", SubDomain);

            if (request.DayOfTheYear > -1 && request.Year > -1)
                uri = string.Format("{0}/{1}/{2}", uri, request.DayOfTheYear, request.Year);

            if (request.UserId > -1)
                uri = string.Format("{0}?user_id={1}", uri, request.UserId);

            ServicePointManager.ServerCertificateValidationCallback = Validator;

            var webRequest = WebRequest.Create(uri) as HttpWebRequest;

            if (webRequest == null)
                return response;

            webRequest.MaximumAutomaticRedirections = 1;
            webRequest.AllowAutoRedirect = true;

            // 2. It's important that both the Accept and ContentType headers are
            // set in order for this to be interpreted as an API request.
            webRequest.Accept = "application/xml";
            webRequest.ContentType = "application/xml";
            webRequest.UserAgent = "harvest_api_sample.cs";

            // 3. Add the Basic Authentication header with username/password string.
            webRequest.Headers.Add("Authorization", string.Format("Basic {0}", Convert.ToBase64String(new ASCIIEncoding().GetBytes(EmailAndPassword))));

            using (var webResponse = webRequest.GetResponse() as HttpWebResponse)
            {
                if (!webRequest.HaveResponse || webResponse == null)
                    return response;

                var responseStream = webResponse.GetResponseStream();

                if (responseStream == null)
                    return response;

                using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var xDoc = XDocument.Load(reader);

                    if (xDoc.Root != null)
                    {
                        var xElement = xDoc.Root.Element("day_entries");

                        if (xElement != null)
                            response.Entries = xElement.Elements().Select(i => new DayEntry(i));
                    }
                }
            }

            return response;
        }
    }
}
