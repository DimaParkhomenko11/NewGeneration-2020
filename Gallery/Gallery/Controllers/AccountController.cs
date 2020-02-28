using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.DAL.DatabaseInteraction;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                DatabaseInteraction authorization = new DatabaseInteraction();

                User user = authorization.AuthorizationInteraction(model);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
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
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                DatabaseInteraction authorization = new DatabaseInteraction();

                User user = authorization.RegistrationInteraction(model);

                if (user == null)
                {
                    //Create a new user
                    
                    user = authorization.CreateNewUser(user, model);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
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