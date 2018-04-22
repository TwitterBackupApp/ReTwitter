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
            this.InUsersFavorites = new HashSet<UserFollowee>();
            this.TweetCollection = new HashSet<Tweet>();
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

        public string FolloweeOriginallyCreatedOn { get; set; }
        
        public int FavoritesCount { get; set; }

        public int StatusesCount { get; set; }
        
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<UserFollowee> InUsersFavorites { get; set; }
        public ICollection<Tweet> TweetCollection { get; set; }
    }
}