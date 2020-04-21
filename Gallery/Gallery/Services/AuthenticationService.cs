using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.BLL.Interfaces;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Gallery.DAL;
using Gallery.DAL.Models;
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
