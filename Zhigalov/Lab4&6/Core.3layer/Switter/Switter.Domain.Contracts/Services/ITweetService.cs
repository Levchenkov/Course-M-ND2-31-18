using Switter.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Domain.Contracts.Services
{
    public interface ITweetService
    {
        void Create(TweetViewModel tweet);
        IEnumerable<TweetViewModel> GetAll();
    }
}
