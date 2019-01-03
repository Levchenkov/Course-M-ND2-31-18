using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface IDomainUnitOfWork
    {
        IUserService UserService { get; }
        IPostService PostService { get; }
    }
}
