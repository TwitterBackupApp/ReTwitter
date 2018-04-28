using System.Collections.Generic;
using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Web.Areas.Admin.Models.Statistics
{
    public class DeletedFolloweesViewModel
    {
        public IEnumerable<DeletedFolloweesModel> DeletedFolloweesModels { get; set; }
    }
}
