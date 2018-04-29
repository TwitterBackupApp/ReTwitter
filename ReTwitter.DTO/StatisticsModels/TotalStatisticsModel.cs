namespace ReTwitter.DTO.StatisticsModels
{
    public class TotalStatisticsModel
    {
        public int TotalUsers { get; set; }
        public int TotalActivelyFollowedAccountsCount { get; set; }
        public int TotalDeletedAccountsCount { get; set; }
        public int TotalSavedTweetsCount { get; set; }
        public int TotalDeletedTweetsCount { get; set; }

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
                   && this.TotalDeletedTweetsCount.Equals(item.TotalDeletedTweetsCount);
        }
    }
}