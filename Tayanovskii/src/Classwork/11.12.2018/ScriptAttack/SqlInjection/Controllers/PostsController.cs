using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlInjection.Models;

namespace SqlInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostContext _context;

        public PostsController(PostContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPost()
        {
            var post1 = new Post() { Id = 1, Description = "test", Title = "test" };
            var post2 = new Post() { Id = 2, Description = "danger", Title = "danger" };
            _context.Post.Add(post1);
            _context.Post.Add(post2);
            _context.SaveChangesAsync();
            return _context.Post;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public string GetPost([FromRoute] int id)
        {
            using (SqlConnection connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=PostContext-379a212c-a266-4257-b946-55281b0e39fc;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                connection.Open();
                var testcommand = $"SELECT * FROM dbo.Post WHERE Id = {HttpContext.Request.Query["id"]}";
                SqlCommand command = new SqlCommand($"SELECT * FROM dbo.Post WHERE Id = {HttpContext.Request.Query["id"]}", connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returnString = string.Empty;
                        returnString += $"Title : {reader["Title"]}. ";
                        returnString += $"Description : {reader["Description"]}";
                        return   returnString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

      
    }
}