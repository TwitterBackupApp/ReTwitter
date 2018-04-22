using System;
using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class UserFollowee : IDeletable, IAuditable
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string FolloweeId { get; set; }
        public Followee Followee { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
