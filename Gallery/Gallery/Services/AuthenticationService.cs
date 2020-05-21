using System.Security.Claims;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Gallery.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public void OwinCookieAuthentication(IOwinContext owinContext, ClaimsIdentity claim)
        {
            owinContext.Authentication.SignOut();
            owinContext.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);

        }

        public ClaimsIdentity ClaimTypesСreation(UserDto userDto)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userDto.UserId.ToString(), ClaimValueTypes.String));
            if (userDto.UserRole != null)
            {
                foreach (var role in userDto.UserRole)
                {
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name, ClaimValueTypes.String));
                }
            }
            return claim;

        }
    }
}
