using Lab4.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Domain.Contracts.Services
{
    public interface ITweetService
    {
        List<TweetViewModel> GetTweetList();
        TweetViewModel GetTweetById(int id);
        TweetViewModel Create(TweetViewModel model);
        void Delete(int id);
        void Edit(TweetViewModel model);
    }
}
