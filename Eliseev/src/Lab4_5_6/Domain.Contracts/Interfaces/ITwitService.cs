using Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface ITwitService:IDisposable
    {
        IEnumerable<TwitViewModel> GetLatest(int ammount);
        Task<IOperationDetails> CreateAsynk(TwitViewModel twit);
    }
}
