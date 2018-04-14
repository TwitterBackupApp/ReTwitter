using System.Collections.Generic;

namespace ReTwitter.Data.Models
{
    public class Followee
    {
        public Followee()
        {
            this.InUsersFavorites = new List<User>();
        }

        public int Id { get; set; }

        public string TwitterId { get; set; }

        public string ScreenName { get; set; }

        public virtual ICollection<User> InUsersFavorites { get; set; }
    }
}