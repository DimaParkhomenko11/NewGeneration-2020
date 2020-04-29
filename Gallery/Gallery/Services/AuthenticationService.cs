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

        public ClaimsIdentity ClaimTypesСreation(string userId)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userId, ClaimValueTypes.String));
            return claim;

        }
    }
}
