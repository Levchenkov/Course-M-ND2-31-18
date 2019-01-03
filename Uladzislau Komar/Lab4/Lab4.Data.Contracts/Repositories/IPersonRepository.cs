using System;
using System.Collections.Generic;
using System.Text;
using Lab4.Data.Contracts.Entities;

namespace Lab4.Data.Contracts.Repositories
{
    public interface IPersonRepository
    {
        PersonEntity Create(PersonEntity entity);
        PersonEntity Read(int id);
        PersonEntity Read(PersonEntity entity);
        void Update(PersonEntity entity);
    }
}
