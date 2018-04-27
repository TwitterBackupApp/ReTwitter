using System.Collections.Generic;
using ReTwitter.DTO;

namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class ActivelyFollowingViewModel
    {
        public IEnumerable<ActivelyFollowingModel> ActivelyFollowingModels { get; set; }
        public string UserId { get; set; }
    }
}
