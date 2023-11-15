using Microsoft.AspNetCore.Authentication;

namespace Benday.EasyAuthDemo.IntegrationTests.SecurityFakes
{
    public class FakeSecurityAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public bool IsAuthorized { get; set; }
        public string Username { get; set; }
    }
}
