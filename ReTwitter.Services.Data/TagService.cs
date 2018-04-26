using System;
using System.Linq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Services.Data
{
    public class TagService : ITagService
    {
        private readonly IMappingProvider mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;

        public TagService(IMappingProvider mapper, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
        }


        public Tag FindOrCreate(string name)
        {
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

        public void Save(TagDto dto)
        {
            var model = this.mapper.MapTo<Tag>(dto);
            this.unitOfWork.Tags.Add(model);
            this.unitOfWork.SaveChanges();
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
