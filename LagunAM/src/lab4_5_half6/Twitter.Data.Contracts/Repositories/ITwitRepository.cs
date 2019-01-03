using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Data.Contracts.Repositories
{
    public interface ITwitRepository: IRepository<int, Twit>
    {
        IEnumerable<Twit> GetPack(int size);
        void Save();
        void TwitTagSave(Twit twit, IEnumerable<Tag> tags);
        string GetTagsString(Twit twit);
    }
}
