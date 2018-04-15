using ReTwitter.Data.Models.Abstracts;
using System.Linq;

namespace ReTwitter.Data.Repository
{
    public interface IGenericRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
