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

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetTagServiceTests
{
    [TestClass]
    public class TweetTagExists_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_String_Is_Null()
        {
            //Arrange
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit, fakeTagService, fakeDateTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.TweetTagExists(5, null));
        }

        [TestMethod]
        public void Return_True_When_Tweet_Tag_Exists()
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
            var exists = sut.TweetTagExists(1, "TestTweetId1");

            //Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void Return_False_When_Tweet_Tag_Does_Not_Exist()
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
            var exists = sut.TweetTagExists(1, "Pesho");

            //Assert
            Assert.IsFalse(exists);
        }
    }
}
