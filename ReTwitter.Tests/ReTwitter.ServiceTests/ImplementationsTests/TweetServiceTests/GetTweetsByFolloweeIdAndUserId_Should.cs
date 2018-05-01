using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetServiceTests
{
    [TestClass]
    public class GetTweetsByFolloweeIdAndUserId_Should
    {
        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_FolloweeId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            var userId = "someUsersId";
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTweetsByFolloweeIdAndUserId(null, userId));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_UserId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            var followeeId = "someFolloweeId";
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTweetsByFolloweeIdAndUserId(followeeId, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_FolloweeId_And_UserId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTweetsByFolloweeIdAndUserId(null, null));
        }

        [TestMethod]
        public void Throws_ArgumentException_When_Called_With_StringEmpty_FolloweeId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            var userId = "someUsersId";
            Assert.ThrowsException<ArgumentException>(() => sut.GetTweetsByFolloweeIdAndUserId("", userId));
        }

        [TestMethod]
        public void Throws_ArgumentException_When_Called_With_StringEmpty_UserId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            var followeeId = "someFolloweeId";
            Assert.ThrowsException<ArgumentException>(() => sut.GetTweetsByFolloweeIdAndUserId(followeeId, ""));
        }

        [TestMethod]
        public void Throws_ArgumentException_When_Called_With_StringEmpty_FolloweeId_And_UserId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.GetTweetsByFolloweeIdAndUserId("", ""));
        }


        [TestMethod]
        public void Return_Correct_Values_When_UserTweets_Exist()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var timeProvider = new TestDateTimeProvider();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
              twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
              tagServiceMock.Object, dateTimeParserMock.Object);

            var fakeUserTweetRepo = new Mock<IGenericRepository<UserTweet>>();

            var testTweet1 = new Tweet { Text = "test1", TweetId = "10", OriginalTweetCreatedOn = timeProvider.OriginallyCreatedOn, FolloweeId = "666" };
            var testTweet2 = new Tweet { Text = "test2", TweetId = "11", OriginalTweetCreatedOn = timeProvider.OriginallyCreatedOn, FolloweeId = "666" };
            var testTweet3 = new Tweet { Text = "test3", TweetId = "12", OriginalTweetCreatedOn = timeProvider.OriginallyCreatedOn, FolloweeId = "789" };

            var testUserTweet1 = new UserTweet { UserId = "100", Tweet = testTweet1, TweetId = testTweet1.TweetId };
            var testUserTweet2 = new UserTweet { UserId = "100", Tweet = testTweet2, TweetId = testTweet2.TweetId };
            var testUserTweet3 = new UserTweet { UserId = "100", Tweet = testTweet3, TweetId = testTweet3.TweetId };

            var userTweetCollection = new List<UserTweet> { testUserTweet1, testUserTweet2, testUserTweet3 };
            fakeUserTweetRepo.Setup(r => r.All).Returns(userTweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.UserTweets).Returns(fakeUserTweetRepo.Object);
            var expectedTweet1 = new TweetDto
            {
                TweetId = testTweet1.TweetId,
                OriginalTweetCreatedOn = testTweet1.OriginalTweetCreatedOn,
                Text = testTweet1.Text,
                UsersMentioned = testTweet1.UsersMentioned
            };

            var expectedTweet2 = new TweetDto
            {
                TweetId = testTweet2.TweetId,
                OriginalTweetCreatedOn = testTweet2.OriginalTweetCreatedOn,
                Text = testTweet2.Text,
                UsersMentioned = testTweet2.UsersMentioned
            };
            var expectedTweetDtos = new List<TweetDto> { expectedTweet1, expectedTweet2 };

            //Act
            var actualTweetDtos = sut.GetTweetsByFolloweeIdAndUserId("666","100");

            //Assert
            CollectionAssert.AreEqual(expectedTweetDtos, actualTweetDtos.ToList());
        }
    }
}
