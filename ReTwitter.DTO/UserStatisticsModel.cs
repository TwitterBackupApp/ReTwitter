using System;

namespace ReTwitter.DTO
{
    public class UserStatisticsModel
    {
        public string UserName { get; set; }
        public DateTime UserNameCreatedOn { get; set; }
        public string ActiveStatus { get; set; }
        public int ActivelyFollowedAccountsCount { get; set; }
        public int DeletedAccountsCount { get; set; }
        public int SavedTweetsCount { get; set; }
        public int DeletedTweetsCount { get; set; }
    }
}
