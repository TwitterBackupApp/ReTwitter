﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Services.Data.TwitterApiService;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Tests.ReTwitter.ServiceTests.ImplementationsTests.TwitterApiCallServiceTests
{
    [TestClass]
    public class GetTweetsByUserScreenName_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_Input_Is_Null()
        {
            //Arrange
            var apiCallerMock = Mock.Of<ITwitterApiCaller>();
            var jsonDerserializerMock = Mock.Of<IJsonDeserializer>();
            var sut = new TwitterApiCallService(apiCallerMock, jsonDerserializerMock);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTweetsByUserScreenName(null));
        }

        [TestMethod]
        public void Invoke_GetTwitterData_From_ApiCaller_When_Input_Is_Valid()
        {
            //Arrange
            var apiCallerMock = new Mock<ITwitterApiCaller>();
            var jsonDerserializerMock = Mock.Of<IJsonDeserializer>();
            var sut = new TwitterApiCallService(apiCallerMock.Object, jsonDerserializerMock);

            //Act
            var result = sut.GetTweetsByUserScreenName("Pesho");

            //Assert
            apiCallerMock.Verify(v => v.GetTwitterData(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Invoke_Deserialize_From_jsonDeserializer_When_Input_Is_Valid()
        {
            //Arrange
            var apiCallerMock = new Mock<ITwitterApiCaller>();
            var jsonDerserializerMock = new Mock<IJsonDeserializer>();
            var sut = new TwitterApiCallService(apiCallerMock.Object, jsonDerserializerMock.Object);
            jsonDerserializerMock.Setup(s => s.Deserialize<TweetFromApiDto[]>(It.IsAny<string>())).Returns(new TweetFromApiDto[0]);

            //Act
            var result = sut.GetTweetsByUserScreenName("Pesho");

            //Assert
            jsonDerserializerMock.Verify(v => v.Deserialize<TweetFromApiDto[]>(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Return_Instance_Of_TypeFolloweeFromApiDtoArray_When_Input_Is_Valid()
        {
            //Arrange
            var apiCallerMock = new Mock<ITwitterApiCaller>();
            var jsonDerserializerMock = new Mock<IJsonDeserializer>();
            var sut = new TwitterApiCallService(apiCallerMock.Object, jsonDerserializerMock.Object);
            jsonDerserializerMock.Setup(s => s.Deserialize<TweetFromApiDto[]>(It.IsAny<string>())).Returns(new TweetFromApiDto[0]);

            //Act
            var result = sut.GetTweetsByUserScreenName("Pesho");

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TweetFromApiDto>));
        }
    }
}
