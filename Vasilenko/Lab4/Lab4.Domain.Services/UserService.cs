using Lab4.Data.Contracts.Repositories;
using Lab4.Domain.Contracts;
using System.Linq;
using UserContract = Lab4.Data.Contracts.Entities.User;

namespace Lab4.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IContextFactory contextFactory;

        public UserService(IContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void AddUser(string id)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Add(context.UsersRepository, new UserContract()
                {
                    Id = id
                });
                context.Save();

            }
        }

        public bool FindUser(string id)
        {
            using (var context = contextFactory.GetContext())
            {
                var find = context.UsersRepository.Where(x => x.Id==id).FirstOrDefault();

                if (find != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
