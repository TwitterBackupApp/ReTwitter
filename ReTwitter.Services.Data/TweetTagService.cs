using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class TweetTagService : ITweetTagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagService tagService;

        public TweetTagService(IUnitOfWork unitOfWork, ITagService tagService)
        {
            this.unitOfWork = unitOfWork;
            this.tagService = tagService;
        }

        public void DeleteTweetTag(int tagId, string tweetId)
        {
            var tweetTagFound = this.unitOfWork.TweetTags.All.FirstOrDefault(w => w.TagId == tagId && w.TweetId == tweetId);

            if (tweetTagFound != null)
            {
                this.unitOfWork.TweetTags.Delete(tweetTagFound);
                this.unitOfWork.SaveChanges();
                if (!this.TweetsSavedThisTagById(tagId))
                {
                    this.tagService.Delete(tagId);
                }
            }
        }

        public bool TweetsSavedThisTagById(int tagId)
        {
            return this.unitOfWork.TweetTags.All.Any(a => a.TagId == tagId);
        }
    }
}
