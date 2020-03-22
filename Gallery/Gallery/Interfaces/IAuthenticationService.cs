using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Gallery.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        void OwinCookieAuthentication(IOwinContext owinContext, ClaimsIdentity claim);

        ClaimsIdentity ClaimTypesСreation(string userId);
    }
}
