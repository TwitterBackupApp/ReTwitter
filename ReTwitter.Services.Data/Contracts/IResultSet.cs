using System.Collections.Generic;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IResultSet<T>
    {
        IEnumerable<T> Items { get; set; }

        Pager Pager { get; }
    }
}
