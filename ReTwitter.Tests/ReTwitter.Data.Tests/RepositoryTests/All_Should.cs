using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Tests.Providers;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.Data.Tests.RepositoryTests
{
    [TestClass]
    public class All_Should
    {
        [TestMethod]
        public void ReturnCorrectNumberOfCountries_WhenCalled()
        {
            var mockContext = DatabaseProvider.GetDatabase();

            var followeeRepository = new GenericRepository<Followee>(mockContext);

            var testFollowee1 = new Followee { ScreenName = "TestScreenName1", Bio = "TestBio1TestBio1TestBio1TestBio1TestBio1", FolloweeId = "TestFolloweeId1", Name = "TestFolloweeName1" };
            var testFollowee2 = new Followee { ScreenName = "TestScreenName2", Bio = "TestBio2TestBio2TestBio2TestBio2TestBio2", FolloweeId = "TestFolloweeId2", Name = "TestFolloweeName2" };
            var testFollowee3 = new Followee { ScreenName = "TestScreenName3", Bio = "TestBio3TestBio3TestBio3TestBio3TestBio3", FolloweeId = "TestFolloweeId3", Name = "TestFolloweeName3" };

            mockContext.Followees.Add(testFollowee1);
            mockContext.Followees.Add(testFollowee2);
            mockContext.Followees.Add(testFollowee3);

            mockContext.SaveChanges();

            var result = followeeRepository.All.ToList();

            Assert.AreEqual(3, result.Count());
        }
    }
}
