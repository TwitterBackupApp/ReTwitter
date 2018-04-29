using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class UserStatisticsModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime UserNameCreatedOn { get; set; }
        public string ActiveStatus { get; set; }
        public int ActivelyFollowedAccountsCount { get; set; }
        public int DeletedAccountsCount { get; set; }
        public int SavedTweetsCount { get; set; }
        public int DeletedTweetsCount { get; set; }

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
