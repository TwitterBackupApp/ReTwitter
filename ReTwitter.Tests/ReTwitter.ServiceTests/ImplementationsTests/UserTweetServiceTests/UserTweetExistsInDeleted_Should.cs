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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserTweetServiceTests
{
    [TestClass]
    public class UserTweetExistsInDeleted_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserTweetExistsInDeleted(null, "123"));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_TweetId_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserTweetExistsInDeleted("123", null));
        }

        [TestMethod]
        public void Return_True_When_UserTweet_Exists()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();

            var userTweet = new UserTweet { UserId = "123", TweetId = "456" };

            var userTweetCollection = new List<UserTweet> { userTweet };

            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);

            //Act
            var exists = sut.UserTweetExistsInDeleted(userTweet.UserId, userTweet.TweetId);

            //Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void Return_False_When_UserTweet_Does_Not_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();

            var userTweet = new UserTweet { UserId = "123", TweetId = "456" };

            var userTweetCollection = new List<UserTweet> { userTweet };

            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);

            //Act
            var exists = sut.UserTweetExistsInDeleted(userTweet.UserId, "666");

            //Assert
            Assert.IsFalse(exists);
        }
    }
}
