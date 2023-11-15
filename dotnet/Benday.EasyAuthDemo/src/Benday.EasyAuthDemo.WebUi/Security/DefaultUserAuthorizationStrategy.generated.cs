using Benday.EasyAuthDemo.Api.Security;

namespace Benday.EasyAuthDemo.WebUi.Security
{
    public partial class DefaultUserAuthorizationStrategy : IUserAuthorizationStrategy
    {
        private readonly SecurityUtility _securityUtility;
        
        public DefaultUserAuthorizationStrategy(
            IUserClaimsPrincipalProvider provider)
        {
            var principal = provider.GetUser();
            
            _securityUtility =
                new SecurityUtility(principal.Identity, principal);
        }
        
        private bool IsAdministrator()
        {
            return _securityUtility.IsInRole(
                SecurityConstants.RoleName_Admin);
        }
    }
}