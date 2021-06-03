using PressAgencySystem.Models;
using PressAgencySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PressAgencySystem.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext _context;
        public HomeController()
        {
            _context = new StoreContext();
        }
        public ActionResult Index(User user)
        {
            if (Session["UserId"] == null)
                return View();
            var id = ((int)Session["UserId"]);
            var User = _context.Users.SingleOrDefault(c => c.Id == id);

            return View(User);
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
            if (!ModelState.IsValid)
                return View("Register", user);
            if (_context.Users.Where(u => u.Email == user.Email).Any())
            {
                ModelState.AddModelError("Email", "This Email Already Exists");
                return View("Register", user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("GetUsers");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _context.Users.SingleOrDefault(c => c.Id == id);
            if (user == null)
                return HttpNotFound();
   
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
                return View("Edit", user);

            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetUsers()
        {
            var users = _context.Users.Where(u => u.RoleId != 1);
            return View(users);
        }

        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _context.Users.SingleOrDefault(c => c.Id == id);
            if (user == null)
                return HttpNotFound();
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("GetUsers");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}