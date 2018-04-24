namespace ReTwitter.DTO
{
    public class FolloweeDto
    {
        public string FolloweeId { get; set; }

        public string ScreenName { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public string Url { get; set; }

        public long FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public int StatusesCount { get; set; }
    }
}
