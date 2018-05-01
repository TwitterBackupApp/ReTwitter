using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.Fakes
{
    internal class FakeUserFolloweeService : UserFolloweeService
    {
        public FakeUserFolloweeService(IUnitOfWork unitOfWork, IFolloweeService followeeService, IDateTimeProvider dateTimeProvider) : base(unitOfWork, followeeService, dateTimeProvider)
        {
        }

        public override bool UserFolloweeExistsInDeleted(string userId, string followeeId)
        {
            throw new FakeTestException();
        }
    }
}
