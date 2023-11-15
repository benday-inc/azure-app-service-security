namespace Benday.EasyAuthDemo.Api.ServiceLayers
{
    public interface IEmailService
    {
        Task SendEmail(string recipientEmail, string recipientName, string subject);
    }
}
