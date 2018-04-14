using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class UserFollowee : DataModel
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string FolloweeId { get; set; }
        public Followee Followee { get; set; }
    }
}
