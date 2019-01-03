using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Domain.Contracts.InfoModels
{
    public interface ITwitInfo
    {
        int TwitId { get; set; }
        string Header { get; set; }
        string Content { get; set; }
        string TagString { get; set; }
    }
}
