namespace ReTwitter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ReTwitter.Data.Models.Abstracts;

    public class TweetTag : DataModel
    {
        public int TweetId { get; set; }
        [Required]
        public Tweet Tweet { get; set; }

        public int TagId { get; set; }
        [Required]
        public Tag Tag { get; set; }
    }
}
