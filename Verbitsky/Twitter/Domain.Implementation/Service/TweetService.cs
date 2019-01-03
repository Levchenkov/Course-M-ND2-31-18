using AutoMapper;
using Data.Contracts.Models;
using Data.Implementation;
using DomainContracts.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Implementation.Service
{
    public class TweetService
    {
        private readonly TweetRepository repository;
        private readonly UserRepository userRepository;
        private readonly IMapper mapper;
        public TweetService(ApplicationDbContext context, IMapper mapper)
        {
            repository = new TweetRepository(context);
            userRepository = new UserRepository(context);
            this.mapper = mapper;
        }
        public List<TweetViewModel> Get()
        {
            var tweetEntities = repository.Read();
            var tweetsView = mapper.Map<IEnumerable<TweetEntity>, IEnumerable<TweetViewModel>>(tweetEntities).ToList();
            return tweetsView;
        }
        public TweetViewModel GetsById(int id)
        {
            var view = repository.Read(id);
            var output = mapper.Map<TweetEntity, TweetViewModel>(view);
            return output;
        }
        public void Create(TweetViewModel view)
        {
            var entity = mapper.Map<TweetViewModel, TweetEntity>(view);
            entity.Author = userRepository.Read(view.AuthorId);
            repository.Create(entity);
        }
        public void Edit(TweetViewModel tweet)
        {
            var entity = mapper.Map<TweetViewModel, TweetEntity>(tweet);
            repository.Update(entity);
        }
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
