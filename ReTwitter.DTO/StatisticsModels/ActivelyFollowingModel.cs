namespace ReTwitter.DTO.StatisticsModels
{
    public class ActivelyFollowingModel
    {
        public string FolloweeId { get; set; }

        public string ScreenName { get; set; }

        public string Bio { get; set; }

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
