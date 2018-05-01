using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.Services.External;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Tests.ReTwitter.Services.External.Tests.TwitterApiCallerTests
{
    [TestClass]
    public class GetTwitterData_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_Input_Is_Null()
        {
            //Arrange
            var credentialsProverMock = new Mock<ITwitterCredentialsProvider>();
            credentialsProverMock.Setup(s => s.AccessToken).Returns("pesho");
            credentialsProverMock.Setup(s => s.AccessTokenSecret).Returns("gosho");
            credentialsProverMock.Setup(s => s.ConsumerSecret).Returns("merry");
            credentialsProverMock.Setup(s => s.ConsumerKey).Returns("minka");

            var sut = new TwitterApiCaller(credentialsProverMock.Object);
            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.GetTwitterData(null));
        }
    }
}
