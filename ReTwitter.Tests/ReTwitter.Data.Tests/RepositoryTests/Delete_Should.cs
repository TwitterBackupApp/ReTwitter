using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.RepositoryTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_When_CalledWithNullEntity()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var followeeRepository = new GenericRepository<Followee>(mockContext);

            Assert.ThrowsException<ArgumentNullException>(() => followeeRepository.Delete(null));
        }

        [TestMethod]
        public void DeletesEntity_When_InvokedWithCorrectValues()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var followeeRepository = new GenericRepository<Followee>(mockContext);
            var testFollowee1 = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };

            mockContext.Followees.Add(testFollowee1);
            mockContext.SaveChanges();
            followeeRepository.Delete(testFollowee1);
            mockContext.SaveChanges();

            var followee = mockContext.Followees.SingleOrDefault(x => x.ScreenName == "TestScreenName1");
            Assert.IsTrue(followee.IsDeleted);
        }
    }
}
