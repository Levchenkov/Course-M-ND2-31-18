using AutoMapper;
using Lab_2.Models;
using Lab_2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_2.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public static void Run()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperProfile>();
            });
        }

        public AutoMapperProfile()
        {
            CreateMap<VMPost, Post>()
                .ForMember(a => a.Author, a => a.MapFrom(s => s.Author))
                .ForMember(a => a.Comments, a => a.MapFrom(s => s.Comments))
                .ForMember(a => a.Content, a => a.MapFrom(s => s.Content))
                .ForMember(a => a.Created, a => a.MapFrom(s => s.Created))
                .ForMember(a => a.Tags, a => a.MapFrom(s => s.Tags));

            CreateMap<Post, VMPost>()
                .ForMember(a => a.Author, a => a.MapFrom(s => s.Author))
                .ForMember(a => a.Comments, a => a.MapFrom(s => s.Comments))
                .ForMember(a => a.Content, a => a.MapFrom(s => s.Content))
                .ForMember(a => a.Created, a => a.MapFrom(s => s.Created))
                .ForMember(a => a.Tags, a => a.MapFrom(s => s.Tags));

            CreateMap<VMComment, Comment>()
                .ForMember(a => a.Author, a => a.MapFrom(s => s.Author))
                .ForMember(a => a.Content, a => a.MapFrom(s => s.Content))
                .ForMember(a => a.Created, a => a.MapFrom(s => s.Created))
                .ForMember(a => a.Post, a => a.MapFrom(s => s.Post));

            CreateMap<Comment, VMComment>()
                .ForMember(a => a.Author, a => a.MapFrom(s => s.Author))
                .ForMember(a => a.Content, a => a.MapFrom(s => s.Content))
                .ForMember(a => a.Created, a => a.MapFrom(s => s.Created))
                .ForMember(a => a.Post, a => a.MapFrom(s => s.Post));

            CreateMap<VMStudent, Student>()
                .ForMember(a => a.Comments, a => a.MapFrom(s => s.Comments))
                .ForMember(a => a.FirstName, a => a.MapFrom(s => s.FirstName))
                .ForMember(a => a.LastName, a => a.MapFrom(s => s.LastName))
                .ForMember(a => a.Posts, a => a.MapFrom(s => s.Posts));

            CreateMap<Student, VMStudent>()
                .ForMember(a => a.Comments, a => a.MapFrom(s => s.Comments))
                .ForMember(a => a.FirstName, a => a.MapFrom(s => s.FirstName))
                .ForMember(a => a.LastName, a => a.MapFrom(s => s.LastName))
                .ForMember(a => a.Posts, a => a.MapFrom(s => s.Posts));

            CreateMap<VMTag, Tag>()
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Posts, a => a.MapFrom(s => s.Posts));

            CreateMap<Tag, VMTag>()
                .ForMember(a => a.Name, a => a.MapFrom(s => s.Name))
                .ForMember(a => a.Posts, a => a.MapFrom(s => s.Posts));
        }
    }
}