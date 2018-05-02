using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;
using System;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.RepositoryTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void NotReturnNull_When_RepositoryCalled()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var followeeRepository = new GenericRepository<Followee>(mockContext);

            Assert.IsNotNull(followeeRepository);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_RepositoryInvokedWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>((() => new GenericRepository<User>(null)));
        }
    }
}
