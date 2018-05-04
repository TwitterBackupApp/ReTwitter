namespace ReTwitter.DTO.StatisticsModels
{
    public class TotalStatisticsModel
    {
        public int TotalUsers { get; set; }

        public int TotalDeletedUsers { get; set; }

        public int TotalActiveUsers { get; set; }

        public int TotalActivelyFollowedAccountsCount { get; set; }

        public int TotalDeletedAccountsCount { get; set; }

        public int TotalSavedTweetsCount { get; set; }

        public int TotalDeletedTweetsCount { get; set; }
    }
}