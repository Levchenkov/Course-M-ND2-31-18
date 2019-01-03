using AutoMapper;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;

namespace BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }

        public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
        public void Create(CommentDataModel commentDataModel)
        {
            commentDataModel.Created = DateTime.Now;
            var newComment = Mapper.Map<CommentDataModel, Comment>(commentDataModel);
            UnitOfWork.Comments.Create(newComment);
            UnitOfWork.Save();
        }
        public CommentDataModel Get(int id)
        {
            var comment = UnitOfWork.Comments.Get(id);
            var commentDataModel = Mapper.Map<Comment, CommentDataModel>(comment);
            return commentDataModel;
        }
    }
}
