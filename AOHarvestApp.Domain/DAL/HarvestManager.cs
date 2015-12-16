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

namespace AOHarvestApp.Domain.DAL
{
    public class HarvestManager
    {
        public string SubDomain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UsernameAndPassword
        {
            get
            {
                return string.Format("{0}:{1}", Username, Password);
            }
        }

        public HarvestManager(string subDomain, string username, string password)
        {
            SubDomain = subDomain;
            Username = username;
            Password = password;
        }

        public bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public IEnumerable<DayEntry> GetDayEntries()
        {
            var dayEntries = Enumerable.Empty<DayEntry>();

            string uri = string.Format("https://{0}.harvestapp.com/daily/#346/#2015", SubDomain);

            ServicePointManager.ServerCertificateValidationCallback = Validator;

            var request = WebRequest.Create(uri) as HttpWebRequest;
            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;

            // 2. It's important that both the Accept and ContentType headers are
            // set in order for this to be interpreted as an API request.
            request.Accept = "application/xml";
            request.ContentType = "application/xml";
            request.UserAgent = "harvest_api_sample.cs";

            // 3. Add the Basic Authentication header with username/password string.
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(UsernameAndPassword)));

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (request.HaveResponse == true && response != null)
                {
                    using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        var xDoc = XDocument.Load(reader);

                        dayEntries = xDoc.Root.Element("day_entries").Elements().Select(i => new DayEntry(i));
                    }
                }
            }

            return dayEntries;
        }
    }
}
