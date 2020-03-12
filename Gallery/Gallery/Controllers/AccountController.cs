using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.DAL.InterfaceImplementation;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {

        private IUsersService _usersService;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public AccountController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public AccountController() : this(new UserService(new UsersRepository(new UserContext()))) { }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var canAuthorize = await _usersService.IsUserExistAsync(model.Name, model.Password);

                if (canAuthorize)
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, model.Name, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User is not found");
                }

            }
            return View(model);
        }


        public ActionResult LoginOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                var IfUserExist = await _usersService.IsUserExistAsync(model.Name, model.Password);

                if (IfUserExist == false)
                {
                    //Create a new user
                    AddUserDto userDto = new AddUserDto(model.Name, model.Password);

                    await _usersService.AddUser(userDto);
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, model.Name, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "User already exists");
                }
            }
            return View(model);
        }
    }
}