﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.Statistics;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.StatisticsServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange, Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new StatisticsService(null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            
            //Act && Assert
            Assert.IsInstanceOfType(new StatisticsService(fakeUnitOfWork.Object), typeof(IStatisticsService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.IsNotNull(new StatisticsService(fakeUnitOfWork.Object));
        }
    }
}
