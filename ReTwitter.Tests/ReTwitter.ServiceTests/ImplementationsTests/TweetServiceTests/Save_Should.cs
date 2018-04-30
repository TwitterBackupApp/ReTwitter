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
    public class Save_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_Tweet_Is_Null()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Save(null));
        }

        [TestMethod]
        public void Saves_Tweet()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweetDto = new TweetDto
            {
                TweetId = "1",
                Text = "Tweet text",
                OriginalTweetCreatedOn = DateTime.Now,
            };

            var tweet = new Tweet
            {
                TweetId = tweetDto.TweetId,
                Text = tweetDto.Text,
                OriginalTweetCreatedOn = tweetDto.OriginalTweetCreatedOn,
                FolloweeId = "1"
            };

            var tweetCollection = new List<Tweet> { tweet };

            repoMock.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(repoMock.Object);
            unitOfWorkMock.Setup(u => u.Tweets.Add(It.IsAny<Tweet>())).Verifiable();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Save(tweetDto);

            //Assert
            unitOfWorkMock.Verify(v => v.Tweets.Add(It.IsAny<Tweet>()), Times.Once);
        }

        [TestMethod]
        public void Invokes_SaveChanges_When_Tweet_Is_Created()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();
            var repoMock = new Mock<IGenericRepository<Tweet>>();

            var tweetDto = new TweetDto
            {
                TweetId = "1",
                Text = "Tweet text",
                OriginalTweetCreatedOn = DateTime.Now,
            };

            var tweet = new Tweet
            {
                TweetId = tweetDto.TweetId,
                Text = tweetDto.Text,
                OriginalTweetCreatedOn = tweetDto.OriginalTweetCreatedOn,
                FolloweeId = "1"
            };

            var tweetCollection = new List<Tweet> { tweet };

            repoMock.Setup(r => r.AllAndDeleted).Returns(tweetCollection.AsQueryable());
            unitOfWorkMock.Setup(u => u.Tweets).Returns(repoMock.Object);
            unitOfWorkMock.Setup(u => u.Tweets.Add(It.IsAny<Tweet>())).Verifiable();

            var sut = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
               twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
               tagServiceMock.Object, dateTimeParserMock.Object);

            //Act
            sut.Save(tweetDto);

            //Assert
            unitOfWorkMock.Verify(v => v.SaveChanges(), Times.Once);
        }
    }
}
