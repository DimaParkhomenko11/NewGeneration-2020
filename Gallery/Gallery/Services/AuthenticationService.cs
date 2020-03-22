using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.BLL.Interfaces;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Gallery.DAL;
using Microsoft.Owin;


namespace Gallery.BLL.Services
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
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            return claim;
        }
    }
}
