using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReTwitter.DTO.TwitterDto;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Tests.Fakes;
using ReTwitter.Web.Controllers;
using ReTwitter.Web.Models.TweetViewModel;

namespace ReTwitter.Tests.ReTwitter.Web.Tests.TweetControllerTests
{
    [TestClass]
    public class TweetSearchResult_Should
    {
        [TestMethod]
        public void Return_View_When_Provided_Correct_Parameter()
        {
            //Arrange
            var apiCallerServiceMock = new Mock<ITwitterApiCallService>();
            var tweetServiceMock = new Mock<ITweetService>();
            var followeeServiceMock = new Mock<IFolloweeService>();
            var userManagerMock = MockUserManager.New;
            var userTweetServiceMock = Mock.Of<IUserTweetService>();
            var cascadeDeleteServiceMock = Mock.Of<ICascadeDeleteService>();
            var sut = new TweetController(apiCallerServiceMock.Object, tweetServiceMock.Object, userManagerMock.Object,
                userTweetServiceMock, cascadeDeleteServiceMock, followeeServiceMock.Object);

            var tweetFromApiCollection = new[] {new TweetFromApiDto(), new TweetFromApiDto()};
            apiCallerServiceMock.Setup(s => s.GetTweetsByUserId(It.IsAny<string>())).Returns(tweetFromApiCollection);

            //Act
            var result = sut.TweetSearchResult("123");

            // Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<TweetSearchResultViewModel>().TweetSearchResults == tweetFromApiCollection);
        }
    }
}
