namespace AOHarvestApp.Models.Requests
{
    public class GetDailyEntriesRequest
    {
        public int DayOfTheYear { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }

        public GetDailyEntriesRequest()
        {
            
        }
    }
}
