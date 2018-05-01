using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.CascadeDeleteServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UserTweetService_Is_Null()
        {
            //Arrange
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(null, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_UserFolloweeService_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(fakeUserTweetSetvice, null, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, null, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_FolloweeService_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, null, fakeTweetService, fakeTweetTagService, fakeAdminUserService));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TweetService_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, null, fakeTweetTagService, fakeAdminUserService));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TweetTagService_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, null, fakeAdminUserService));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_AdminUserService_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.IsInstanceOfType(new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService), typeof(ICascadeDeleteService));
        }

       [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();

            //Act && Assert
            Assert.IsNotNull(new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService));
        }
    }
}
