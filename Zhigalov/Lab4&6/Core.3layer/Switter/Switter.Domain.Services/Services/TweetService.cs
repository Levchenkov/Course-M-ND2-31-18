using AutoMapper;
using Switter.Data.Contracts.Entities;
using Switter.Data.Contracts.Repositories;
using Switter.Domain.Contracts.Services;
using Switter.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Domain.Services.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository tweetRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public TweetService(ITweetRepository tweetRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.tweetRepository = tweetRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public void Create(TweetViewModel tweet)
        {
            Tweet tweetEntity = mapper.Map<Tweet>(tweet);
            tweetRepository.Create(tweetEntity);
        }

        public IEnumerable<TweetViewModel> GetAll()
        {
            IEnumerable<Tweet> tweetsEntity = tweetRepository.GetAll();
            IEnumerable<TweetViewModel> tweets = mapper.Map<IEnumerable<TweetViewModel>>(tweetsEntity);
            return tweets;
        }
    }
}
