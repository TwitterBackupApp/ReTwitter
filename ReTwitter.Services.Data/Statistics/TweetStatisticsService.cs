using System.Collections.Generic;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.DTO.StatisticsModels;
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

        public IEnumerable<SavedTweetsModel> GetSavedTweetsByUserId(string userId)
        {
            var savedTweets = this.unitOfWork.UserTweets.All.Where(u => u.UserId == userId).Select(s =>
                new SavedTweetsModel
                {
                    AuthorScreenName = s.Tweet.Followee.ScreenName,
                    Text = s.Tweet.Text,
                    TweetId = s.TweetId,
                    OriginalTweetCreatedOn = s.Tweet.OriginalTweetCreatedOn
                }).ToList();

            return savedTweets;
        }

        public IEnumerable<DeletedTweetsModel> GetDeletedTweetsyUserId(string userId)
        {
            var deletedTweets = this.unitOfWork.UserTweets.AllAndDeleted.Where(u => u.UserId == userId && u.IsDeleted).Select(s =>
                new DeletedTweetsModel
                {
                    AuthorScreenName = s.Tweet.Followee.ScreenName,
                    Text = s.Tweet.Text,
                    OriginalTweetCreatedOn = s.Tweet.OriginalTweetCreatedOn,
                    TweetDeletedOn = s.DeletedOn.Value
                }).ToList();

            return deletedTweets;
        }

    }
}
