using System;

namespace ReTwitter.DTO
{
    public class TweetDto
    {
        public string TweetId { get; set; }

        public string Text { get; set; }

        public DateTime OriginalTweetCreatedOn { get; set; }

        public int UsersMentioned { get; set; }

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
