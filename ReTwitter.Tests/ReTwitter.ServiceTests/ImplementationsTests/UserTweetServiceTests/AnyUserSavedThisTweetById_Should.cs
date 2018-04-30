using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserTweetServiceTests
{
    [TestClass]
    public class AnyUserSavedThisTweetById_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_TweetId_Is_Null()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.AnyUserSavedThisTweetById(null));
        }

        [TestMethod]
        public void Return_True_When_User_Saved_This_Tweet()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();

            var userTweet = new UserTweet { UserId = "123", TweetId = "456" };
            var userTweetCollection = new List<UserTweet> { userTweet };
            fakeUserTweetRepo.Setup(s => s.All).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);

            //Act
            var expectedResult = sut.AnyUserSavedThisTweetById(userTweet.TweetId);

            //Assert
            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void Return_False_When_No_User_Saved_This_Tweet()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();

            var userTweet = new UserTweet { UserId = "123", TweetId = "456" };
            var userTweetCollection = new List<UserTweet> { userTweet };
            fakeUserTweetRepo.Setup(s => s.All).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);

            //Act
            var expectedResult = sut.AnyUserSavedThisTweetById("7777");

            //Assert
            Assert.IsFalse(expectedResult);
        }
    }
}
