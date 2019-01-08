using Data.Repositories.Entities;
using Data.Repositories.Interfaces;
using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TwitService : ITwitService
    {
        private IUnitOfWork unitOfWork;

        public TwitService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public async Task<IOperationDetails> CreateAsynk(TwitViewModel twit)
        {
            Twit targetTwit = AppMapper.Mapper.Map<Twit>(twit);
            targetTwit.Created = DateTime.Now;
            User user = await unitOfWork.UserManager.FindByIdAsync(twit.UserId);
            targetTwit.User = user;
            unitOfWork.TwitRepository.Create(targetTwit);
            await unitOfWork.SaveAsynk();
            return new OperationDetails(true, "Twit has been created", "");
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<TwitViewModel> GetLatest(int ammount)
        {
            var twits = unitOfWork.TwitRepository.GetAll().OrderByDescending(t => t.Created).Take(ammount);
            var twitViewModels = AppMapper.Mapper.Map<IEnumerable<Twit>, IEnumerable<TwitViewModel>>(twits);
            return twitViewModels;
        }
    }
}
