using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class TweetUserMention : DataModel
    {
        public string TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public string FolloweeId { get; set; }
        public Followee Followee { get; set; }
    }
}
