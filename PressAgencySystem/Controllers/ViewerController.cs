using PressAgencySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net;

namespace PressAgencySystem.Controllers
{
    public class ViewerController : Controller
    {
        private StoreContext _context;
        public ViewerController()
        {
            _context = new StoreContext();
        }

        public ActionResult Index()
        {
            var posts = _context.Posts.Include(c => c.ArticleType).Include(u => u.Creator).Where(p => p.Accepted == 1).ToList();

            return View(posts);
        }

        public ActionResult Search(string search)
        {
            var posts = _context.Posts.SqlQuery("Select * from Posts Where Title Like '%' + @search + '%' Or Description Like '%' + @search + '%'", new SqlParameter("@search", search)).ToList();
            var postsWithJoins = _context.Posts.Include(c => c.ArticleType).Include(u => u.Creator).Where(p => p.Accepted == 1).ToList();

            var filtered = new List<Post>();

            foreach (var withj in postsWithJoins)
            {
                if (withj.Creator.UserName == search || withj.ArticleType.Name.ToLower() == search)
                {
                    filtered.Add(withj);
                }
            }
            List<Post> result = posts.Concat(filtered).ToList();
            List<Post> uniqueList = result.Distinct().ToList();

            return View("Index", uniqueList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _context.Posts.Include(c => c.ArticleType).Include(u => u.Creator).SingleOrDefault(c => c.Id == id);
            if (post == null)
                return HttpNotFound();
            post.Views = post.Views + 1;
            _context.SaveChanges();
            return View(post);
        }
    }
}