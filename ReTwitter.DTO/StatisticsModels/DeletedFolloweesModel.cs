using System;

namespace ReTwitter.DTO.StatisticsModels
{
    public class DeletedFolloweesModel
    {
        public string ScreenName { get; set; }

        public string Bio { get; set; }

        public DateTime DeletedOn { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as DeletedFolloweesModel;

            if (item == null)
            {
                return false;
            }

            return this.DeletedOn.Equals(item.DeletedOn)
                   && this.ScreenName.Equals(item.ScreenName)
                   && this.Bio.Equals(item.Bio);
        }
    }
}
