using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twitter.Data;
using Twitter.Data.Entities;
using Twitter.Data.Repositories;
using Twitter.Models;

namespace Twitter.Repositories
{
    public class PostRepository : IRepository<Post>
    {

        private readonly ApplicationDbContext dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<Post>> GetAllAsync()
        {
                return await dbContext.Posts.Include(post => post.Author).ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
                return await dbContext.Posts.Include(post => post.Author).FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task CreateAsync(Post item)
        {
                await dbContext.Posts.AddAsync(item);
                await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post item)
        {
                dbContext.Entry(item).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
                var deletePost = await dbContext.Posts.FirstOrDefaultAsync(post => post.Id == id);
                if (deletePost != null)
                {
                    dbContext.Entry(deletePost).State = EntityState.Deleted;
                }
                await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> FindAsync(Expression<Func<Post, bool>> predicate)
        {
                return await dbContext.Posts.Where(predicate).ToListAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}