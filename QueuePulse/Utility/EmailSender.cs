
using Microsoft.AspNetCore.Identity.UI.Services;

namespace QueuePulse.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            //Add Implementation here, logic for sending email

            return Task.CompletedTask;
        }
    }
}
