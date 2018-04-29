using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetTagServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetTagService(null, fakeTagService, fakeTimeProvider));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_DateTimeProvider_Is_Null()
        {
            //Arrange
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeTagService = Mock.Of<ITagService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetTagService(fakeUnitOfWork, fakeTagService, null));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TagService_Is_Null()
        {
            //Arrange
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetTagService(fakeUnitOfWork, null, fakeTimeProvider));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeTagService = Mock.Of<ITagService>();

            //Act && Assert
            Assert.IsInstanceOfType(new TweetTagService(fakeUnitOfWork, fakeTagService, fakeTimeProvider), typeof(ITweetTagService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeTagService = Mock.Of<ITagService>();

            //Act && Assert
            Assert.IsNotNull(new TweetTagService(fakeUnitOfWork, fakeTagService, fakeTimeProvider));
        }
    }
}
