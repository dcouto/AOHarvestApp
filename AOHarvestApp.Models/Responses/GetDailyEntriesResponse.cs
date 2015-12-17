using System.Collections.Generic;
using System.Linq;

namespace AOHarvestApp.Models.Responses
{
    public class GetDailyEntriesResponse
    {
        public IEnumerable<DayEntry> Entries { get; set; }

        public GetDailyEntriesResponse()
        {
            Entries = Enumerable.Empty<DayEntry>();
        }
    }
}
