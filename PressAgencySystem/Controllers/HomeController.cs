using PressAgencySystem.Models;
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}