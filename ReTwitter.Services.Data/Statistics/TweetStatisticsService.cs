using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.Statistics
{
    public class TweetStatisticsService : ITweetStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public TweetStatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int SavedTweetsCountByUserId(string userId)
        {
            var savedTweetsCount = this.unitOfWork
                                            .UserTweets
                                            .All
                                            .Count(w => w.UserId == userId);

            return savedTweetsCount;
        }

        public int SavedTweetsCount()
        {
            var savedTweetsCount = this.unitOfWork
                .UserTweets
                .All
                .Count();

            return savedTweetsCount;
        }

        public int DeletedTweetsCountByUserId(string userId)
        {
            var deletedTweetsCount = this.unitOfWork
                .UserTweets
                .AllAndDeleted
                .Count(w => w.UserId == userId && w.IsDeleted);

            return deletedTweetsCount;
        }

        public int DeletedTweetsCount()
        {
            var deletedTweetsCount = this.unitOfWork
                .Tweets
                .AllAndDeleted
                .Count(w => w.IsDeleted);

            return deletedTweetsCount;
        }
    }
}
