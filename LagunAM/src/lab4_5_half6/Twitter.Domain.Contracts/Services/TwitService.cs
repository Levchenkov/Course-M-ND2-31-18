using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Contracts.Repositories;
using Twitter.Domain.Contracts.InfoModels;

namespace Twitter.Domain.Contracts.Services
{
    public class TwitService: ITwitService
    {
        private readonly ITwitRepository twitRepository;
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;

        private ICollection<Tag> TagStringToTags(string tagString)
        {
            IEnumerable<string> splitList = tagString.Split(' ').Distinct().ToList();
            ((List<string>)(splitList)).RemoveAll(p => p == " ");
            ICollection<Tag> tags = new List<Tag>();
            foreach (var item in splitList)
            {
                var currentTag = tagRepository.CheckExist(item);
                if (currentTag == null)
                {
                    Tag tag = new Tag { Name = item };
                    tagRepository.Create(tag);
                    tags.Add(tag);
                }
                else
                {
                    tags.Add(currentTag);
                }
            }
            tagRepository.Save();
            return tags;
        }


        public TwitService(ITwitRepository twitRepository, IMapper mapper, ITagRepository tagRepository)
        {
            this.twitRepository = twitRepository;
            this.mapper = mapper;
            this.tagRepository = tagRepository;
        }
        
        public IEnumerable<ITwitInfo> GetPackTwits(int size)
        {
            var pack = twitRepository.GetPack(size);
            List<ITwitInfo> twits = new List<ITwitInfo>();
            foreach (var item in pack)
            {
                var tmp = mapper.Map<Twit, ITwitInfo>(item);
                tmp.TagString = twitRepository.GetTagsString(item);
                twits.Add(tmp);
            }
            return twits;               
        }

        public void Add(ITwitInfo twitInfo)
        {
            var item = mapper.Map<ITwitInfo, Twit>(twitInfo);
            twitRepository.Create(item);
            twitRepository.Save();

            var tags = TagStringToTags(twitInfo.TagString);

            twitRepository.TwitTagSave(item, tags);
            twitRepository.Save();
        }

        
    }
}
