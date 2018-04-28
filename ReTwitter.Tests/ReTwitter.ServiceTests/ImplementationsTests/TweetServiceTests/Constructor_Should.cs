using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void NotReturnNull_When_TweetServiceCalled()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var tweetService = new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object, 
                tagServiceMock.Object, dateTimeParserMock.Object);

            Assert.IsNotNull(tweetService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, null,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IMappingProvider()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(null, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITwitterApiCallService()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                null, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IDateTimeParser()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITweetTagServicer()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, null,
                tagServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITagService()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                null, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork_And_IMappingProvider()
        {
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(null, null,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IMappingProvider_And_ITwitterApiCallService()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(null, unitOfWorkMock.Object,
                null, tweetTagServiceMock.Object,
                tagServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork_And_IDateTimeParser()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var tweetTagServiceMock = new Mock<ITweetTagService>();
            var tagServiceMock = new Mock<ITagService>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, null,
                twitterApiCallServiceMock.Object, tweetTagServiceMock.Object,
                tagServiceMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITweetTagService_And_ITagService()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new TweetService(mapperMock.Object, unitOfWorkMock.Object,
                twitterApiCallServiceMock.Object, null,
                null, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_All_Null_Parameters()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetService(null, null, null, null, null, null));
        }
    }
}
