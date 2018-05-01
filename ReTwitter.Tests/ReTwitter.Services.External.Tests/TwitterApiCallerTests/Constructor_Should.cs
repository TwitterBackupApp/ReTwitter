using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.External;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Tests.ReTwitter.Services.External.Tests.TwitterApiCallerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_ITwitterCredentialsProvider_IsNull()
        {
            //Arrange, Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCaller(null));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_ConsumerKey_IsNull()
        {
            //Arrange
            var credentialsProverMock = new Mock<ITwitterCredentialsProvider>();
            credentialsProverMock.Setup(s => s.AccessToken).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.AccessTokenSecret).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.ConsumerSecret).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.ConsumerKey).Returns(string.Empty);
            
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCaller(credentialsProverMock.Object));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_ConsumerSecret_IsNull()
        {
            //Arrange
            var credentialsProverMock = new Mock<ITwitterCredentialsProvider>();
            credentialsProverMock.Setup(s => s.AccessToken).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.AccessTokenSecret).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.ConsumerSecret).Returns(string.Empty);
            credentialsProverMock.Setup(s => s.ConsumerKey).Returns(It.IsNotNull<string>());

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCaller(credentialsProverMock.Object));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_AccessToken_IsNull()
        {
            //Arrange
            var credentialsProverMock = new Mock<ITwitterCredentialsProvider>();
            credentialsProverMock.Setup(s => s.AccessToken).Returns(string.Empty);
            credentialsProverMock.Setup(s => s.AccessTokenSecret).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.ConsumerSecret).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.ConsumerKey).Returns(It.IsNotNull<string>());

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCaller(credentialsProverMock.Object));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_AccessTokenSecret_IsNull()
        {
            //Arrange
            var credentialsProverMock = new Mock<ITwitterCredentialsProvider>();
            credentialsProverMock.Setup(s => s.AccessToken).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.AccessTokenSecret).Returns(string.Empty);
            credentialsProverMock.Setup(s => s.ConsumerSecret).Returns(It.IsNotNull<string>());
            credentialsProverMock.Setup(s => s.ConsumerKey).Returns(It.IsNotNull<string>());

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TwitterApiCaller(credentialsProverMock.Object));
        }
    }
}
