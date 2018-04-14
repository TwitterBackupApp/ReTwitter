using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class Followee : IAuditable, IDeletable
    {
        public Followee()
        {
            this.InUsersFavorites = new List<UserFollowee>();
            this.TweetCollection = new List<Tweet>();
            this.MentionedInTweets = new List<TweetUserMention>();
        }

        [Key]
        [Required]
        public string FolloweeId { get; set; }

        [Required]
        public string ScreenName { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public long FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        [Required]
        public string FolloweeOriginallyCreatedOn { get; set; }
        
        public int FavoritesCount { get; set; }

        public int StatusesCount { get; set; }
        
        [Required]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserFollowee> InUsersFavorites { get; set; }
        public ICollection<Tweet> TweetCollection { get; set; }
        public ICollection<TweetUserMention> MentionedInTweets { get; set; }
    }
}