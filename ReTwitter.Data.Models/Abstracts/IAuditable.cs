using System;
using System.Collections.Generic;
using System.Text;

namespace ReTwitter.Data.Models.Abstracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
