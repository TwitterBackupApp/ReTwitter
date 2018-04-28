using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using System;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.FolloweeServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void NotReturnNull_When_FolloweeServiceCalled()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            var followeeService = new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                twitterApiCallServiceMock.Object, dateTimeParserMock.Object);

            Assert.IsNotNull(followeeService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() => 
            new FolloweeService(null, mapperMock.Object,
                twitterApiCallServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IMappingProvider()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new FolloweeService(unitOfWorkMock.Object, null,
                twitterApiCallServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITwitterApiCallService()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                null, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IDateTimeParser()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new FolloweeService(unitOfWorkMock.Object, mapperMock.Object,
                twitterApiCallServiceMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork_And_IMappingProvider()
        {
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new FolloweeService(null, null,
            twitterApiCallServiceMock.Object, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IMappingProvider_And_ITwitterApiCallService()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var dateTimeParserMock = new Mock<IDateTimeParser>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new FolloweeService(unitOfWorkMock.Object, null,
            null, dateTimeParserMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUnitOfWork_And_IDateTimeParser()
        {
            var mapperMock = new Mock<IMappingProvider>();
            var twitterApiCallServiceMock = new Mock<ITwitterApiCallService>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            new FolloweeService(null, mapperMock.Object,
            twitterApiCallServiceMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_All_Null_Parameters()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new FolloweeService(null, null, null, null));
        }
    }
}
