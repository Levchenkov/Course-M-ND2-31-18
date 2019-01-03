using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab9.Web.Data;
using Lab9.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lab9.Web.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public UserProfilesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public JsonResult SearchProfiles(string query)
        {
            var result = _context.UserProfiles.Where(x => x.FirstName.Contains(query) || x.LastName.Contains(query));
            return Json(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            var user = userProfile.User;
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("Id,UserId,Avatar,FirstName,LastName")] UserProfile userProfile,
            IFormFile avatarFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var extension = Path.GetExtension(avatarFile.FileName);
                    var fileName = $"{userProfile.UserId}{extension}";
                    var filePath = Path.Combine(hostingEnvironment.ContentRootPath, "Avatars", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatarFile.CopyToAsync(stream);
                    }
                    userProfile.Avatar = $"/Avatars/{fileName}";
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), new { id });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userProfile.UserId);
            return View(userProfile);
        }

        

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.Id == id);
        }
    }
}
