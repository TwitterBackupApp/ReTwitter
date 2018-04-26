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


        public bool TweetTagExists(int tagId, string tweetId)
        {
            return this.unitOfWork.TweetTags
                .All
                .Any(a => a.TweetId == tweetId && a.TagId == tagId);
        }

        public bool TweetTagExistsInDeleted(int tagId, string tweetId)
        {
            return this.unitOfWork.TweetTags
                .AllAndDeleted
                .Any(a => a.TweetId == tweetId && a.TagId == tagId);
        }

        public void AddTweetTagByTweetIdTagId(int tagId, string tweetId)
        {
            if(!this.TweetTagExistsInDeleted(tagId, tweetId))
            {
                var tweetTagToAdd = new TweetTag { TweetId = tweetId, TagId = tagId };
                this.unitOfWork.TweetTags.Add(tweetTagToAdd);
                this.unitOfWork.SaveChanges();
            }
            else
            {
                var tweetTagToBeReadded = this.unitOfWork
                    .TweetTags
                    .AllAndDeleted
                    .FirstOrDefault(fd => fd.TagId == tagId && fd.TweetId == tweetId);

                if(tweetTagToBeReadded != null)
                {
                    tweetTagToBeReadded.IsDeleted = false;
                    this.unitOfWork.SaveChanges();
                }
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

        public bool AnyTweetSavedThisTagById(int tagId) => this.unitOfWork.TweetTags.All.Any(a => a.TagId == tagId);
    }
}
