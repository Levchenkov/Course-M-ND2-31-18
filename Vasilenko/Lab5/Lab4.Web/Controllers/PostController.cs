using AutoMapper;
using Lab4.Domain.Contracts;
using Lab4.Domain.Contracts.Entities;
using Lab4.Web.Models.PostViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Lab4.Web.Controllers
{
    [Authorize]
    public class PostController:Controller
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetPosts()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var posts = postService.GetPosts(id);
            var postsViewModel = mapper.Map<List<PostViewModel>>(posts);
            return View(postsViewModel);
        }

        [HttpPost]
        public void GetPosts(PostViewModel postViewModel)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var post = mapper.Map<Post>(postViewModel);
            postService.AddPost(post, id);
            Response.Redirect(Request.Path);
        }

        [HttpGet]
        public IActionResult UpdatePost(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var postContent = postService.FindContentPost(id, userId);
            var mapPostContent = mapper.Map<PostViewModel>(postContent);
            return View(mapPostContent);
        }

        [HttpPost]
        public IActionResult UpdatePost(PostViewModel updatedPostViewModel)
        {
            var post = mapper.Map<Post>(updatedPostViewModel);
            postService.UpdatePost(post);
            return RedirectToAction("GetPosts");
        }
    }
}
