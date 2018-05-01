using ReTwitter.DTO;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeTweetDto : TweetDto
    {
        public override bool Equals(object obj)
        {
            var item = obj as TweetDto;

            if (item == null)
            {
                return false;
            }

            return this.TweetId.Equals(item.TweetId)
                   && this.Text.Equals(item.Text)
                   && this.OriginalTweetCreatedOn.Equals(item.OriginalTweetCreatedOn)
                   && this.UsersMentioned.Equals(item.UsersMentioned);
        }
    }
}
