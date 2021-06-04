﻿using PressAgencySystem.Models;
using PressAgencySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Diagnostics;
using System.IO;

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
        public ActionResult Create(Post post , HttpPostedFileBase file)
        {
            if (!ModelState.IsValid && file == null)
                return RedirectToAction("Create", post);
            if (post.Id > 0)
            {
                _context.Entry(post).State = System.Data.Entity.EntityState.Modified;
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
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var posts = _context.Posts.Include(c => c.ArticleType).Include(u => u.Creator).Where(p => p.Accepted == 1).ToList();

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
        public ActionResult PostsRequests ()
        {
            var posts = _context.Posts.Include(c => c.ArticleType).Where(p => p.Accepted == 0);
            return View(posts);
        }

        public ActionResult Approve(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _context.Posts.SingleOrDefault(c => c.Id == id);
            if (post == null)
                return HttpNotFound();

            post.Accepted = 1;
            _context.SaveChanges();
            return RedirectToAction("Index");
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}