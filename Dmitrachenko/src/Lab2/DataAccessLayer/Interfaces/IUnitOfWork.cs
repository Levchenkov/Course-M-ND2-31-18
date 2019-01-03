using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Post> Posts { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Tag> Tags { get; }
        void Save();
    }
}
