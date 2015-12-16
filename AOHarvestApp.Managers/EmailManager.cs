using System.Net.Mail;
using AOHarvestApp.Manager.Interfaces;

namespace AOHarvestApp.Managers
{
    public class EmailManager : IEmailManager
    {
        public EmailManager() { }

        public void SendIncompleteDailyEntriesEmail(string toEmailAddress)
        {
            using (var smptClient = new SmtpClient())
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("harvestapp@agencyoasis.com", "HarvestApp");
                    message.To.Add(new MailAddress(toEmailAddress));

                    message.Subject = "HarvestApp | Incomplete Daily Entries";

                    message.IsBodyHtml = true;

                    message.Body = "<p>Please complete your daily entries for today.</p>";

                    smptClient.Send(message);
                }
            }
        }
    }
}
