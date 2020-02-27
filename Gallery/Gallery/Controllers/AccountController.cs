using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gallery.DAL;
using Gallery.DAL.Models;

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
        public ActionResult Login(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext database = new UserContext())
                {
                    user = database.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                }



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
                User user = null;
                using (UserContext database = new UserContext())
                {
                    user = database.Users.FirstOrDefault(u => u.Email == model.Name);
                }

                if (user == null)
                {
                    //Create a new user
                    using (UserContext database = new UserContext())
                    {
                        database.Users.Add(new User { Email = model.Name, Password = model.Password });
                        database.SaveChanges();
                        user = database.Users.Where(u => u.Email == model.Name && u.Password == model.Password)
                            .FirstOrDefault();
                    }

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