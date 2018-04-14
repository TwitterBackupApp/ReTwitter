using System;

namespace ReTwitter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ReTwitter.Data.Models.Abstracts;

    public class TweetTag: IDeletable, IAuditable
    {
        public string TweetId { get; set; }
        [Required]
        public Tweet Tweet { get; set; }

        public int TagId { get; set; }
        [Required]
        public Tag Tag { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
