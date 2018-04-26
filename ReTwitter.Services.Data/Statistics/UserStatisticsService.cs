using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data.Statistics
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserStatisticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int ActiveUsersCount()
        {
            var activeUsers = this.unitOfWork.Users.All.Count();

            return activeUsers;
        }

        public int DeletedUsersCount()
        {
            var deletedUsers = this.unitOfWork.Users.AllAndDeleted.Count(w => w.IsDeleted);

            return deletedUsers;
        }
    }
}
