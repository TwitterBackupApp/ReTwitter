using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Fakes.Models;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserTweetServiceTests
{
    [TestClass]
    public class GetTweetsByUserIdAndFolloweeId_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTweetsByUserIdAndFolloweeId(null, "123"));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_FolloweeId_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTweetsByUserIdAndFolloweeId("123", null));
        }

        [TestMethod]
        public void Return_Correct_Values_When_UserTweets_Exist()
        {
            //Arrange
            var timeProvider = new TestDateTimeProvider();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService, timeProvider);
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();

            var testTweet1 = new Tweet { Text = "test1", TweetId = "10", OriginalTweetCreatedOn = timeProvider.OriginallyCreatedOn, FolloweeId = "666" };
            var testTweet2 = new Tweet { Text = "test2", TweetId = "11", OriginalTweetCreatedOn = timeProvider.OriginallyCreatedOn, FolloweeId = "666" };
            var testTweet3 = new Tweet { Text = "test3", TweetId = "12", OriginalTweetCreatedOn = timeProvider.OriginallyCreatedOn, FolloweeId = "789" };

            var testUserTweet1 = new UserTweet { UserId = "100", Tweet = testTweet1, TweetId = testTweet1.TweetId };
            var testUserTweet2 = new UserTweet { UserId = "100", Tweet = testTweet2, TweetId = testTweet2.TweetId };
            var testUserTweet3 = new UserTweet { UserId = "100", Tweet = testTweet3, TweetId = testTweet3.TweetId };

            var userTweetCollection = new List<UserTweet> { testUserTweet1, testUserTweet2, testUserTweet3 };
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            var expectedTweet1 = new FakeTweetDto
            {
                TweetId = testTweet1.TweetId,
                OriginalTweetCreatedOn = testTweet1.OriginalTweetCreatedOn,
                Text = testTweet1.Text,
                UsersMentioned = testTweet1.UsersMentioned
            };

            var expectedTweet2 = new FakeTweetDto
            {
                TweetId = testTweet2.TweetId,
                OriginalTweetCreatedOn = testTweet2.OriginalTweetCreatedOn,
                Text = testTweet2.Text,
                UsersMentioned = testTweet2.UsersMentioned
            };
            var expectedTweetDtos = new List<FakeTweetDto> { expectedTweet1, expectedTweet2 };

            //Act
            var actualTweetDtos = sut.GetTweetsByUserIdAndFolloweeId("100", "666");

            //Assert
            CollectionAssert.AreEqual(expectedTweetDtos, actualTweetDtos.ToList());
        }
    }
}
