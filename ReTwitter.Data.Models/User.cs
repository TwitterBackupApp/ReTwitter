using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReTwitter.Data.Models.Abstracts;

namespace ReTwitter.Data.Models
{
    public class User : IdentityUser, IDeletable, IAuditable
    {
        public User()
        {
            this.FollowedPeople = new List<Followee>();
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string TwitterName { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Followee> FollowedPeople { get; set; }
    }
}
