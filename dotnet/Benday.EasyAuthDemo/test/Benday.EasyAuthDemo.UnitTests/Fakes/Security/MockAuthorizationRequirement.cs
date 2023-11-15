using Microsoft.AspNetCore.Authorization;

namespace Benday.EasyAuthDemo.UnitTests.Fakes.Security
{
    public class MockAuthorizationRequirement : IAuthorizationRequirement
    {
        public MockAuthorizationRequirement(bool isAuthorizedReturnValue)
        {
            IsAuthorizedReturnValue = isAuthorizedReturnValue;
        }

        public bool IsAuthorizedReturnValue { get; set; }
    }
}
