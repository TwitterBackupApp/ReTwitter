using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class DeletedTweetsModel
    {
        public string AuthorScreenName { get; set; }

        public string Text { get; set; }

        public DateTime OriginalTweetCreatedOn { get; set; }

        public DateTime TweetDeletedOn { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as DeletedTweetsModel;

            if (item == null)
            {
                return false;
            }

            return this.TweetDeletedOn.Equals(item.TweetDeletedOn)
                   && this.AuthorScreenName.Equals(item.AuthorScreenName)
                   && this.Text.Equals(item.Text)
                   && this.OriginalTweetCreatedOn.Equals(item.OriginalTweetCreatedOn);
        }

    }
}
