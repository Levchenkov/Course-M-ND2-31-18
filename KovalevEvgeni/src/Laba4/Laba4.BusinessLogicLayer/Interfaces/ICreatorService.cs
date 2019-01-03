using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.BusinessLogicLayer.Interfaces
{
    public interface ICreatorService
    {
        IUserService CreateUserService(string connection);
    }
}
