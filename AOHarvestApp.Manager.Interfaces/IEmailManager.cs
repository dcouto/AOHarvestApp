namespace AOHarvestApp.Manager.Interfaces
{
    public interface IEmailManager
    {
        void SendIncompleteDailyEntriesEmail(string toEmailAddress);
    }
}
