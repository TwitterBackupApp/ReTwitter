using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Data.Models;
using ReTwitter.Data.Repository;
using ReTwitter.DTO;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetServiceTests
{
    [TestClass]
    public class GetTweetByTweetId_Should
    {
        [TestMethod]
        public void ReturnTweet_WhenInvokedAnExistingInDbTweetId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetId = "1",
                    Text = "Tweet text",
                    OriginalTweetCreatedOn = DateTime.Now,
                    FolloweeId = "1"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<TweetDto>(tweets[0]))
                 .Returns(new TweetDto { TweetId = tweets[0].TweetId });

            repoMock.Setup(r => r.All).Returns(tweets.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(repoMock.Object);

            var tweetService = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            var cut = tweetService.GetTweetByTweetId("1");

            Assert.AreEqual("1", cut.TweetId);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_TweetId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetId = "1",
                    Text = "Tweet text",
                    OriginalTweetCreatedOn = DateTime.Now,
                    FolloweeId = "1"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<TweetDto>(tweets[0]))
                 .Returns(new TweetDto { TweetId = tweets[0].TweetId });

            repoMock.Setup(r => r.All).Returns(tweets.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(repoMock.Object);

            var tweetService = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => tweetService.GetTweetByTweetId(null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_StringEmpty_TweetId()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetId = "1",
                    Text = "Tweet text",
                    OriginalTweetCreatedOn = DateTime.Now,
                    FolloweeId = "1"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<TweetDto>(tweets[0]))
                 .Returns(new TweetDto { TweetId = tweets[0].TweetId });

            repoMock.Setup(r => r.All).Returns(tweets.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(repoMock.Object);

            var tweetService = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentException>(() => tweetService.GetTweetByTweetId(""));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Tweet_Is_Null()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetId = "1",
                    Text = "Tweet text",
                    OriginalTweetCreatedOn = DateTime.Now,
                    FolloweeId = "1"
                }
            };

            mapperMock.Setup(x =>
                     x.MapTo<TweetDto>(tweets[0]))
                 .Returns(new TweetDto { TweetId = tweets[0].TweetId });

            repoMock.Setup(r => r.All).Returns(tweets.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(repoMock.Object);

            var tweetService = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => tweetService.GetTweetByTweetId("2"));
        }
    }
}