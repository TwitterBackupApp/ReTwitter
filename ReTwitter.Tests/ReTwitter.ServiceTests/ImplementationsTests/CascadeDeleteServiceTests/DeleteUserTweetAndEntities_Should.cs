using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Fakes;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.CascadeDeleteServiceTests
{
    [TestClass]
    public class DeleteUserTweetAndEntities_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UserId_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserTweetAndEntities(null, "123"));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_TweetId_Is_Null()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserTweetAndEntities("456", null));
        }

        [TestMethod]
        public void Invoke_DeleteUserTweet_In_userTweetService_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUserTweetSetvice = new Mock<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice.Object, fakeUserFolloweeService, fakeUnit.Object, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService);
            fakeUserTweetSetvice.Setup(s => s.AnyUserSavedThisTweetById(It.IsAny<string>())).Returns(true);

            //Act
            sut.DeleteUserTweetAndEntities("123", "456");

            //Assert
            fakeUserTweetSetvice.Verify(v => v.DeleteUserTweet(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_Delete_In_TweetService_When_Provided_Correct_Parameters_And_No_User_Saved_Tweet()
        {
            //Arrange
            var fakeUserTweetSetvice = new Mock<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = new Mock<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice.Object, fakeUserFolloweeService, fakeUnit.Object, fakeFolloweeService, fakeTweetService.Object, fakeTweetTagService, fakeAdminUserService);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTags = new List<TweetTag>();
            fakeTweetTagRepo.Setup(r => r.All).Returns(tweetTags.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeUserTweetSetvice.Setup(s => s.AnyUserSavedThisTweetById(It.IsAny<string>())).Returns(false);

            //Act
            sut.DeleteUserTweetAndEntities("123", "456");

            //Assert
            fakeTweetService.Verify(v => v.Delete(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_DeleteEntitiesOfTweet_In_Same_Method_When_Provided_Correct_Parameters_And_No_User_Saved_Tweet()
        {
            //Arrange
            var fakeUserTweetSetvice = new Mock<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = new Mock<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new FakeCascadeDeleteService(fakeUserTweetSetvice.Object, fakeUserFolloweeService, fakeUnit.Object, fakeFolloweeService, fakeTweetService.Object, fakeTweetTagService, fakeAdminUserService);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userTweets = new List<UserTweet>();
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweets.AsQueryable());
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUserTweetSetvice.Setup(s => s.AnyUserSavedThisTweetById(It.IsAny<string>())).Returns(false);

            //Act && Assert
            Assert.ThrowsException<FakeTestException>(() => sut.DeleteUserTweetAndEntities("123", "456"));
        }
    }
}
