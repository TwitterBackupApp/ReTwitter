using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Fakes;
using ReTwitter.Web.Controllers;

namespace ReTwitter.Tests.ReTwitter.Web.Tests.TweetControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_FolloweeService_Is_Null()
        {
            //Arrange
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetController(apiCallerServiceMock, null, userManagerMock.Object, userTweetServiceMock, cascadeDeleteServiceMock, null));
        }
        [TestMethod]
        public void Throw_ArgumentNullException_When_TweetService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetController(apiCallerServiceMock, null, userManagerMock.Object, userTweetServiceMock, cascadeDeleteServiceMock, followeeServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TwitterApiCallService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var tweetServiceMock = Mock.Of<ITweetService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetController(null, tweetServiceMock, userManagerMock.Object, userTweetServiceMock, cascadeDeleteServiceMock, followeeServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserTweetService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var tweetServiceMock = Mock.Of<ITweetService>();
            var userManagerMock = MockUserManager.New;
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetController(apiCallerServiceMock, tweetServiceMock, userManagerMock.Object, null, cascadeDeleteServiceMock, followeeServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_UserManager_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var tweetServiceMock = Mock.Of<ITweetService>();
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetController(apiCallerServiceMock, tweetServiceMock, null, userTweetServiceMock, cascadeDeleteServiceMock, followeeServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_CascadeDeleteService_Is_Null()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var tweetServiceMock = Mock.Of<ITweetService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetController(apiCallerServiceMock, tweetServiceMock, userManagerMock.Object, userTweetServiceMock, null, followeeServiceMock));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var tweetServiceMock = Mock.Of<ITweetService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();


            //Act && Assert
            Assert.IsInstanceOfType(new TweetController(apiCallerServiceMock, tweetServiceMock, userManagerMock.Object, userTweetServiceMock, cascadeDeleteServiceMock, followeeServiceMock), typeof(Controller));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var followeeServiceMock = Mock.Of<IFolloweeService>();
            var apiCallerServiceMock = Mock.Of<ITwitterApiCallService>();
            var tweetServiceMock = Mock.Of<ITweetService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();

            //Act && Assert
            Assert.IsNotNull(new TweetController(apiCallerServiceMock, tweetServiceMock, userManagerMock.Object, userTweetServiceMock, cascadeDeleteServiceMock, followeeServiceMock));

        }
    }
}