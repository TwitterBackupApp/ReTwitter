using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.AdminUserServiceTests
{
    [TestClass]
    public class AllAsync_Should
    {
        [TestMethod]
        public async Task FindAsyncShouldReturnCorrectResultWithFilterAndOrder()
        {

        }

        private ReTwitterDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ReTwitterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ReTwitterDbContext(dbOptions);
        }
    }
}
