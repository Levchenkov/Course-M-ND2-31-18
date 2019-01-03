using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twitter.Api.Models;
using Twitter.Api.Models.Context;

namespace Twitter.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;

        public TwitterController(UserManager<User> userManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        // GET api/twitter
        [HttpGet]
        public ActionResult<IEnumerable<Tweet>> Get()
        {
            return context.Tweets.OrderByDescending(x => x.Date).ToList();
        }

        // GET api/twitter/5
        [HttpGet("{id}")]
        public Tweet Get(int id)
        {
            return context.Tweets.FirstOrDefault(x => x.Id == id);
        }

        // POST api/twitter
        [HttpPost]
        public void Post()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            Tweet tweet = new Tweet();
            tweet.Content = Request.Form["content"];
            tweet.AuthorId = userManager.Users.FirstOrDefault(x => x.Email == email).Id;
            tweet.Author = userManager.Users.FirstOrDefault(x => x.Email == email);
            tweet.AuthorName = userManager.Users.FirstOrDefault(x => x.Email == email).UserName;
            tweet.Date = DateTime.Now;
            context.Tweets.Add(tweet);
            context.SaveChanges();
        }

        // PUT api/twitter/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/twitter/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}