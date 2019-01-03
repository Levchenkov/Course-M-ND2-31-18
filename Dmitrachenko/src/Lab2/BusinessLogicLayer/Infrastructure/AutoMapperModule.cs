using AutoMapper;
using BusinessLogicLayer.DataModel;
using DataAccessLayer.Entities;
using Ninject;
using Ninject.Modules;

namespace BusinessLogicLayer.Infrastructure
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();
        }
        private IMapper AutoMapper(Ninject.Activation.IContext context)
        {

            Mapper.Initialize(config =>
            {
                config.ConstructServicesUsing(type => context.Kernel.Get(type));

                config.CreateMap<StudentDataModel, Student>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.LastName))
                    .ForAllOtherMembers(e => e.Ignore());

                config.CreateMap<Student, StudentDataModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.LastName))
                    .ForAllOtherMembers(e => e.Ignore());

                config.CreateMap<Post, PostDataModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(source => source.Content))
                    .ForMember(dest => dest.Author, opt => opt.MapFrom(source => source.Author))
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(source => source.Comments))
                    .ForMember(dest => dest.Tags, opt => opt.MapFrom(source => source.Tags));

                config.CreateMap<PostDataModel, Post>()
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(source => source.Content))
                    .ForMember(dest => dest.Author, opt => opt.MapFrom(source => source.Author))
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(source => source.Comments))
                    .ForMember(dest => dest.Tags, opt => opt.MapFrom(source => source.Tags));

                config.CreateMap<PostDataModel, CreatePostDataModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(source => source.Content))
                    .ForMember(dest => dest.Author, opt => opt.MapFrom(source => source.Author))
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(source => source.AuthorId))
                    .ForMember(dest => dest.Created, opt => opt.MapFrom(source => source.Created))
                    .ForAllOtherMembers(e => e.Ignore());

                config.CreateMap<TagDataModel, Tag>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.Posts, opt => opt.MapFrom(source => source.Posts))
                    .ForAllOtherMembers(e => e.Ignore());

                config.CreateMap<Tag, TagDataModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.Posts, opt => opt.MapFrom(source => source.Posts))
                    .ForAllOtherMembers(e => e.Ignore());

                config.CreateMap<CommentDataModel, Comment>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(source => source.Content))
                    .ForMember(dest => dest.Created, opt => opt.MapFrom(source => source.Created))
                    .ForMember(dest => dest.Post, opt => opt.MapFrom(source => source.Post))
                    .ForMember(dest => dest.Author, opt => opt.MapFrom(source => source.Author))
                    .ForMember(dest => dest.PostId, opt => opt.MapFrom(source => source.PostId))
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(source => source.AuthorId))
                    .ForAllOtherMembers(e => e.Ignore());

                config.CreateMap<Comment, CommentDataModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(source => source.Content))
                    .ForMember(dest => dest.Created, opt => opt.MapFrom(source => source.Created))
                    .ForMember(dest => dest.Post, opt => opt.MapFrom(source => source.Post))
                    .ForMember(dest => dest.Author, opt => opt.MapFrom(source => source.Author))
                    .ForMember(dest => dest.PostId, opt => opt.MapFrom(source => source.PostId))
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(source => source.AuthorId))
                    .ForAllOtherMembers(e => e.Ignore());

            });

            Mapper.AssertConfigurationIsValid();
            return Mapper.Instance;
        }
    }
}
