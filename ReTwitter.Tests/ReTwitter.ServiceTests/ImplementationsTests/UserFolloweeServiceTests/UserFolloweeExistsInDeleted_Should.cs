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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserFolloweeServiceTests
{
    [TestClass]
    public class UserFolloweeExistsInDeleted_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            var sut = new UserFolloweeService(fakeUnit, fakeFolloweeService, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserFolloweeExistsInDeleted(null, "pesho"));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_FolloweeId_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();

            var sut = new UserFolloweeService(fakeUnit, fakeFolloweeService, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserFolloweeExistsInDeleted("Petka", null));
        }

        [TestMethod]
        public void Return_True_When_UserFollowee_Exists()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService, fakeDateTimeProvider);

            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();

            var userFollowee = new UserFollowee { UserId = "123", FolloweeId = "456" };

            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            var exists = sut.UserFolloweeExistsInDeleted(userFollowee.UserId, userFollowee.FolloweeId);

            //Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void Return_False_When_UserFollowee_Does_Not_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService, fakeDateTimeProvider);

            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();

            var userFollowee = new UserFollowee { UserId = "123", FolloweeId = "456" };

            var userFolloweeCollection = new List<UserFollowee> { userFollowee };

            fakeUserFolloweeRepo.Setup(r => r.AllAndDeleted).Returns(userFolloweeCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            var exists = sut.UserFolloweeExistsInDeleted(userFollowee.UserId, "789");

            //Assert
            Assert.IsFalse(exists);
        }
    }
}
