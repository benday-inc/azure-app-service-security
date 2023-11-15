using Benday.EasyAuthDemo.Api;

namespace Benday.EasyAuthDemo.UnitTests.Fakes.Security
{
    public class FakeUsernameProvider : IUsernameProvider
    {
        public string GetUsernameReturnValue { get; set; }

        public string Username => GetUsernameReturnValue;
    }
}
