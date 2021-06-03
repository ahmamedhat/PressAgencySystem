using PressAgencySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PressAgencySystem.Controllers
{
    public class ArticleTypeController : Controller
    {
        // GET: ArticleType

        private StoreContext _context;
        public ArticleTypeController()
        {
            _context = new StoreContext();
        }
        public ActionResult Index() 
        {
            var articleTypes = _context.ArticleTypes.ToList();
            return View(articleTypes);
        }
        public ActionResult Create()
        {
            return View(new ArticleType { Id = 0});
        }
        [HttpPost]
        public ActionResult Create(ArticleType _articleType)
        {
            if (!ModelState.IsValid)
                return View("Create", _articleType);
            if (_articleType.Id > 0)
                _context.Entry(_articleType).State = System.Data.Entity.EntityState.Modified;
            else
                _context.ArticleTypes.Add(_articleType);
                
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var articalType = _context.ArticleTypes.SingleOrDefault(c => c.Id == id);
            if (articalType == null)
                return HttpNotFound();

            _context.ArticleTypes.Remove(articalType);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var articleType = _context.ArticleTypes.SingleOrDefault(c => c.Id == id);
            if (articleType == null)
                return HttpNotFound();
            return View("Create", articleType);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);
        }
    }
}