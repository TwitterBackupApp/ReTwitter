using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class DeletedTweetsModel
    {
        public string AuthorScreenName { get; set; }

        public string Text { get; set; }

        public DateTime OriginalTweetCreatedOn { get; set; }

        public DateTime TweetDeletedOn { get; set; }
    }
}
