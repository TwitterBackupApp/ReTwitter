using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeSavedTweetsModel : SavedTweetsModel
    {
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
