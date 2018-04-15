using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models;

namespace ReTwitter.DTO
{
    public class TweetDto
    {
        public string TweetId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string OriginalTweetCreatedOn { get; set; }

        [Required]
        public string FolloweeId { get; set; }

        [Required]
        public Followee Followee { get; set; }
    }
}
