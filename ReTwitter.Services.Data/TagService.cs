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

        public TagService(IMappingProvider mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
