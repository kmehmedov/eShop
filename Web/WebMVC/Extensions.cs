using IdentityModel;
using System.Security.Claims;
using System.Security.Principal;
using WebMVC.ViewModels;

namespace WebMVC
{
    public static class Extensions
    {
        public static AppUser GetAppUser(this IPrincipal principal)
        {
            var claimsPrincipal = principal as ClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                return new AppUser();
            }
            else
            {
                var id = claimsPrincipal.FindFirstValue(JwtClaimTypes.Subject);
                var firstName = claimsPrincipal.FindFirstValue(JwtClaimTypes.GivenName);
                var lastName = claimsPrincipal.FindFirstValue(JwtClaimTypes.FamilyName);

                return new AppUser()
                {
                    Id = id,
                    Name = firstName,
                    LastName = lastName
                };
            }
        }
    }
}
