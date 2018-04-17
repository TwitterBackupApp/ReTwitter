using System;

namespace ReTwitter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ReTwitter.Data.Models.Abstracts;

    public class Tag : IAuditable, IDeletable
    {
        public Tag()
        {
            this.TweetTags = new HashSet<TweetTag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public ICollection<TweetTag> TweetTags { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
