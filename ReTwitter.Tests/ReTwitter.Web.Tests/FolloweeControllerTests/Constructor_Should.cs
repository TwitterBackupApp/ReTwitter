using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Fakes;
using ReTwitter.Web.Controllers;

namespace ReTwitter.Tests.ReTwitter.Web.Tests.FolloweeControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_FolloweeService_Is_Null()
        {
            //Arrange
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();
            var userFolloweeServiceMock = Mock.Of<IUserFolloweeService>();
            var userManagerMock = MockUserManager.New;
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new FolloweeController(null, apiCallerMock, userFolloweeServiceMock, userManagerMock.Object, cascadeDeleteServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TwitterApiCallService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var userFolloweeServiceMock = Mock.Of<IUserFolloweeService>();
            var userManagerMock = MockUserManager.New;
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new FolloweeController(followeeServiceMock, null, userFolloweeServiceMock, userManagerMock.Object, cascadeDeleteServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserFolloweeService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();
            var userManagerMock = MockUserManager.New;
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new FolloweeController(followeeServiceMock, apiCallerMock, null, userManagerMock.Object, cascadeDeleteServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserManager_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();
            var userFolloweeServiceMock = Mock.Of<IUserFolloweeService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new FolloweeController(followeeServiceMock, apiCallerMock, userFolloweeServiceMock, null, cascadeDeleteServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_CascadeDeleteService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();
            var userFolloweeServiceMock = Mock.Of<IUserFolloweeService>();
            var userManagerMock = MockUserManager.New;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new FolloweeController(followeeServiceMock, apiCallerMock, userFolloweeServiceMock, userManagerMock.Object, null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();
            var userFolloweeServiceMock = Mock.Of<IUserFolloweeService>();
            var userManagerMock = MockUserManager.New;
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();


            //Act && Assert
            Assert.IsInstanceOfType(new FolloweeController(followeeServiceMock, apiCallerMock, userFolloweeServiceMock, userManagerMock.Object, cascadeDeleteServiceMock), typeof(Controller));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();
            var userFolloweeServiceMock = Mock.Of<IUserFolloweeService>();
            var userManagerMock = MockUserManager.New;
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act && Assert
            Assert.IsNotNull(new FolloweeController(followeeServiceMock, apiCallerMock, userFolloweeServiceMock, userManagerMock.Object, cascadeDeleteServiceMock));
        }
    }
}
