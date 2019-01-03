using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Twitter.Data;
using Twitter.Models;
using Twitter.Services;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;

        public HomeController(IPostService postService)
        {
            this.postService = postService;
        }

        // GET: PostViewModels
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var allPosts = postService.GetLastPostsAsync(20);
            return View(await allPosts);
        }

        // GET: PostViewModels/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content")] PostViewModel postViewModel)
        {
            if (!ModelState.IsValid) return View(postViewModel);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            postViewModel.AuthorId = userId;
            await postService.CreateAsync(postViewModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var postViewModel = await postService.GetAsync(id.Value);
            if (postViewModel == null)
            {
                return NotFound();
            }
            return View(postViewModel);
        }

        // POST: PostViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content")] PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await postService.EditAsync(postViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (postService.GetAsync(postViewModel.Id)==null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }

        // GET: PostViewModels/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postViewModel = await postService.GetAsync(id.Value);
            if (postViewModel == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }

        // POST: PostViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await postService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        protected override void Dispose(bool disposing)
        {
            postService.Dispose();
            base.Dispose(disposing);
        }
    }

}
