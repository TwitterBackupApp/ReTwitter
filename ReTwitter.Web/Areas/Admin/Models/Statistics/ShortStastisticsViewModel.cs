namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class ShortStatisticsViewModel
    {
        public string Username { get; set; }
        public int ActivelyFollowedAccountsCount { get; set; }
        public int DeletedAccountsCount { get; set; }
        public int SavedTweetsCount { get; set; }
        public int DeletedTweetsCount { get; set; }
    }
}
