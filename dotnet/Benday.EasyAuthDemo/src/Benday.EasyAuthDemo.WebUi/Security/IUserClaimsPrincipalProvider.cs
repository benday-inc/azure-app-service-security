using System.Security.Claims;

namespace Benday.EasyAuthDemo.WebUi.Security
{
    public interface IUserClaimsPrincipalProvider
    {
        ClaimsPrincipal GetUser();
    }
}
