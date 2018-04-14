using System;
using System.ComponentModel.DataAnnotations;

namespace ReTwitter.Data.Models.Abstracts
{
    public abstract class DataModel : IAuditable, IDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
