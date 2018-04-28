using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class DeletedFolloweesModel
    {
        public string ScreenName { get; set; }

        public string Bio { get; set; }

        public DateTime DeletedOn { get; set; }
    }
}
