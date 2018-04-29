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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetStatisticsServiceTests
{
    [TestClass]
    public class GetDeletedTweetsyUserId_Should
    {
        [TestMethod]
        public void Throw_Argument_Exception_When_String_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var sut = new TweetStatisticsService(fakeUnit);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.GetDeletedTweetsyUserId(null));
        }

        [TestMethod]
        public void Return_Correct_Value_When_Provided_Valid_Id()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTimeProvider = new TestDateTimeProvider();

            var testUser1 = new User { UserName = "TestUser1", Id = "TestId1" };
            var testUser2 = new User { UserName = "TestUser2", Id = "TestId2" };

            var testFollowee1 = new Followee { ScreenName = "TestScreenName1" };
            var testFollowee2 = new Followee { ScreenName = "TestScreenName1" };

            var testTweet1 = new Tweet { Text = "testTweet1", OriginalTweetCreatedOn = fakeTimeProvider.CreatedOn, TweetId = "testTweet1Id", Followee = testFollowee1 };
            var testTweet2 = new Tweet { Text = "testTweet2", OriginalTweetCreatedOn = fakeTimeProvider.CreatedOn, TweetId = "testTweet2Id", Followee = testFollowee2 };
            var testTweet3 = new Tweet { Text = "testTweet3", OriginalTweetCreatedOn = fakeTimeProvider.CreatedOn, TweetId = "testTweet3Id", Followee = testFollowee1 };

            var testUserTweet1 = new UserTweet { Tweet = testTweet1, TweetId = testTweet1.TweetId, UserId = testUser1.Id, User = testUser1, IsDeleted = true, DeletedOn = fakeTimeProvider.DeletedOn};
            var testUserTweet2 = new UserTweet { Tweet = testTweet2, TweetId = testTweet2.TweetId, UserId = testUser1.Id, User = testUser1, IsDeleted = false};
            var testUserTweet3 = new UserTweet { Tweet = testTweet3, TweetId = testTweet3.TweetId, UserId = testUser2.Id, User = testUser2 };

            var fakeUserTweetCollection = new List<UserTweet> { testUserTweet1, testUserTweet2, testUserTweet3 };

            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(fakeUserTweetCollection.AsQueryable());
            fakeUnitOfWork.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);

            var deletedModel = new DeletedTweetsModel
            {
                TweetDeletedOn = testUserTweet1.DeletedOn.Value,
                Text = testUserTweet1.Tweet.Text,
                OriginalTweetCreatedOn = testUserTweet1.Tweet.OriginalTweetCreatedOn,
                AuthorScreenName = testUserTweet1.Tweet.Followee.ScreenName
            };
           
            var expectedResult = new List<DeletedTweetsModel> { deletedModel };
            var sut = new TweetStatisticsService(fakeUnitOfWork.Object);

            //Act
            var actualResult = sut.GetDeletedTweetsyUserId("TestId1").ToList();

            //Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
