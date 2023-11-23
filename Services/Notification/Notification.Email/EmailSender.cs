namespace Notification.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendAsync()
        {
            // Send some mail;
            return Task.CompletedTask;
        }
    }
}
