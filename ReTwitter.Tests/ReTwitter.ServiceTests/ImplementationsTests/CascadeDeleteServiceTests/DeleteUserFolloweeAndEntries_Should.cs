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
    public class DeleteUserFolloweeAndEntries_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_FolloweeId_Is_Null()
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserFolloweeAndEntries(null, "123"));
        }

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
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteUserFolloweeAndEntries("456", null));
        }

        [TestMethod]
        public void Invoke_DeleteUserFollowee_In_userFolloweeService_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = new Mock<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService.Object, fakeUnit.Object, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userFollowees = new List<UserFollowee>();
            var userTweets = new List<UserTweet>();
            fakeUserFolloweeRepo.Setup(r => r.All).Returns(userFollowees.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweets.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUserFolloweeService.Setup(s => s.AnyUserSavedThisFolloweeById(It.IsAny<string>())).Returns(true);

            //Act
            sut.DeleteUserFolloweeAndEntries("123", "456");

            //Assert
            fakeUserFolloweeService.Verify(v => v.DeleteUserFollowee(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_AnyUserSavedThisFolloweeById_In_userFolloweeService_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = new Mock<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService.Object, fakeUnit.Object, fakeFolloweeService, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userFollowees = new List<UserFollowee>();
            var userTweets = new List<UserTweet>();
            fakeUserFolloweeRepo.Setup(r => r.All).Returns(userFollowees.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweets.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUserFolloweeService.Setup(s => s.AnyUserSavedThisFolloweeById(It.IsAny<string>())).Verifiable();

            //Act
            sut.DeleteUserFolloweeAndEntries("123", "456");

            //Assert
            fakeUserFolloweeService.Verify(v => v.AnyUserSavedThisFolloweeById(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_Delete_In_followeeService_When_Provided_Correct_Parameters_And_No_User_Saved_The_Followee()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = new Mock<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService.Object, fakeUnit.Object, fakeFolloweeService.Object, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userFollowees = new List<UserFollowee>();
            var userTweets = new List<UserTweet>();
            fakeUserFolloweeRepo.Setup(r => r.All).Returns(userFollowees.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweets.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUserFolloweeService.Setup(s => s.AnyUserSavedThisFolloweeById(It.IsAny<string>())).Returns(false);

            //Act
            sut.DeleteUserFolloweeAndEntries("123", "456");

            //Assert
            fakeFolloweeService.Verify(v => v.Delete(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_DeleteUserTweetAndEntities_In_Same_Method_When_Provided_Correct_Parameters_And_User_Saved_Tweets()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = new Mock<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = new Mock<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = Mock.Of<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new FakeCascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService.Object, fakeUnit.Object, fakeFolloweeService.Object, fakeTweetService, fakeTweetTagService, fakeAdminUserService);

            var fakeUserFolloweeRepo = new Mock<IGenericRepository<UserFollowee>>();
            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var userFollowees = new List<UserFollowee>();
            var testTweet = new Tweet {FolloweeId = "123", TweetId = "666"};
            var testUserTweet = new UserTweet {Tweet = testTweet, TweetId = testTweet.TweetId, UserId = "456"};
            var userTweets = new List<UserTweet>{ testUserTweet };
            fakeUserFolloweeRepo.Setup(r => r.All).Returns(userFollowees.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweets.AsQueryable());
            fakeUnit.Setup(u => u.UserFollowees).Returns(fakeUserFolloweeRepo.Object);
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUserFolloweeService.Setup(s => s.AnyUserSavedThisFolloweeById(It.IsAny<string>())).Returns(false);

            //Act & Assert
            Assert.ThrowsException<FakeTestException>(() => sut.DeleteUserFolloweeAndEntries("123", "456"));
        }
    }
}
