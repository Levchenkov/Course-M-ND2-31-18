using AutoMapper;
using Lab4.Domain.Contracts.Entities;
using Lab4.Web.Models.PostViewModel;
using System.Collections.Generic;

namespace Lab4.Web
{
    public class Map:Profile
    {
        public Map()
        {
            CreateMap<PostViewModel, Post>().ReverseMap();  
        }
    }
}
