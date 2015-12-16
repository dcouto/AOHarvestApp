using AOHarvestApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOHarvestApp.Domain.Interfaces.DAL
{
    public interface IHarvestManager
    {
        string SubDomain { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string UsernameAndPassword { get; }

        bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);

        IEnumerable<DayEntry> GetDayEntries();
    }
}
