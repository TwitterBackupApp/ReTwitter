namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class StatisticsViewModel
    {
        public int ActiveUsers { get; set; }
        public int DeletedUsers { get; set; }
        public int ActivelyFollowedAccountsCount { get; set; }
        public int DeletedAccountsCount { get; set; }
        public int SavedTweetsCount { get; set; }
        public int DeletedTweetsCount { get; set; }
    }
}
