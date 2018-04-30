using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserFolloweeServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserFolloweeService(null, fakeFolloweeService, fakeTimeProvider));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TimeProvider_Is_Null()
        {
            //Arrange
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserFolloweeService(fakeUnitOfWork, fakeFolloweeService, null));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_FolloweeService_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserFolloweeService(fakeUnitOfWork, null, fakeTimeProvider));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            //Act && Assert
            Assert.IsInstanceOfType(new UserFolloweeService(fakeUnitOfWork, fakeFolloweeService, fakeTimeProvider), typeof(IUserFolloweeService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            //Act && Assert
            Assert.IsNotNull(new UserFolloweeService(fakeUnitOfWork, fakeFolloweeService, fakeTimeProvider));
        }
    }
}
