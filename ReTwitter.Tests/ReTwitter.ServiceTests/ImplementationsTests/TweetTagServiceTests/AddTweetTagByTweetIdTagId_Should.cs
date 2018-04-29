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
    public class AddTweetTagByTweetIdTagId_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sut.AddTweetTagByTweetIdTagId(5, null));
        }

        [TestMethod]
        public void Invoke_Add_Method_When_TweetTag_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1 };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeUnit.Setup(s => s.TweetTags.Add(It.IsAny<TweetTag>())).Verifiable();
            
            //Act
            sut.AddTweetTagByTweetIdTagId(2, "TestTweetId2");

            //Assert
            fakeUnit.Verify(v => v.TweetTags.Add(It.IsAny<TweetTag>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_TweetTag_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1 };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeUnit.Setup(s => s.SaveChanges()).Verifiable();

            //Act
            sut.AddTweetTagByTweetIdTagId(3, "TestTweetId5");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Change_Deleted_State_When_TweetTag_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1, IsDeleted = true};
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);

            //Act
            sut.AddTweetTagByTweetIdTagId(1, "TestTweetId1");
            var addedTag =
                fakeTweetTagRepo.Object.AllAndDeleted.FirstOrDefault(w => w.TweetId == "TestTweetId1" && w.TagId == 1);

            //Assert
            Assert.IsFalse(addedTag.IsDeleted);
            Assert.IsNull(addedTag.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_When_TweetTag_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1, IsDeleted = true };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);

            //Act
            sut.AddTweetTagByTweetIdTagId(1, "TestTweetId1");
            var addedTag =
                fakeTweetTagRepo.Object.AllAndDeleted.FirstOrDefault(w => w.TweetId == "TestTweetId1" && w.TagId == 1);

            //Assert
            Assert.AreEqual(fakeDateTimeProvider.Now, addedTag.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_TweetTag_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTagService = Mock.Of<ITagService>();

            var sut = new TweetTagService(fakeUnit.Object, fakeTagService, fakeDateTimeProvider);

            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();
            var tweetTag = new TweetTag { TweetId = "TestTweetId1", TagId = 1, IsDeleted = true };
            var tweetTagsCollection = new List<TweetTag> { tweetTag };

            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagsCollection.AsQueryable());
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);

            //Act
            sut.AddTweetTagByTweetIdTagId(1, "TestTweetId1");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
