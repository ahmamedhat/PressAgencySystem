using PressAgencySystem.Models;
using PressAgencySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace PressAgencySystem.Controllers
{
    public class PostController : Controller
    {
        private StoreContext _context;
        public PostController()
        {
            _context = new StoreContext();
        }

        public ActionResult Create()
        {
            var articleTypes = _context.ArticleTypes.ToList();
            var viewModel = new PostFormViewModel
            {
                Post = new Post { Id = 0 },
                articleTypes = articleTypes
            };
            return View(viewModel);
        }
        // GET: Post
        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View("Create", post);
            if (post.Id > 0)
            {
                _context.Entry(post).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                post.Accepted = 0;
                _context.Posts.Add(post);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var posts = _context.Posts.Include(c => c.ArticleType).ToList();

            return View(posts);
        }

        public ActionResult Edit (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _context.Posts.SingleOrDefault(c => c.Id == id);
            if (post == null)
                return HttpNotFound();
            var viewModel = new PostFormViewModel
            {
                Post = post,
                articleTypes = _context.ArticleTypes.ToList()
            };
            return View("Create", viewModel);
        }

        public ActionResult Delete (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _context.Posts.SingleOrDefault(c => c.Id == id);
            if (post == null)
                return HttpNotFound();
            _context.Posts.Remove(post);
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