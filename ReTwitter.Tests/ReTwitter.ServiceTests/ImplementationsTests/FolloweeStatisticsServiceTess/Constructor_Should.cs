using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.Statistics;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.FolloweeStatisticsServiceTess
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_Unit_Of_Work_Is_Null()
        {
            //Arrange, Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new FolloweeStatisticsService(null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.IsInstanceOfType(new FolloweeStatisticsService(fakeUnitOfWork.Object), typeof(IFolloweeStatisticsService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.IsNotNull(new FolloweeStatisticsService(fakeUnitOfWork.Object));
        }
    }
}
