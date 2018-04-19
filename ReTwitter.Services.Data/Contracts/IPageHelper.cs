using System.Linq;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IPageHelper<T>
    {
        IResultSet<T> GetPage(IQueryable<T> items, int pageNumber);
    }
}
