using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Controllers;

namespace ReTwitter.Tests.ReTwitter.Web.Tests.SearchControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_TwitterApiCallService_Is_Null()
        {
            //Arrange, Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SearchController(null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();

            //Act && Assert
            Assert.IsInstanceOfType(new SearchController(apiCallerMock), typeof(Controller));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var apiCallerMock = Mock.Of<ITwitterApiCallService>();


            //Act && Assert
            Assert.IsNotNull(new SearchController(apiCallerMock));
        }
    }
}
