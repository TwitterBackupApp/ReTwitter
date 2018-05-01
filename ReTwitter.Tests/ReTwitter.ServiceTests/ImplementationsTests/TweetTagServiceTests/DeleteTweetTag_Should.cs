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
using ReTwitter.Tests.Fakes;
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetTagServiceTests
{
    [TestClass]
    public class DeleteTweetTag_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.DeleteTweetTag(5, null));
        }

        [TestMethod]
        public void Invoke_Delete_Method_When_TweetTag_Exists()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1 };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.Delete(It.IsAny<TweetTag>())).Verifiable();
            fakeTweetTagRepo.Setup(r => r.All).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);


            //Act
            sut.DeleteTweetTag(tweetTag.TagId, tweetTag.TweetId);

            //Assert
            fakeTweetTagRepo.Verify(v => v.Delete(It.IsAny<TweetTag>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_TweetTag_Exists()
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
            fakeUnit.Setup(s => s.SaveChanges()).Verifiable();

            //Act
            sut.DeleteTweetTag(tweetTag.TagId, tweetTag.TweetId);

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Invoke_AnyTweetSavedThisTagById_In_Same_Method_When_TweetTag_Found()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new FakeTweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1 };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.All).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeUnit.Setup(s => s.SaveChanges()).Verifiable();

            //Act && Assert
            Assert.ThrowsException<FakeTestException>(() => sut.DeleteTweetTag(tweetTag.TagId, tweetTag.TweetId));
        }
    }
}
