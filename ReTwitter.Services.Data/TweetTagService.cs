using System;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class TweetTagService : ITweetTagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagService tagService;
        private readonly IDateTimeProvider dateTimeProvider;

        public TweetTagService(IUnitOfWork unitOfWork, ITagService tagService, IDateTimeProvider dateTimeProvider)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }


        public bool TweetTagExists(int tagId, string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException("Tweet Id cannot be null or empty!");
            }

            return this.unitOfWork.TweetTags
                .All
                .Any(a => a.TweetId == tweetId && a.TagId == tagId);
        }

        public bool TweetTagExistsInDeleted(int tagId, string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException("Tweet Id cannot be null or empty!");
            }

            return this.unitOfWork.TweetTags
                .AllAndDeleted
                .Any(a => a.TweetId == tweetId && a.TagId == tagId);
        }

        public void AddTweetTagByTweetIdTagId(int tagId, string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException("Tweet Id cannot be null or empty!");
            }

            var tweetTagToAdd = this.unitOfWork.TweetTags.AllAndDeleted.FirstOrDefault(w => w.TweetId == tweetId && w.TagId == tagId);

            if (tweetTagToAdd == null)
            {
                tweetTagToAdd = new TweetTag { TagId = tagId, TweetId = tweetId };

                this.unitOfWork.TweetTags.Add(tweetTagToAdd);
                this.unitOfWork.SaveChanges();
            }
            else
            {
                if (tweetTagToAdd.IsDeleted)
                {
                    tweetTagToAdd.IsDeleted = false;
                    tweetTagToAdd.DeletedOn = null;
                    tweetTagToAdd.ModifiedOn = this.dateTimeProvider.Now;
                    this.unitOfWork.SaveChanges();
                }
            }
        }

        public void DeleteTweetTag(int tagId, string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                throw new ArgumentNullException("Tweet Id cannot be null or empty!");
            }

            var tweetTagFound = this.unitOfWork.TweetTags.All.FirstOrDefault(w => w.TagId == tagId && w.TweetId == tweetId);

            if (tweetTagFound != null)
            {
                this.unitOfWork.юTweetTags.Delete(tweetTagFound);
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
