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

        public bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public IEnumerable<DayEntry> GetDailyEntries(int dayOfTheYear, int year, int userId)
        {
            var dailyEntries = Enumerable.Empty<DayEntry>();

            var uri = string.Format("https://{0}.harvestapp.com/daily", SubDomain);

            if (dayOfTheYear > -1 && year > -1)
                uri = string.Format("{0}/{1}/{2}", uri, dayOfTheYear, year);

            if (userId > -1)
                uri = string.Format("{0}?user_id={1}", uri, userId);

            ServicePointManager.ServerCertificateValidationCallback = Validator;

            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (request == null)
                return dailyEntries;

            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;

            // 2. It's important that both the Accept and ContentType headers are
            // set in order for this to be interpreted as an API request.
            request.Accept = "application/xml";
            request.ContentType = "application/xml";
            request.UserAgent = "harvest_api_sample.cs";

            // 3. Add the Basic Authentication header with username/password string.
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(EmailAndPassword)));

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (!request.HaveResponse || response == null)
                    return dailyEntries;

                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                    return dailyEntries;

                using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var xDoc = XDocument.Load(reader);

                    if (xDoc.Root != null)
                    {
                        var xElement = xDoc.Root.Element("day_entries");

                        if (xElement != null)
                            dailyEntries = xElement.Elements().Select(i => new DayEntry(i));
                    }
                }
            }

            return dailyEntries;
        }
    }
}
