using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Lab4.Data.Contracts.Entities
{
    public class PersonEntity : IdentityUser<int>
    {
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public ICollection<TweetEntity> TweetList { get; set; }
    }
}
