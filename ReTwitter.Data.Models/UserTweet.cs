using System;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class UserTweet : IDeletable, IAuditable
    {
        public string TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
