using System;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;

        public TagService(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }


        public Tag FindOrCreate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("UserId cannot be null");
            }

            var tagFound = this.unitOfWork.Tags.AllAndDeleted.FirstOrDefault(f => f.Text == name);

            if (tagFound == null)
            {
                tagFound = new Tag { Text = name };
                this.unitOfWork.Tags.Add(tagFound);
                this.unitOfWork.SaveChanges();
            }
            else
            {
                if (tagFound.IsDeleted)
                {
                    tagFound.IsDeleted = false;
                    tagFound.DeletedOn = null;
                    tagFound.ModifiedOn = this.dateTimeProvider.Now;
                    this.unitOfWork.SaveChanges();
                }
            }
            return tagFound;
        }

        public void Delete(int id)
        {
            var tag = this.unitOfWork.Tags.All.FirstOrDefault(x => x.Id == id);

            if (tag == null)
            {
                throw new ArgumentException("Tag with such ID is not found!");
            }

            this.unitOfWork.Tags.Delete(tag);
            this.unitOfWork.SaveChanges();
        }
    }
}
