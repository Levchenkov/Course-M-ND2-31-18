using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Domain.Contracts.InfoModels;


namespace Twitter.Web.ViewModels.Twit
{
    public class TwitView
    {
        [Required]
        public string AddedHeader { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(240, ErrorMessage = "Max lenght is 240")]
        public string AddedTwit { get; set; }
        [DataType(DataType.MultilineText)]
        [Remote("CheckDuplicateString", "Twit", ErrorMessage = "Duplicate tags")]
        public string AddedTags { get; set; }
        public IEnumerable<ITwitInfo> Twits { get; set; }
    }
}
