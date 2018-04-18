using ReTwitter.Data.Models;
using ReTwitter.DTO;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITagService
    {
        void Save(TagDto dto);
        void Delete(int id);
        Tag FindOrCreate(string name);
    }
}
