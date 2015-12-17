using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AOHarvestApp.Models.Requests;
using AOHarvestApp.Models.Responses;

namespace AOHarvestApp.Manager.Interfaces
{
    public interface IHarvestManager
    {
        string SubDomain { get; set; }
        string Email { get; set; }
        string Password { get; set; }

        GetDailyEntriesResponse GetDailyEntries(GetDailyEntriesRequest request);
    }
}
