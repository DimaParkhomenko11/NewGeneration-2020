using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.DAL.Models;
using Gallery.DAL.InterfaceImplementation;

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
        public async Task<ActionResult> Login(LoginModel model)
        {
            bool isConnection = await _usersService.IsConnectionAvailable();
            if (isConnection)
            {


                if (ModelState.IsValid)
                {
                    var canAuthorize = await _usersService.IsUserExistAsync(model.Name, model.Password);



                    if (canAuthorize)
                    {
                        var userId = _usersService.GetIdUsers(model.Name).ToString();
                        var claim = _authenticationService.ClaimTypesСreation(userId);
                        _authenticationService.OwinCookieAuthentication(HttpContext.GetOwinContext(), claim);


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User is not found");
                    }

                }
            }
            else
            {
                ViewBag.Error = "No database connection!";
                return View("Error");
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
        public async Task<ActionResult> Register(RegisterModel model)
        {
            bool isConnection = await _usersService.IsConnectionAvailable();
            if (isConnection)
            {
                if (ModelState.IsValid)
                {

                    var IfUserExist = await _usersService.IsUserExistAsync(model.Name, model.Password);

                    if (IfUserExist == false)
                    {
                        //Create a new user
                        AddUserDto userDto = new AddUserDto(model.Name, model.Password);
                        await _usersService.AddUser(userDto);

                        var userId = _usersService.GetIdUsers(model.Name).ToString();
                        var claim = _authenticationService.ClaimTypesСreation(userId);
                        _authenticationService.OwinCookieAuthentication(HttpContext.GetOwinContext(), claim);

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("", "User already exists");
                    }
                }
            }
            else
            {
                ViewBag.Error = "No database connection!";
                return View("Error");
            }
            return View(model);
        }
    }
}