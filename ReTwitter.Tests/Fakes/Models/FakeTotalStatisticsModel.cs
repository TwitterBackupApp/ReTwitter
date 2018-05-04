using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeTotalStatisticsModel : TotalStatisticsModel
    {
        public override bool Equals(object obj)
        {
            var item = obj as TotalStatisticsModel;

            if (item == null)
            {
                return false;
            }

            return this.TotalUsers.Equals(item.TotalUsers)
                   && this.TotalActivelyFollowedAccountsCount.Equals(item.TotalActivelyFollowedAccountsCount)
                   && this.TotalDeletedAccountsCount.Equals(item.TotalDeletedAccountsCount)
                   && this.TotalSavedTweetsCount.Equals(item.TotalSavedTweetsCount)
                   && this.TotalActiveUsers.Equals(item.TotalActiveUsers)
                   && this.TotalDeletedUsers.Equals(item.TotalDeletedUsers)
                   && this.TotalDeletedTweetsCount.Equals(item.TotalDeletedTweetsCount);
        }
    }
}
