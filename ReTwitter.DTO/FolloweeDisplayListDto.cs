using System;

namespace ReTwitter.DTO
{
    public class FolloweeDisplayListDto
    {
        public string FolloweeId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Bio { get; set; }

        public DateTime FolloweeOriginallyCreatedOn { get; set; }
    }
}