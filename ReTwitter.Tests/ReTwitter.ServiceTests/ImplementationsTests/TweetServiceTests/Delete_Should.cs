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
    public class Delete_Should
    {
        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_TweetId()
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

            Assert.ThrowsException<ArgumentNullException>(() => sut.Delete(null));
        }

        [TestMethod]
        public void Throws_ArgumentException_When_Called_With_StringEmpty_TweetId()
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

            Assert.ThrowsException<ArgumentException>(() => sut.Delete(""));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_Tweet_Not_Found()
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

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.Delete("2"));
        }

        [TestMethod]
        public void Invoke_Delete_When_Tweet_Exists()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweet = new Tweet
            {
                TweetId = "1",
                Text = "Tweet text",
                OriginalTweetCreatedOn = DateTime.Now,
                FolloweeId = "1"
            };

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

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Delete(tweet.TweetId);

            //Assert
            repoMock.Verify(v => v.Delete(It.IsAny<Tweet>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_SaveChanges_When_Tweet_Exists()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweet = new Tweet
            {
                TweetId = "1",
                Text = "Tweet text",
                OriginalTweetCreatedOn = DateTime.Now,
                FolloweeId = "1"
            };

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

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Delete(tweet.TweetId);

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
