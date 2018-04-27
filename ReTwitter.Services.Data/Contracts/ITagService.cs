using ReTwitter.Data.Models;

namespace ReTwitter.Services.Data.Contracts
{
    public interface ITagService
    {
        void Delete(int id);

        Tag FindOrCreate(string name);
    }
}
