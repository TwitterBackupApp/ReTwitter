using System;
using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class UserFollowee : IDeletable, IAuditable
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string FolloweeId { get; set; }
        public Followee Followee { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
