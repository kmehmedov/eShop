namespace Notification.Email
{
    public interface IEmailSender
    {
        Task SendAsync();
    }
}
