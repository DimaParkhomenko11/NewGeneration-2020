using System.Security.Claims;
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

        public ClaimsIdentity ClaimTypesСreation(string userId, int role , User user)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userId, ClaimValueTypes.String));

            if (user.Role != null)
                claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));
            return claim;

        }
    }
}
