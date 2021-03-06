﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Data.Contracts;
using ReTwitter.Infrastructure.Providers;
using ReTwitter.Services.Data;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.Statistics;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TagServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_When_UnitOfWork_Is_Null()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TagService(null, fakeTimeProvider));
        }

        [TestMethod]
        public void Throw_Argument_Null_When_DateTimeProvider_Is_Null()
        {
            //Arrange
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TagService(fakeUnitOfWork, null));
        }


        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();


            //Act && Assert
            Assert.IsInstanceOfType(new TagService(fakeUnitOfWork, fakeTimeProvider), typeof(ITagService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var fakeTimeProvider = Mock.Of<IDateTimeProvider>();
            var fakeUnitOfWork = Mock.Of<IUnitOfWork>();
            //Act && Assert
            Assert.IsNotNull(new TagService(fakeUnitOfWork, fakeTimeProvider));
        }
    }
}
