using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    public class FakeStatisticsScreenModel : StatisticsScreenModel
    {
        public override bool Equals(object obj)
        {
            var item = obj as StatisticsScreenModel;

            if (item == null)
            {
                return false;
            }

            return this.TotalStatisticsModel.Equals(item.TotalStatisticsModel)
                   && this.UserStatisticsModels.Equals(item.UserStatisticsModels);
        }
    }
}
