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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetTagServiceTests
{
    [TestClass]
    public class AnyTweetSavedThisTagById_Should
    {
        [TestMethod]
        public void Return_True_If_Saved()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1 };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.All).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);

            //Act
            var exists = sut.AnyTweetSavedThisTagById(1);

            //Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void Return_False_If_Not_Saved()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1 };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.All).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);

            //Act
            var exists = sut.AnyTweetSavedThisTagById(2);

            //Assert
            Assert.IsFalse(exists);
        }
    }
}
