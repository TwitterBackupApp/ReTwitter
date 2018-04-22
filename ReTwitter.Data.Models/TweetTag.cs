using System;

namespace ReTwitter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ReTwitter.Data.Models.Abstracts;

    public class TweetTag: IDeletable, IAuditable
    {
        [Required]
        public string TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
