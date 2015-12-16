using AOHarvestApp.Models;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace AOHarvestApp.Manager.Interfaces
{
    public interface IHarvestManager
    {
        string SubDomain { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string EmailAndPassword { get; }

        bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);

        IEnumerable<DayEntry> GetDailyEntries(int dayOfTheYear, int year, int userId);
    }
}
