using AutoMapper;
using Lab4.Data.Contracts.Entities;
using Lab4.Data.Contracts.Repositories;
using Lab4.Domain.Contracts.Services;
using Lab4.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Domain.Implementation
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository repository;
        private readonly IMapper mapper;

        public TweetService(ITweetRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public TweetViewModel Create(TweetViewModel model)
        {
            var entity = mapper.Map<TweetViewModel, TweetEntity>(model);
            var createdEntity = repository.Create(entity);
            var output = mapper.Map<TweetEntity, TweetViewModel>(createdEntity);
            return output;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public void Edit(TweetViewModel model)
        {
            var entity = mapper.Map<TweetViewModel, TweetEntity>(model);
            repository.Update(entity);
        }

        public TweetViewModel GetTweetById(int id)
        {
            var entity = repository.Read(id);
            var model = mapper.Map<TweetEntity, TweetViewModel>(entity);
            return model;
        }

        public List<TweetViewModel> GetTweetList()
        {
            var entityList = repository.Read().Take(20).OrderByDescending(comparer => comparer.Created).ToList();
            var modelList = new List<TweetViewModel>();
            foreach (var entity in entityList)
            {
                var model = mapper.Map<TweetEntity, TweetViewModel>(entity);
                modelList.Add(model);
            }
            return modelList;
        }
    }
}
