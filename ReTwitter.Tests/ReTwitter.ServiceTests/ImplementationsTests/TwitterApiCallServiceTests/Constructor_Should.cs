using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.TwitterApiService;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TwitterApiCallServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_Argument_Null_Exception_When_ApiCaller_Is_Null()
        {
            //Arrange
            var deserializerMock = Mock.Of<IJsonDeserializer>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCallService(null, deserializerMock));
        }

        [TestMethod]
        public void Throw_Argument_Null_Exception_When_JsonDeserializer_Is_Null()
        {
            //Arrange
            var apiCallerMock = Mock.Of<ITwitterApiCaller>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCallService(apiCallerMock, null));
        }

        [TestMethod]
        public void Return_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var deserializerMock = Mock.Of<IJsonDeserializer>();
            var apiCallerMock = Mock.Of<ITwitterApiCaller>();

            //Act && Assert
            Assert.IsInstanceOfType(new TwitterApiCallService(apiCallerMock, deserializerMock), typeof(ITwitterApiCallService));
        }

        [TestMethod]
        public void Return_NotNull_Instance_When_Provided_Correct_Parameters()
        {
            //Arrange
            var deserializerMock = Mock.Of<IJsonDeserializer>();
            var apiCallerMock = Mock.Of<ITwitterApiCaller>();


            //Act && Assert
            Assert.IsNotNull(new TwitterApiCallService(apiCallerMock, deserializerMock));
        }
    }
}