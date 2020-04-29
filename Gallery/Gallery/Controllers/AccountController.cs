using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.Filters;
using Gallery.Models.AccountModels;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IUsersService _usersService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IUsersService usersService, IAuthenticationService authenticationService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        [LogFilter]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var canAuthorize = await _usersService.IsUserExistAsync(model.Email, model.Password);
                if (canAuthorize)
                {
                   
                    var userId = _usersService.GetIdUsers(model.Email).ToString();
                    
                    var claim = _authenticationService.ClaimTypesСreation(userId);
                    _authenticationService.OwinCookieAuthentication(HttpContext.GetOwinContext(), claim);


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
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        public async Task<ActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {

                var IfUserExist = await _usersService.IsUserExistAsync(model.Email, model.Password);

                if (IfUserExist == false)
                {
                    //Create a new user
                    
                    AddUserDto userDto = new AddUserDto(model.Email, model.Password);
                    await _usersService.AddUserAsync(userDto);
                    
                    var userId = _usersService.GetIdUsers(model.Email).ToString();
                    
                    var claim = _authenticationService.ClaimTypesСreation(userId);
                    _authenticationService.OwinCookieAuthentication(HttpContext.GetOwinContext(), claim);

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