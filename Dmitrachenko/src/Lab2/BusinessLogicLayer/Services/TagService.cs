using AutoMapper;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TagService : ITagService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }

        public TagService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public void Create(TagDataModel tagDataModel)
        {
            var newTag = Mapper.Map<TagDataModel, Tag>(tagDataModel);
            UnitOfWork.Tags.Create(newTag);
            UnitOfWork.Save();
        }

        public void Edit(TagDataModel tagDataModel)
        {
            var editTag = Mapper.Map<TagDataModel, Tag>(tagDataModel);
            UnitOfWork.Tags.Update(editTag);
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            UnitOfWork.Tags.Delete(id);
            UnitOfWork.Save();
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
