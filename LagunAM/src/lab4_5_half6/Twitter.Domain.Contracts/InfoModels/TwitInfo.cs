using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Domain.Contracts.InfoModels
{
    public class TwitInfo: ITwitInfo
    {
        public int TwitId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string TagString { get; set; }

    }
}
