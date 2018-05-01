using System;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data;

namespace ReTwitter.Tests.Providers
{
    internal static class DatabaseProvider
    {
        internal static ReTwitterDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ReTwitterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ReTwitterDbContext(dbOptions);
        }
    }
}
