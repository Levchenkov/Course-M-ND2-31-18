using Laba4.BusinessLogicLayer.Services;
using Laba4.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Laba4.BusinessLogicLayer.DataTransferObject;
using Laba4.Models;
using AutoMapper;
using Laba4.Services;

namespace Laba4.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private PostService postService;
        private IMapper mapper;
        public PostController()
        {
            postService = new PostService(new EFUnitOfWork());
            mapper = new MapperConfiguration(c => { c.AddProfile<MappingProfileViewModel>(); }).CreateMapper();
        }

        // GET: Post
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            return View(postService.GetPostes(user));
        }

        public ActionResult Create()
        {
            return View(new PostModel());
        }
        [HttpPost]
        public ActionResult Create(PostModel postCreate)
        {
            if (ModelState.IsValid)
            {
                PostViewModel post = mapper.Map<PostModel, PostViewModel>(postCreate);
                post.UserId = User.Identity.GetUserId();
                postService.Insert(post);
                return RedirectToAction("Index");
            }
            return View(postCreate);
        }
    }
}