using System.Collections.Generic;
using System.Linq;
using AOHarvestApp.Models;

namespace AOHarvestApp.Web.ViewModels.Home
{
    public class GetDayEntriesViewModel
    {
        public string SubDomain { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DayOfTheYear { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }
        public IEnumerable<DayEntry> DailyEntries { get; set; }

        public GetDayEntriesViewModel()
        {
            DayOfTheYear = -1;
            Year = -1;
            UserId = -1;
            DailyEntries = Enumerable.Empty<DayEntry>();
        }
    }
}