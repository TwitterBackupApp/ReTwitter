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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.CascadeDeleteServiceTests
{
    [TestClass]
    public class DeleteEntitiesOfTweet_Should
    {
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteEntitiesOfTweet(null));
        }

        [TestMethod]
        public void Invoke_DeleteTweetTag_In_tweetTagService_When_Provided_Correct_Parameters_And_Tweet_Has_Tags()
        {
            //Arrange
            var fakeUserTweetSetvice = Mock.Of<IUserTweetService>();
            var fakeUserFolloweeService = Mock.Of<IUserFolloweeService>();
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeFolloweeService = Mock.Of<IFolloweeService>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var fakeTweetTagService = new Mock<ITweetTagService>();
            var fakeAdminUserService = Mock.Of<IAdminUserService>();
            var sut = new CascadeDeleteService(fakeUserTweetSetvice, fakeUserFolloweeService, fakeUnit.Object, fakeFolloweeService, fakeTweetService, fakeTweetTagService.Object, fakeAdminUserService);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "123", TagId = 2 };
            var tweetTags = new List<TweetTag> { tweetTag };
            fakeTweetTagRepo.Setup(r => r.All).Returns(tweetTags.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);

            //Act
            sut.DeleteEntitiesOfTweet("123");

            //Assert
            fakeTweetTagService.Verify(v => v.DeleteTweetTag(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}
