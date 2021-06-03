using PressAgencySystem.Models;
using PressAgencySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PressAgencySystem.Controllers
{
    public class AuthenticationController : Controller
    {
        private StoreContext _context;
        public AuthenticationController()
        {
            _context = new StoreContext();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
                return View("Register", user);
            if (_context.Users.Where(u => u.Email == user.Email).Any())
            {
                ModelState.AddModelError("Email", "This Email Already Exists");
                return View("Register", user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Content("Registered Successfully");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginFormViewModel user)
        {
            if (!ModelState.IsValid)
                return View("Login", user);

            var loginUser = _context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if (loginUser == null)
            {
                ModelState.AddModelError("UserName", "UserName or Password is Incorrect");
                return View("Login", user);
            }
            else
            {
                Session["UserName"] = loginUser.UserName;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    
    }
}