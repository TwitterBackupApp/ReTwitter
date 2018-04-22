using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
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

        public void AddTweetTagByTweetIdTagId(int tagId, string tweetId)
        {
            var tweetTagFound =
                this.unitOfWork.TweetTags.All.FirstOrDefault(fd => fd.TagId == tagId && fd.TweetId == tweetId);

            if (tweetTagFound == null)
            {
                var tweetTagToAdd = new TweetTag {TweetId = tweetId, TagId = tagId};
                this.unitOfWork.TweetTags.Add(tweetTagToAdd);
                this.unitOfWork.SaveChanges();
            }
        }

        public void DeleteTweetTag(int tagId, string tweetId)
        {
            var tweetTagFound = this.unitOfWork.TweetTags.All.FirstOrDefault(w => w.TagId == tagId && w.TweetId == tweetId);

            if (tweetTagFound != null)
            {
                this.unitOfWork.TweetTags.Delete(tweetTagFound);
                this.unitOfWork.SaveChanges();
                if (!this.AnyTweetSavedThisTagById(tagId))
                {
                    this.tagService.Delete(tagId);
                }
            }
        }

        public bool AnyTweetSavedThisTagById(int tagId)
        {
            return this.unitOfWork.TweetTags.All.Any(a => a.TagId == tagId);
        }
    }
}
