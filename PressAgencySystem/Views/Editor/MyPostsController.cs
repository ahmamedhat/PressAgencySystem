using PressAgencySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using PressAgencySystem.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;

namespace PressAgencySystem.Views.Editor
{
    public class MyPostsController : Controller
    {
        private StoreContext _context;
        public MyPostsController()
        {
            _context = new StoreContext();
        }
        // GET: MyPosts
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return View();
            var id = ((int)Session["UserId"]);
            var posts = _context.Posts.Include(c => c.ArticleType).Where(u => u.CreatorId == id).ToList();

            return View(posts);
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
        [HttpPost]
        public ActionResult Create(Post post, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid && file == null)
                return RedirectToAction("Create", post);
            if (post.Id > 0)
            {
                string imageName2 = (file == null) ? null : System.IO.Path.GetFileName(file.FileName);
                string imagePath2 = "~/Uploads/Posts";
                string pathForSaving2 = Server.MapPath(imagePath2);
                string uploadFilePathAndName2 = Path.Combine(pathForSaving2, imageName2);
                file.SaveAs(uploadFilePathAndName2);
                var oldPost = _context.Posts.SingleOrDefault(c => c.Id == post.Id);
                oldPost.ImagePath = imagePath2 + "/" + imageName2;
                oldPost.CreatedDate = DateTime.Now;
                oldPost.Description = post.Description;
                oldPost.ArticleTypeId = post.ArticleTypeId;
                oldPost.Accepted = post.Accepted;
                oldPost.CreatedDate = DateTime.Now;
                var role = Session["UserRole"];
                if (role == "Admin")
                    oldPost.Accepted = 1;
                oldPost.Views = post.Views;
            }
            else
            {
                string imageName = (file == null) ? null : System.IO.Path.GetFileName(file.FileName);
                string imagePath = "~/Uploads/Posts";
                string pathForSaving = Server.MapPath(imagePath);
                string uploadFilePathAndName = Path.Combine(pathForSaving, imageName);
                file.SaveAs(uploadFilePathAndName);
                post.ImagePath = imagePath + "/" + imageName;
                var id = ((int)Session["UserId"]);
                var role = Session["UserRole"];
                post.Accepted = 0;
                if (role == "Admin")
                    post.Accepted = 1;
                post.CreatorId = id;
                post.Views = 0;
                post.CreatedDate = DateTime.Now;
                _context.Posts.Add(post);
            }

            _context.SaveChanges();
            /* if (!ModelState.IsValid)
                return RedirectToAction("Create",post);
            if (post.Id > 0)
            {
                var oldPost = _context.Posts.SingleOrDefault(c => c.Id == post.Id);
                oldPost.Title = post.Title;
                oldPost.Description = post.Description;
                oldPost.ArticleTypeId = post.ArticleTypeId;
            }
            else
            {
                var id = ((int)Session["UserId"]);
                post.Accepted = 0;
                post.CreatorId = id;
                _context.Posts.Add(post);
            }
            _context.SaveChanges();*/
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
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

        public ActionResult Delete(int? id)
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

        public ActionResult Search (string search)
        {

            var id = ((int)Session["UserId"]);
            var posts = _context.Posts.SqlQuery("Select * from Posts where Title Like '%' + @search + '%'", new SqlParameter("@search", search)).ToList();
            var filtered = new List<Post>();

            foreach (var post in posts)
            {
                if (post.CreatorId == id)
                {
                    filtered.Add(post);
                }
            }
            return View("Index", filtered);
        }
    }
}