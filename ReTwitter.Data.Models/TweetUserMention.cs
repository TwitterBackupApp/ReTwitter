using System;
using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class TweetUserMention : IDeletable, IAuditable
    {
        public string TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public string FolloweeId { get; set; }
        public Followee Followee { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
