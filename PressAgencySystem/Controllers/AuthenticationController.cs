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
            var roles = _context.Roles.ToList();
            var viewModel = new UserFormViewModel
            {
                User = new User(),
                Roles = roles
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (_context.Users.Where(u => u.Email == user.Email).Any())
            {
                ModelState.AddModelError("Email", "This Email Already Exists");
                return RedirectToAction("Register");
            }
            if (!ModelState.IsValid)
                return RedirectToAction("Register");

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
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
                Session["UserId"] = loginUser.Id;

                if (loginUser.RoleId == 1)
                    Session["UserRole"] = "Admin";
                else if (loginUser.RoleId == 2)
                    Session["UserRole"] = "Editor";
                else
                {
                    Session["UserRole"] = "Viewer";
                    return RedirectToAction("Index", "Viewer", loginUser);
                }
                return RedirectToAction("Index", "Home" , loginUser);
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    
    }
}