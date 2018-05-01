using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.Fakes
{
    internal class FakeUserTweetService : UserTweetService
    {
        public FakeUserTweetService(IUnitOfWork unitOfWork, ITweetService tweetService, IDateTimeProvider dateTimeProvider) : base(unitOfWork, tweetService, dateTimeProvider)
        {
        }

        public override bool UserTweetExistsInDeleted(string userId, string tweetId)
        {
            throw new FakeTestException();
        }
    }
}
