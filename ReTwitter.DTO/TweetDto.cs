using System;

namespace ReTwitter.DTO
{
    public class TweetDto
    {
        public string TweetId { get; set; }

        public string Text { get; set; }

        public DateTime OriginalTweetCreatedOn { get; set; }

        public string UsersMentioned { get; set; }
    }
}
