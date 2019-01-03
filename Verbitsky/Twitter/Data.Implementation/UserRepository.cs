using Data.Contracts.Models;
using Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Implementation
{
    public class UserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(UserEntity user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public IEnumerable<UserEntity> Read()
        {
            return context.Users.ToList();
        }
        public UserEntity Read(string id)
        {
            return context.Users.Find(id);
        }
        public void Update(UserEntity user)
        {
            context.Users.Attach(user);
            var entry = context.Entry(user);
            entry.Property(a => a.UserName).IsModified = true;
            entry.Property(a => a.FirstName).IsModified = true;
            entry.Property(a => a.LastName).IsModified = true;
            context.SaveChanges();
        }
        public void Delete(string id)
        {
            var oldEntity = new UserEntity { Id = id };
            context.Users.Attach(oldEntity);
            context.Users.Remove(oldEntity);
            context.SaveChanges();
        }
    }
}
