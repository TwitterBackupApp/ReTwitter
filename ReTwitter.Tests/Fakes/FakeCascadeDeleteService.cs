using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.Fakes
{
    public class FakeCascadeDeleteService : CascadeDeleteService
    {
        public FakeCascadeDeleteService(IUserTweetService userTweetService, IUserFolloweeService userFolloweeService, IUnitOfWork unitOfWork, IFolloweeService followeeService, ITweetService tweetService, ITweetTagService tweetTagService, IAdminUserService userService) : base(userTweetService, userFolloweeService, unitOfWork, followeeService, tweetService, tweetTagService, userService)
        {
        }

        public override void DeleteUserFolloweeAndEntries(string followeeId, string userId)
        {
            throw new FakeTestException();
        }

        public override void DeleteUserTweetAndEntities(string userId, string tweetId)
        {
            throw new FakeTestException();
        }

        public override void DeleteEntitiesOfTweet(string tweetId)
        {
            throw new FakeTestException();
        }
    }
}