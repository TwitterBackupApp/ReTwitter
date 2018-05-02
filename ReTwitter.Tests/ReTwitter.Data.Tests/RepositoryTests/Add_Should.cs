using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;
using System;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.RepositoryTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_When_InvokedWithNullValue()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var tweetRepository = new GenericRepository<Tweet>(mockContext);

            Tweet nullTweet = null;

            Assert.ThrowsException<ArgumentNullException>(() => tweetRepository.Add(nullTweet));
        }

        [TestMethod]
        public void AddsSuccesfully_When_InvokedWithCorrectValues()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var followeeRepository = new GenericRepository<Followee>(mockContext);

            var testFollowee1 = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };
            var testFollowee2 = new Followee { ScreenName = "TestScreenName2", Bio = "TestBio2TestBio2TestBio2TestBio2TestBio2", FolloweeId = "TestFolloweeId2", Name = "TestFolloweeName2" };
            var testFollowee3 = new Followee { ScreenName = "TestScreenName3", Bio = "TestBio3TestBio3TestBio3TestBio3TestBio3", FolloweeId = "TestFolloweeId3", Name = "TestFolloweeName3" };

            followeeRepository.Add(testFollowee1);
            followeeRepository.Add(testFollowee2);
            followeeRepository.Add(testFollowee3);

            mockContext.SaveChanges();

            Assert.AreEqual(3, mockContext.Followees.Count());
        }
    }
}
