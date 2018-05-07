using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class User : IdentityUser, IDeletable, IAuditable
    {
        public User()
        {
            this.FollowedPeople = new HashSet<UserFollowee>();
            this.TweetCollection = new HashSet<UserTweet>();
        }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserFollowee> FollowedPeople { get; set; }
        public ICollection<UserTweet> TweetCollection { get; set; }
    }
}
