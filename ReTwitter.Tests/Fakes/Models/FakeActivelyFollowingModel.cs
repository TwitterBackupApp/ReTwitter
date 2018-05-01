using ReTwitter.DTO.StatisticsModels;

namespace ReTwitter.Tests.Fakes.Models
{
    internal class FakeActivelyFollowingModel : ActivelyFollowingModel
    {
        public override bool Equals(object obj)
        {
            var item = obj as ActivelyFollowingModel;

            if (item == null)
            {
                return false;
            }

            return this.FolloweeId.Equals(item.FolloweeId)
                   && this.ScreenName.Equals(item.ScreenName)
                   && this.Bio.Equals(item.Bio);
        }
    }
}
