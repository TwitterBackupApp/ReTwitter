using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models;

namespace ReTwitter.DTO
{
    public class TweetDto
    {
        public string TweetId { get; set; }

        public string Text { get; set; }

        public string OriginalTweetCreatedOn { get; set; }

        public string FolloweeId { get; set; }

        public Followee Followee { get; set; }
    }
}
