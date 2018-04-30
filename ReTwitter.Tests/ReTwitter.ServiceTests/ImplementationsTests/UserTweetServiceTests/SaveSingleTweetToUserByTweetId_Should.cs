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
using ReTwitter.Tests.Providers;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.UserTweetServiceTests
{
    [TestClass]
    public class SaveSingleTweetToUserByTweetId_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_UserId_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserTweetExistsInDeleted(null, "123"));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_TweetId_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnit = Mock.Of<IUnitOfWork>();
            var fakeTweetService = Mock.Of<ITweetService>();
            var sut = new UserTweetService(fakeUnit, fakeTweetService, fakeTimeProvider);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.UserTweetExistsInDeleted("123", null));
        }

        [TestMethod]
        public void Invoke_CreateFromApiById_In_TweetService_When_Tweet_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();

            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);
            fakeTweetService.Setup(s => s.CreateFromApiById(It.IsAny<string>())).Returns(new Tweet { TweetId = "789" });

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "789");

            //Assert
            fakeTweetService.Verify(v => v.CreateFromApiById(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void Invoke_Add_In_UserTweetRepo_When_Tweet_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();

            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);
            fakeTweetService.Setup(s => s.CreateFromApiById(It.IsAny<string>())).Returns(new Tweet { TweetId = "789" });

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "789");

            //Assert
            fakeUserTweetRepo.Verify(v => v.Add(It.IsAny<UserTweet>()), Times.Once());
        }

        [TestMethod]
        public void Invoke_SaveChanges_In_UnitOfWork_When_Tweet_Is_New()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var fakeDateTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, fakeDateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();

            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);
            fakeTweetService.Setup(s => s.CreateFromApiById(It.IsAny<string>())).Returns(new Tweet { TweetId = "789" });

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "789");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Change_Deleted_State_When_Tweet_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetCollection = new List<Tweet> { tweet };
            var tweetTagCollection = new List<TweetTag>();
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.IsFalse(tweet.IsDeleted);
            Assert.IsNull(tweet.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_When_Tweet_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true };
            var tweetCollection = new List<Tweet> { tweet };
            var tweetTagCollection = new List<TweetTag>();
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.AreEqual(dateTimeProvider.Now, tweet.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Tweet_Exists_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true };
            var tweetCollection = new List<Tweet> { tweet };
            var tweetTagCollection = new List<TweetTag>();
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);
            fakeUnit.Setup(s => s.SaveChanges()).Verifiable();

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Exactly(2)); //Once for the Modified/Deleted and twice for the UserTweet
        }

        [TestMethod]
        public void Change_Deleted_State_Of_TweetTag_When_Tweet_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true };
            var tweetCollection = new List<Tweet> { tweet };
            var testTag1 = new Tag { Id = 1 };
            var testTag2 = new Tag { Id = 2 };
            var testTweetTag1 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag1.Id, Tag = testTag1, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTweetTag2 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag2.Id, Tag = testTag2, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetTagCollection = new List<TweetTag> { testTweetTag1, testTweetTag2 };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.IsFalse(testTweetTag1.IsDeleted);
            Assert.IsNull(testTweetTag1.DeletedOn);
            Assert.IsFalse(testTweetTag2.IsDeleted);
            Assert.IsNull(testTweetTag2.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_Of_TweetTag_When_Tweet_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true };
            var tweetCollection = new List<Tweet> { tweet };
            var testTag1 = new Tag { Id = 1 };
            var testTag2 = new Tag { Id = 2 };
            var testTweetTag1 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag1.Id, Tag = testTag1, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTweetTag2 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag2.Id, Tag = testTag2, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetTagCollection = new List<TweetTag> { testTweetTag1, testTweetTag2 };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.AreEqual(dateTimeProvider.Now, testTweetTag1.ModifiedOn.Value);
            Assert.AreEqual(dateTimeProvider.Now, testTweetTag2.ModifiedOn.Value);
        }

        [TestMethod]
        public void Change_Deleted_State_Of_Tag_When_Tweet_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetCollection = new List<Tweet> { tweet };
            var testTag1 = new Tag { Id = 1, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTag2 = new Tag { Id = 2, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTweetTag1 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag1.Id, Tag = testTag1, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTweetTag2 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag2.Id, Tag = testTag2, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetTagCollection = new List<TweetTag> { testTweetTag1, testTweetTag2 };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.IsFalse(testTag1.IsDeleted);
            Assert.IsNull(testTag1.DeletedOn);
            Assert.IsFalse(testTag2.IsDeleted);
            Assert.IsNull(testTag2.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_Of_Tag_When_Tweet_Exist_But_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            var fakeTweetTagRepo = new Mock<IGenericRepository<TweetTag>>();

            var tweet = new Tweet { TweetId = "456", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetCollection = new List<Tweet> { tweet };
            var testTag1 = new Tag { Id = 1, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTag2 = new Tag { Id = 2, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTweetTag1 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag1.Id, Tag = testTag1, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var testTweetTag2 = new TweetTag { TweetId = tweet.TweetId, TagId = testTag2.Id, Tag = testTag2, IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var tweetTagCollection = new List<TweetTag> { testTweetTag1, testTweetTag2 };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeUnit.Setup(u => u.TweetTags).Returns(fakeTweetTagRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeTweetTagRepo.Setup(r => r.AllAndDeleted).Returns(tweetTagCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.AreEqual(dateTimeProvider.Now, testTag1.ModifiedOn.Value);
            Assert.AreEqual(dateTimeProvider.Now, testTag2.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_Add_In_UserTweetRepo_When_Tweet_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();

            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            fakeUserTweetRepo.Verify(v => v.Add(It.IsAny<UserTweet>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_In_UnitOfWork_When_Tweet_Exist()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            
            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };
            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);
            fakeUnit.Setup(u => u.SaveChanges()).Verifiable();

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Change_Deleted_State_When_Tweet_Exist_TweetTag_Is_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            
            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };

            var userTweet = new UserTweet { TweetId = tweet.TweetId, UserId = "123", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userTweetCollection = new List<UserTweet> { userTweet };

            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.IsFalse(userTweet.IsDeleted);
            Assert.IsNull(userTweet.DeletedOn);
        }

        [TestMethod]
        public void Change_DeletedOn_When_Tweet_Exist_TweetTag_Is_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            
            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };

            var userTweet = new UserTweet { TweetId = tweet.TweetId, UserId = "123", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userTweetCollection = new List<UserTweet> { userTweet };

            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            Assert.AreEqual(dateTimeProvider.Now, userTweet.ModifiedOn.Value);
        }

        [TestMethod]
        public void Invoke_SaveChanges_In_UnitOfWork_When_Tweet_Exist_TweetTag_Is_Deleted()
        {
            //Arrange
            var fakeUnit = new Mock<IUnitOfWork>();
            var dateTimeProvider = new TestDateTimeProvider();
            var fakeTweetService = new Mock<ITweetService>();
            var sut = new UserTweetService(fakeUnit.Object, fakeTweetService.Object, dateTimeProvider);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();
            var fakeTweetRepo = new Mock<IGenericRepository<Tweet>>();
            
            var tweet = new Tweet { TweetId = "456" };
            var tweetCollection = new List<Tweet> { tweet };

            var userTweet = new UserTweet { TweetId = tweet.TweetId, UserId = "123", IsDeleted = true, DeletedOn = dateTimeProvider.DeletedOn };
            var userTweetCollection = new List<UserTweet> { userTweet };

            fakeUnit.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            fakeTweetRepo.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            fakeUserTweetRepo.Setup(r => r.AllAndDeleted).Returns(userTweetCollection.AsQueryable());
            fakeUnit.Setup(u => u.Tweets).Returns(fakeTweetRepo.Object);
            fakeUnit.Setup(u => u.SaveChanges()).Verifiable();

            //Act
            sut.SaveSingleTweetToUserByTweetId("123", "456");

            //Assert
            fakeUnit.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}