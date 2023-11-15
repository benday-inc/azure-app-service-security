using Benday.EasyAuthDemo.Api.ServiceLayers;

namespace Benday.EasyAuthDemo.UnitTests.Fakes.ServiceLayers
{
    public class FakeEmailService : IEmailService
    {
        public Task SendEmail(string recipientEmail, string recipientName, string subject)
        {
            return Task.CompletedTask;
        }
    }
}
