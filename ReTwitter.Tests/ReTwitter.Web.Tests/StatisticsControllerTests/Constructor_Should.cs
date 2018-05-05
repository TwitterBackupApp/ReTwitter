using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Areas.Admin.Controllers;

namespace ReTwitter.Tests.ReTwitter.Web.Tests.StatisticsControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_FolloweeStatisticsService_Is_Null()
        {
            //Arrange
            var tweetStatisticsServiceMock = Mock.Of<ITweetStatisticsService>();
            var statisticsServiceMock = Mock.Of<IStatisticsService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new StatisticsController(null, tweetStatisticsServiceMock,
                    statisticsServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TweetStatisticsService_Is_Null()
        {
            //Arrange
            var followeeStatisticsServiceMock = Mock.Of<IFolloweeStatisticsService>();
            var statisticsServiceMock = Mock.Of<IStatisticsService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new StatisticsController(followeeStatisticsServiceMock, null,
                    statisticsServiceMock));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_StatisticsService_Is_Null()
        {
            //Arrange
            var followeeStatisticsServiceMock = Mock.Of<IFolloweeStatisticsService>();
            var tweetStatisticsServiceMock = Mock.Of<ITweetStatisticsService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new StatisticsController(followeeStatisticsServiceMock, tweetStatisticsServiceMock,
                    null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var followeeStatisticsServiceMock = Mock.Of<IFolloweeStatisticsService>();
            var tweetStatisticsServiceMock = Mock.Of<ITweetStatisticsService>();
            var statisticsServiceMock = Mock.Of<IStatisticsService>();

            //Act && Assert
            Assert.IsInstanceOfType(new StatisticsController(followeeStatisticsServiceMock, tweetStatisticsServiceMock,
                    statisticsServiceMock), typeof(Controller));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var followeeStatisticsServiceMock = Mock.Of<IFolloweeStatisticsService>();
            var tweetStatisticsServiceMock = Mock.Of<ITweetStatisticsService>();
            var statisticsServiceMock = Mock.Of<IStatisticsService>();

            //Act && Assert
            Assert.IsNotNull(new StatisticsController(followeeStatisticsServiceMock, tweetStatisticsServiceMock,
                statisticsServiceMock));
        }
    }
}
