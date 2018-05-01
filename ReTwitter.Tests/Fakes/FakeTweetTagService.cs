using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.Fakes
{
    internal class FakeTweetTagService : TweetTagService
    {
        public FakeTweetTagService(IUnitOfWork unitOfWork, ITagService tagService, IDateTimeProvider dateTimeProvider) : base(unitOfWork, tagService, dateTimeProvider)
        {
        }

        public override bool AnyTweetSavedThisTagById(int tagId)
        {
            throw new FakeTestException();
        }
    }
}
