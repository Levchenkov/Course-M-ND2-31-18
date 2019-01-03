using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Data.Contracts.Entities;
using Twitter.Domain.Contracts.InfoModels;

namespace Twitter.Domain.Contracts.Services
{
    public interface ITwitService
    {
        IEnumerable<ITwitInfo> GetPackTwits(int size);
        void Add(ITwitInfo twitInfo);
    }
}
