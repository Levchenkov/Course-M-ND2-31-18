using System;
using System.Collections.Generic;
using System.Text;
using Lab4.Data.Contracts.Entities;
using Lab4.Data.Contracts.Repositories;
using System.Linq;

namespace Lab4.Data.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly Lab4DbContext context;

        public PersonRepository(Lab4DbContext context)
        {
            this.context = context;
        }

        public PersonEntity Create(PersonEntity entity)
        {
            context.Persons.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public PersonEntity Read(int id)
        {
            var output = context.Persons.Find(id);
            return output;
        }

        public PersonEntity Read(PersonEntity entity)
        {
            var output = context.Set<PersonEntity>().ToList()
                .Where(x => x.Email == entity.Email)
                .Where(x => x.PasswordHash == entity.PasswordHash)
                .FirstOrDefault();
            return output;
        }

        public void Update(PersonEntity entity)
        {
            context.Persons.Attach(entity);
            var entry = context.Entry(entity);
            entry.Property(e => e.EmailConfirmed).IsModified = true;
            context.SaveChanges();
        }
    }
}
