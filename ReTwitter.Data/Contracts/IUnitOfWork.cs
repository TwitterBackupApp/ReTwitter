using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;

namespace ReTwitter.Data.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<Followee> Followees { get; }

        IGenericRepository<Tag> Tags { get; }

        IGenericRepository<Tweet> Tweets { get; }

        IGenericRepository<UserFollowee> UserFollowees { get; }
        IGenericRepository<UserTweet> UserTweets { get; }
        IGenericRepository<TweetTag> TweetTags { get; }

        int SaveChanges();
    }
}
