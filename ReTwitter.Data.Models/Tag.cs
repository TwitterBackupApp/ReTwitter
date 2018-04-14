namespace ReTwitter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ReTwitter.Data.Models.Abstracts;

    public class Tag : DataModel
    {
        public Tag()
        {
            this.TweetTags = new HashSet<TweetTag>();
        }

        [Required]
        public string Text { get; set; }

        public ICollection<TweetTag> TweetTags { get; set; }
    }
}
