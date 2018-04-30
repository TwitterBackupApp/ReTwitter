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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserFolloweeServiceTests
{
    [TestClass]
    public class AnyUserSavedThisFolloweeById_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_FolloweeId_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var followee = new FolloweeFromApiDto();
            var sut = new UserFolloweeService(fakeUnit, fakeFolloweeService, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.AnyUserSavedThisFolloweeById(null));
        }

        [TestMethod]
        public void Return_True_When_User_Saved_This_Followee()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var userFollowee = new UserFollowee { UserId = "123", FolloweeId = "456" };
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };
            fakeUserFolloweeRepo.Setup(s => s.All).Returns(userFolloweeCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            var expectedResult = sut.AnyUserSavedThisFolloweeById(userFollowee.FolloweeId);

            //Assert
            Assert.IsTrue(expectedResult);
        }

        [TestMethod]
        public void Return_False_When_User_Saved_This_Followee()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var sut = new UserFolloweeService(fakeUnit.Object, fakeFolloweeService.Object, fakeDateTimeProvider);

            var userFollowee = new UserFollowee { UserId = "123", FolloweeId = "456" };
            var userFolloweeCollection = new List<UserFollowee> { userFollowee };
            fakeUserFolloweeRepo.Setup(s => s.All).Returns(userFolloweeCollection.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);

            //Act
            var expectedResult = sut.AnyUserSavedThisFolloweeById("123");

            //Assert
            Assert.IsFalse(expectedResult);
        }
    }
}
