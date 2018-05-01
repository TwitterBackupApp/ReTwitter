using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeUserStatisticsModel : UserStatisticsModel
    {
        public override bool Equals(object obj)
        {
            var item = obj as UserStatisticsModel;

            if (item == null)
            {
                return false;
            }

            return this.UserName.Equals(item.UserName)
                   && this.UserId.Equals(item.UserId)
                   && this.UserNameCreatedOn.Equals(item.UserNameCreatedOn)
                   && this.ActiveStatus.Equals(item.ActiveStatus)
                   && this.ActivelyFollowedAccountsCount.Equals(item.ActivelyFollowedAccountsCount)
                   && this.DeletedAccountsCount.Equals(item.DeletedAccountsCount)
                   && this.SavedTweetsCount.Equals(item.SavedTweetsCount)
                   && this.DeletedTweetsCount.Equals(item.DeletedTweetsCount);
        }
    }
}
