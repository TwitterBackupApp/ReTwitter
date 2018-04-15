using ReTwitter.DTO;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITagService
    {
        void Save(TagDto dto);
        void Delete(int id);
    }
}
