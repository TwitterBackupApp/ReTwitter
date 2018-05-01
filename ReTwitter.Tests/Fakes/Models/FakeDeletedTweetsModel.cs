using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeDeletedTweetsModel : DeletedTweetsModel
    {
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
