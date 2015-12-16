namespace AOHarvestApp.Web.ViewModels.Home
{
    public class SendIncompleteDailyEntriesEmailViewModel
    {
        public string Email { get; set; }
        public bool EmailSuccessfullySent { get; set; }

        public SendIncompleteDailyEntriesEmailViewModel() { }
    }
}