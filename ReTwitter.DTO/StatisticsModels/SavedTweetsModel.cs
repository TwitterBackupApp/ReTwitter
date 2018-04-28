using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class SavedTweetsModel
    {
        public string TweetId { get; set; }

        public string AuthorScreenName { get; set; }

        public string Text { get; set; }

        public DateTime OriginalTweetCreatedOn { get; set; }
    }
}
