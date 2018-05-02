using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.RepositoryTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_When_InvokedWithNullEntity()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var followeeRepository = new GenericRepository<Followee>(mockContext);

            Assert.ThrowsException<ArgumentNullException>(() => followeeRepository.Update(null));
        }
    }
}
