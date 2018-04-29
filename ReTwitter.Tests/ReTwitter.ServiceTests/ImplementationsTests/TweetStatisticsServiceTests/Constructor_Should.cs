using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.Statistics;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TweetStatisticsServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange, Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetStatisticsService(null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.IsInstanceOfType(new TweetStatisticsService(fakeUnitOfWork.Object), typeof(ITweetStatisticsService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.IsNotNull(new TweetStatisticsService(fakeUnitOfWork.Object));
        }
    }
}
