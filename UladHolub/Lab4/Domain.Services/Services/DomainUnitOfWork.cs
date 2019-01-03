using Data.Contracts.Interfaces;
using Domain.Contracts.Interfaces;

namespace Domain.Services.Services
{
    public class DomainUnitOfWork : IDomainUnitOfWork
    {
        private IUnitOfWork dataUnitOfWork;
        private IUserService userService;
        private IPostService postService;

        public DomainUnitOfWork(IUnitOfWork dataUnitOfWork)
        {
            this.dataUnitOfWork = dataUnitOfWork;
            userService = new UserService(dataUnitOfWork);
            postService = new PostService(dataUnitOfWork);
        }

        public IUserService UserService
        {
            get { return userService; }
        }

        public IPostService PostService
        {
            get { return postService; }
        }
    }
}
