using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Data.Contracts.Repositories
{
    public interface ITagRepository: IRepository<int, Tag>
    {
        Tag CheckExist(string tagName);
        void Save();
    }
}
