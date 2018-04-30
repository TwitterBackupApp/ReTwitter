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
    public class DeleteUserTweet_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserTweet(null, "123"));
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserTweet("123", null));
        }

        [TestMethod]
        public void Invoke_Delete_On_UserTweetRepo_When_Found()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userTweet = new UserTweet { TweetId = "789", UserId = "123"};
            var userTweetCollection = new List<UserTweet> { userTweet };
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);

            //Act
            sut.DeleteUserTweet("123", "789");

            //Assert
            fakeUserTweetRepo.Verify(v => v.Delete(It.IsAny<UserTweet>()), Times.Once());
        }

        [TestMethod]
        public void Invoke_SaveChanges_On_UnitOfWork_When_Found()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userTweet = new UserTweet { TweetId = "789", UserId = "123" };
            var userTweetCollection = new List<UserTweet> { userTweet };
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(s => s.SaveChanges()).Verifiable();

            //Act
            sut.DeleteUserTweet("123", "789");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
