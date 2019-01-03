using Laba4.BusinessLogicLayer.Interfaces;
using Laba4.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Services
{
    public class CreatorService : ICreatorService
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new EFUnitOfWork());
        }
    }
}
