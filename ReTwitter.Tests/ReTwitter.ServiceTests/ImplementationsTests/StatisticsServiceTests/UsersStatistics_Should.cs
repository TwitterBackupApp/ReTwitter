using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO.StatisticsModels;
using ReTwitter.Services.Data.Statistics;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.StatisticsServiceTests
{
    [TestClass]
    public class UsersStatistics_Should
    {
        [TestMethod]
        public void Return_Correct_Reports_When_Provided_Valid_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            var fakeUserRepo = new Mock<IGenericRepository<User>>();
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var fakeTimeProvider = new TestDateTimeProvider();

            var testUser1 = new User { UserName = "TestUser1", Id = "TestId1", CreatedOn = fakeTimeProvider.CreatedOn, IsDeleted = true };
            var testUser2 = new User { UserName = "TestUser2", Id = "TestId2", CreatedOn = fakeTimeProvider.CreatedOn, IsDeleted = false };
            var fakeUserCollection = new List<User> { testUser1, testUser2 };
            var testUserFollowee1 = new UserFollowee { User = testUser1, IsDeleted = true };
            var testUserFollowee2 = new UserFollowee { User = testUser2, IsDeleted = true };
            var testUserFollowee3 = new UserFollowee { User = testUser2, IsDeleted = false };
            var fakeUserFolloweeCollection = new List<UserFollowee> { testUserFollowee1, testUserFollowee2, testUserFollowee3 };
            var testUserTweet1 = new UserTweet { User = testUser1, IsDeleted = true };
            var testUserTweet2 = new UserTweet { User = testUser2, IsDeleted = true };
            var testUserTweet3 = new UserTweet { User = testUser2, IsDeleted = false };
            var fakeUserTweetCollection = new List<UserTweet> { testUserTweet1, testUserTweet2, testUserTweet3 };

            fakeUserRepo.Setup(r => r.AllAndDeleted).Returns(fakeUserCollection.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(fakeUserTweetCollection.AsQueryable());
            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(fakeUserFolloweeCollection.AsQueryable());
            fakeUnitOfWork.Setup(u => u.Users).Returns(fakeUserRepo.Object);
            fakeUnitOfWork.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnitOfWork.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            var sut = new StatisticsService(fakeUnitOfWork.Object);
            var statisticsModel1 = new UserStatisticsModel
            {
                ActivelyFollowedAccountsCount = 0,
                DeletedAccountsCount = 1,
                ActiveStatus = "Deleted",
                UserId = "TestId1",
                UserName = "TestUser1",
                SavedTweetsCount = 0,
                DeletedTweetsCount = 1,
                UserNameCreatedOn = fakeTimeProvider.CreatedOn
            };
            var statisticsModel2 = new UserStatisticsModel
            {
                ActivelyFollowedAccountsCount = 1,
                DeletedAccountsCount = 1,
                ActiveStatus = "Active",
                UserId = "TestId2",
                UserName = "TestUser2",
                SavedTweetsCount = 1,
                DeletedTweetsCount = 1,
                UserNameCreatedOn = fakeTimeProvider.CreatedOn
            };
            var totalStatisticsModel = new TotalStatisticsModel
            {
                TotalDeletedAccountsCount = 2,
                TotalSavedTweetsCount = 1,
                TotalDeletedTweetsCount = 2,
                TotalUsers = 2,
                TotalActivelyFollowedAccountsCount = 1
            };
            var usesStatisticsModels = new Dictionary<string, UserStatisticsModel>
            {
                ["TestUser1"] = statisticsModel1,
                ["TestUser2"] = statisticsModel2
            };

            var expectedOutput = new Tuple<IEnumerable<UserStatisticsModel>, TotalStatisticsModel>(usesStatisticsModels.Values, totalStatisticsModel);
            
            //Act
            var actualOutput = sut.UsersStatistics();

            //Assert

            Assert.AreEqual(expectedOutput.Item2, actualOutput.Item2);
            CollectionAssert.AreEqual(expectedOutput.Item1.ToList(), actualOutput.Item1.ToList());
        }
    }
}
