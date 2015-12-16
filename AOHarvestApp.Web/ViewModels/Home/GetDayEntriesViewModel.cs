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
        public IEnumerable<DayEntry> DayEntries { get; set; }

        public GetDayEntriesViewModel()
        {
            DayEntries = Enumerable.Empty<DayEntry>();
        }
    }
}