using System;

namespace ReTwitter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ReTwitter.Data.Models.Abstracts;

    public class Tweet : IDeletable, IAuditable
    {
        public Tweet()
        {
            this.TweetTags = new HashSet<TweetTag>();
            this.UsersMentioned = new HashSet<TweetUserMention>();
        }

        public string TweetId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string OriginalTweetCreatedOn { get; set; }

        public string FolloweeId  { get; set; }

        [Required]
        public Followee Followee { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<TweetTag> TweetTags { get; set; }
        public ICollection<TweetUserMention> UsersMentioned { get; set; }
    }
}
