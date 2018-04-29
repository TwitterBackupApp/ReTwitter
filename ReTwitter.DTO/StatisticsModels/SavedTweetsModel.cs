using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class SavedTweetsModel
    {
        public string TweetId { get; set; }

        public string AuthorScreenName { get; set; }

        public string Text { get; set; }

        public DateTime OriginalTweetCreatedOn { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as SavedTweetsModel;

            if (item == null)
            {
                return false;
            }

            return this.TweetId.Equals(item.TweetId)
                   && this.AuthorScreenName.Equals(item.AuthorScreenName)
                   && this.Text.Equals(item.Text)
                   && this.OriginalTweetCreatedOn.Equals(item.OriginalTweetCreatedOn);
        }
    }
}
