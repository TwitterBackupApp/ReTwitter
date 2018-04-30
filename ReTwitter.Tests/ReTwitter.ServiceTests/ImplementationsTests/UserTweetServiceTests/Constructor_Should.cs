using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserTweetServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = Mock.Of<ITweetService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserTweetService(null, fakeTweetService, fakeTimeProvider));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TweetService_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();


            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserTweetService(fakeUnit, null, fakeTimeProvider));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TimeProvider_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserTweetService(fakeUnit, fakeTweetService, null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            
            //Act && Assert
            Assert.IsInstanceOfType(new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider), typeof(IUserTweetService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            
            //Act && Assert
            Assert.IsNotNull(new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider));
        }
    }
}
