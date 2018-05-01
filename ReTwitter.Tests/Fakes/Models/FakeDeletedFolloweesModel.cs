using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeDeletedFolloweesModel : DeletedFolloweesModel
    {
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
